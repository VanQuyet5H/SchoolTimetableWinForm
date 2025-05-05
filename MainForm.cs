using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SchoolTimetableWinForm.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolTimetableWinForm
{
    public partial class MainForm : Form
    {
        private readonly DbContextOptions<SchoolTimetableContext> _contextOptions;
        private readonly string[] _daysOfWeek = { "T2", "T3", "T4", "T5", "T6", "T7", "CN" };
        private List<School> _cachedSchools;
        private List<TimeSlot> _cachedTimeSlots;
        private List<Teacher> _cachedTeachers;
        private List<Week> _cachedWeeks;
        private List<Class> _cachedClasses;
        private List<SchoolRowGroup> _schoolRowGroups;
        private int _currentPage = 1;
        private int _pageSize = 5;
        private int _totalSchools = 0;
        private int _totalPages = 0;
        private bool _isTimetableMode = true;
        private bool _isUpdating = false;
        private ContextMenuStrip _contextMenu;
        private CancellationTokenSource _loadTimetableCts = new CancellationTokenSource();

        public class SchoolRowGroup
        {
            public School School { get; set; }
        }

        public MainForm(DbContextOptions<SchoolTimetableContext> contextOptions)
        {
            _contextOptions = contextOptions ?? throw new ArgumentNullException(nameof(contextOptions));
            _cachedSchools = new List<School>();
            _cachedTimeSlots = new List<TimeSlot>();
            _cachedTeachers = new List<Teacher>();
            _cachedWeeks = new List<Week>();
            _cachedClasses = new List<Class>();
            _schoolRowGroups = new List<SchoolRowGroup>();

            InitializeComponent();
            InitializeContextMenu();
        }

        private void InitializeContextMenu()
        {
            _contextMenu = new ContextMenuStrip();
            _contextMenu.Items.Add("Quản lý dữ liệu", null, ManageData_Click);
            dgvTimetable.ContextMenuStrip = _contextMenu;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("MainForm_Load: Starting...");
            await LoadComboBoxesAsync();
            ConfigureDataGridView();
            Debug.WriteLine("MainForm_Load: Completed.");
        }

        private void ConfigureDataGridView()
        {
            dgvTimetable.SuspendLayout();
            bool columnsNeedSetup = dgvTimetable.Columns.Count == 0 ||
                                    (_isTimetableMode && dgvTimetable.Columns.Count != 10) ||
                                    (!_isTimetableMode && dgvTimetable.Columns.Count != 6);

            if (columnsNeedSetup)
            {
                Debug.WriteLine("ConfigureDataGridView: Setting up columns...");
                dgvTimetable.Columns.Clear();
                dgvTimetable.Rows.Clear();

                if (_isTimetableMode)
                {
                    AddDataGridViewColumn("SchoolName", "Trường", 150);
                    AddDataGridViewColumn("Session", "Buổi", 80, true);
                    AddDataGridViewColumn("Slot", "Tiết", 100);

                    foreach (var day in _daysOfWeek)
                    {
                        AddDataGridViewColumn(day, day, 150, false, day == "CN" ? Color.FromArgb(255, 204, 204) : Color.Empty);
                    }

                    foreach (DataGridViewColumn column in dgvTimetable.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    // Set default headers based on today's week
                    if (_isTimetableMode)
                    {
                        var defaultStartDate = GetMondayOfWeek(DateTime.Today);
                        UpdateColumnHeaders(defaultStartDate);
                    }
                }
                else
                {
                    AddDataGridViewColumn("TeacherName", "Giáo viên", 150);
                    AddDataGridViewColumn("SchoolName", "Trường", 150);
                    AddDataGridViewColumn("Date", "Ngày", 100);
                    AddDataGridViewColumn("TimeSlot", "Tiết", 100);
                    AddDataGridViewColumn("Status", "Trạng thái", 100);
                    AddDataGridViewColumn("OffReason", "Lý do nghỉ", 200);
                }

                ConfigureDataGridViewProperties();
            }
            else
            {
                Debug.WriteLine("ConfigureDataGridView: Columns already set, clearing rows only...");
                dgvTimetable.Rows.Clear();
            }

            dgvTimetable.ResumeLayout();
        }

        private void AddDataGridViewColumn(string name, string headerText, int width, bool centerAlign = false, Color? backColor = null)
        {
            var column = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = headerText,
                Width = width
            };
            if (centerAlign)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (backColor.HasValue)
            {
                column.DefaultCellStyle.BackColor = backColor.Value;
            }
            dgvTimetable.Columns.Add(column);
        }

        private void ConfigureDataGridViewProperties()
        {
            dgvTimetable.AllowUserToAddRows = false;
            dgvTimetable.ReadOnly = true;
            dgvTimetable.RowHeadersVisible = false;
            dgvTimetable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvTimetable.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvTimetable.SelectionMode = DataGridViewSelectionMode.CellSelect;
            typeof(DataGridView)
       .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
       .SetValue(dgvTimetable, true, null);
        }

        private async Task DebounceLoadTimetableAsync(DateTime startDate)
        {
            _loadTimetableCts.Cancel();
            _loadTimetableCts = new CancellationTokenSource();
            var token = _loadTimetableCts.Token;

            try
            {
                await Task.Delay(300, token); // 300ms debounce delay
                await LoadTimetableAsync(startDate, token);
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("DebounceLoadTimetableAsync: Operation canceled due to new request.");
            }
        }

        private async Task LoadTimetableAsync(DateTime startDate, CancellationToken cancellationToken)
        {
            if (_isUpdating) return;
            _isUpdating = true;

            var stopwatch = Stopwatch.StartNew();
            try
            {
                Debug.WriteLine($"LoadTimetableAsync: Starting with startDate={startDate:dd/MM/yyyy}...");
                Cursor = Cursors.WaitCursor;
                dgvTimetable.Enabled = false;

                int? teacherId = (cmbTeacherFilter.SelectedItem is Teacher selectedTeacher && selectedTeacher.TeacherId != 0)
                                ? (int?)selectedTeacher.TeacherId
                                : null;
                int? schoolId = (cmbSchool.SelectedItem is School selectedSchool && selectedSchool.SchoolId != 0)
                                ? (int?)selectedSchool.SchoolId
                                : null;

                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    var selectedDate = dtpWeekStart.Value;
                    var selectedWeek = _cachedWeeks.OrderBy(w => Math.Abs((w.StartDate - selectedDate).TotalDays)).FirstOrDefault();
                    if (selectedWeek == null)
                    {
                        ShowWarning("Không tìm thấy tuần học phù hợp!");
                        return;
                    }
                    int weekId = selectedWeek.WeekId;

                    var endDate = startDate.AddDays(6);
                    var rowsToAdd = new List<DataGridViewRow>();
                    _schoolRowGroups.Clear();

                    if (!_cachedSchools.Any())
                    {
                        ShowWarning("Không có trường nào trong cơ sở dữ liệu.");
                        return;
                    }

                    if (!_cachedTimeSlots.Any())
                    {
                        await LoadTimeSlotsAsync();
                        if (!_cachedTimeSlots.Any())
                        {
                            ShowWarning("Không có tiết học nào trong cơ sở dữ liệu.");
                            return;
                        }
                    }

                    if (_isTimetableMode)
                    {
                        var schoolsToProcess = schoolId.HasValue
                            ? _cachedSchools.Where(s => s.SchoolId == schoolId.Value).ToList()
                            : _cachedSchools.Where(s => s.SchoolId != 0).ToList();

                        _totalSchools = schoolsToProcess.Count;
                        _totalPages = (int)Math.Ceiling((double)_totalSchools / _pageSize);

                        var pagedSchools = schoolsToProcess
                            .Skip((_currentPage - 1) * _pageSize)
                            .Take(_pageSize)
                            .ToList();

                        var schoolIds = pagedSchools.Select(s => s.SchoolId).ToList();
                        var schedulesQuery = context.Schedules
                            .AsNoTracking()
                            .Include(s => s.Teacher)
                            .Include(s => s.Class)
                            .Include(s => s.TeachingAssistant)
                            .Where(s => schoolIds.Contains(s.SchoolId) && s.WeekId == weekId);

                        if (teacherId.HasValue)
                        {
                            schedulesQuery = schedulesQuery.Where(s => s.TeacherId == teacherId.Value);
                        }

                        schedulesQuery = schedulesQuery.Where(s => s.ScheduleDate >= startDate && s.ScheduleDate <= endDate);

                        var allSchedules = await schedulesQuery
                            .OrderBy(s => s.ScheduleDate)
                            .ThenBy(s => s.TimeSlotId)
                            .ToListAsync(cancellationToken);

                        Debug.WriteLine($"LoadTimetableAsync: Fetched {allSchedules.Count} schedules in total.");

                        int schoolIndex = 1 + (_currentPage - 1) * _pageSize;
                        var morningSlots = _cachedTimeSlots.Where(ts => ts.StartTime.Hours < 12).OrderBy(ts => ts.StartTime).ToList();
                        var afternoonSlots = _cachedTimeSlots.Where(ts => ts.StartTime.Hours >= 12).OrderBy(ts => ts.StartTime).ToList();

                        foreach (var school in pagedSchools)
                        {
                            var schedules = allSchedules.Where(s => s.SchoolId == school.SchoolId).ToList();
                            Debug.WriteLine($"LoadTimetableAsync: School {school.SchoolName}, Schedules count={schedules.Count}");

                            var schoolRows = CreateRowsForSchool(school, schoolIndex++, schedules, morningSlots, afternoonSlots, startDate);
                            rowsToAdd.AddRange(schoolRows);
                            _schoolRowGroups.Add(new SchoolRowGroup { School = school });
                        }
                    }
                    else
                    {
                        rowsToAdd = await LoadAttendanceTableAsync(schoolId, weekId, teacherId, _currentPage, _pageSize, cancellationToken);
                    }

                    if (dgvTimetable.IsDisposed || !dgvTimetable.IsHandleCreated)
                    {
                        Debug.WriteLine("LoadTimetableAsync: DataGridView is disposed or handle not created.");
                        return;
                    }

                    Debug.WriteLine($"LoadTimetableAsync: Adding {rowsToAdd.Count} rows to DataGridView...");
                    dgvTimetable.SuspendLayout();
                    ConfigureDataGridView();
                    if (_isTimetableMode)
                    {
                        UpdateColumnHeaders(startDate); // Update headers before adding rows
                        dgvTimetable.Refresh(); // Force refresh to ensure headers are updated
                    }
                    dgvTimetable.Rows.Clear();
                    dgvTimetable.Rows.AddRange(rowsToAdd.ToArray());
                    lblPageInfo.Text = $"Trang {_currentPage} / {_totalPages}";
                    btnPreviousPage.Enabled = _currentPage > 1;
                    btnNextPage.Enabled = _currentPage < _totalPages;
                    dgvTimetable.ResumeLayout();
                    Debug.WriteLine($"LoadTimetableAsync: DataGridView updated with {dgvTimetable.Rows.Count} rows.");
                }
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("LoadTimetableAsync: Operation was canceled.");
                ShowWarning("Tải dữ liệu bị hủy do mất quá nhiều thời gian.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadTimetableAsync: Error - {ex.Message}\nStackTrace: {ex.StackTrace}");
                ShowError($"Lỗi khi tải lịch giảng dạy: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
                dgvTimetable.Enabled = true;
                _isUpdating = false;
                stopwatch.Stop();
                Debug.WriteLine($"LoadTimetableAsync: Completed in {stopwatch.ElapsedMilliseconds} ms");
            }
        }

        private List<DataGridViewRow> CreateRowsForSchool(School school, int schoolIndex, List<Schedule> schedules, List<TimeSlot> morningSlots, List<TimeSlot> afternoonSlots, DateTime startDate)
        {
            var rows = new List<DataGridViewRow>();

            var schoolRow = new DataGridViewRow();
            schoolRow.CreateCells(dgvTimetable);
            schoolRow.Cells[0].Value = $"{schoolIndex}. {school.SchoolName.ToUpper()}";
            schoolRow.Cells[0].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            schoolRow.DefaultCellStyle.BackColor = Color.LightBlue;
            schoolRow.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            rows.Add(schoolRow);

            var morningSessionRow = new DataGridViewRow();
            morningSessionRow.CreateCells(dgvTimetable);
            morningSessionRow.Cells[0].Value = "";
            morningSessionRow.Cells[1].Value = "Sáng";
            morningSessionRow.DefaultCellStyle.BackColor = Color.LightYellow;
            rows.Add(morningSessionRow);

            foreach (var timeSlot in morningSlots)
            {
                var slotRow = new DataGridViewRow();
                slotRow.CreateCells(dgvTimetable);
                slotRow.Cells[0].Value = "";
                slotRow.Cells[1].Value = "";
                string startTime = timeSlot.StartTime.ToString(@"hh\:mm");
                string endTime = timeSlot.EndTime.ToString(@"hh\:mm");
                slotRow.Cells[2].Value = $"{timeSlot.SlotName} ({startTime} - {endTime})";
                rows.Add(slotRow);
            }

            var afternoonSessionRow = new DataGridViewRow();
            afternoonSessionRow.CreateCells(dgvTimetable);
            afternoonSessionRow.Cells[0].Value = "";
            afternoonSessionRow.Cells[1].Value = "Chiều";
            afternoonSessionRow.DefaultCellStyle.BackColor = Color.LightYellow;
            rows.Add(afternoonSessionRow);

            foreach (var timeSlot in afternoonSlots)
            {
                var slotRow = new DataGridViewRow();
                slotRow.CreateCells(dgvTimetable);
                slotRow.Cells[0].Value = "";
                slotRow.Cells[1].Value = "";
                string startTime = timeSlot.StartTime.ToString(@"hh\:mm");
                string endTime = timeSlot.EndTime.ToString(@"hh\:mm");
                slotRow.Cells[2].Value = $"{timeSlot.SlotName} ({startTime} - {endTime})";
                rows.Add(slotRow);
            }

            foreach (var schedule in schedules)
            {
                var timeSlot = morningSlots.Concat(afternoonSlots).FirstOrDefault(ts => ts.TimeSlotId == schedule.TimeSlotId);
                if (timeSlot == null)
                    continue;

                int slotIndex = morningSlots.Contains(timeSlot)
                    ? morningSlots.IndexOf(timeSlot) + 2
                    : afternoonSlots.IndexOf(timeSlot) + morningSlots.Count + 3;

                if (slotIndex >= rows.Count)
                    continue;

                int dayIndex = (schedule.ScheduleDate - startDate).Days;
                if (dayIndex >= 0 && dayIndex < 7)
                {
                    string teachingAssistantInfo = schedule.TeachingAssistant != null
                        ? schedule.TeachingAssistant.TeachingAssistantCode ?? "N/A"
                        : "N/A";
                    rows[slotIndex].Cells[dayIndex + 3].Value = $"{schedule.Teacher?.TeacherName ?? "N/A"} - {schedule.Class?.ClassName ?? "N/A"} - {teachingAssistantInfo}";
                    rows[slotIndex].Cells[dayIndex + 3].Tag = schedule.ScheduleId;
                }
            }

            return rows;
        }

        private async Task<List<DataGridViewRow>> LoadAttendanceTableAsync(int? schoolId, int weekId, int? teacherId, int page, int pageSize, CancellationToken cancellationToken)
        {
            var rowsToAdd = new List<DataGridViewRow>();
            int schedulesPageSize = 50; // Giới hạn số lượng lịch mỗi trang

            // Tạo ánh xạ TimeSlot để tăng tốc độ tìm kiếm
            var timeSlotDict = _cachedTimeSlots.ToDictionary(ts => ts.TimeSlotId, ts => ts);

            try
            {
                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    var schoolsToProcess = schoolId.HasValue
                        ? _cachedSchools.Where(s => s.SchoolId == schoolId.Value).ToList()
                        : _cachedSchools.Where(s => s.SchoolId != 0).ToList();

                    // Tính tổng số lịch (Schedules) để phân trang
                    int totalSchedules = 0;
                    foreach (var school in schoolsToProcess)
                    {
                        var schedulesQuery = context.Schedules
                            .AsNoTracking()
                            .Where(s => s.SchoolId == school.SchoolId && s.WeekId == weekId);

                        if (teacherId.HasValue)
                        {
                            schedulesQuery = schedulesQuery.Where(s => s.TeacherId == teacherId.Value);
                        }

                        totalSchedules += await schedulesQuery.CountAsync(cancellationToken);
                    }

                    _totalPages = (int)Math.Ceiling((double)totalSchedules / schedulesPageSize);

                    int skip = (page - 1) * schedulesPageSize;
                    int remainingSchedules = totalSchedules - skip;
                    int schedulesToTake = Math.Min(schedulesPageSize, remainingSchedules);

                    if (schedulesToTake <= 0)
                    {
                        Debug.WriteLine("LoadAttendanceTableAsync: No schedules to display for the current page.");
                        return rowsToAdd;
                    }

                    int processedSchedules = 0;
                    int schoolIndex = 1;

                    foreach (var school in schoolsToProcess)
                    {
                        if (processedSchedules >= schedulesToTake + skip)
                        {
                            break; // Đã xử lý đủ số lượng lịch cho trang này
                        }

                        var schedulesQuery = context.Schedules
                            .AsNoTracking()
                            .Include(s => s.Teacher)
                            .Include(s => s.Class)
                            .Include(s => s.TeachingAssistant)
                            .Where(s => s.SchoolId == school.SchoolId && s.WeekId == weekId);

                        if (teacherId.HasValue)
                        {
                            schedulesQuery = schedulesQuery.Where(s => s.TeacherId == teacherId.Value);
                        }

                        // Áp dụng phân trang cho Schedules
                        var schedules = await schedulesQuery
                            .OrderBy(s => s.ScheduleDate)
                            .ThenBy(s => s.TimeSlotId)
                            .Skip(Math.Max(0, skip - processedSchedules))
                            .Take(Math.Max(0, schedulesToTake - processedSchedules))
                            .ToListAsync(cancellationToken);

                        processedSchedules += schedules.Count;

                        Debug.WriteLine($"LoadAttendanceTableAsync: School {school.SchoolName}, Schedules count={schedules?.Count ?? 0}");

                        if (schedules == null || !schedules.Any())
                        {
                            continue; // Bỏ qua trường không có lịch trong trang này
                        }

                        // Thêm hàng tiêu đề cho trường
                        var schoolRow = new DataGridViewRow();
                        schoolRow.CreateCells(dgvTimetable);
                        if (dgvTimetable.Columns.Count > 1) // Kiểm tra số lượng cột
                        {
                            schoolRow.Cells[1].Value = $"{schoolIndex++}. {school.SchoolName.ToUpper()}";
                            schoolRow.Cells[1].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                            schoolRow.DefaultCellStyle.BackColor = Color.LightBlue;
                            schoolRow.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            rowsToAdd.Add(schoolRow);
                            _schoolRowGroups.Add(new SchoolRowGroup { School = school });
                        }
                        else
                        {
                            Debug.WriteLine("LoadAttendanceTableAsync: Not enough columns in DataGridView to add school row.");
                            continue;
                        }

                        // Thêm hàng cho từng lịch
                        foreach (var session in schedules)
                        {
                            var row = new DataGridViewRow();
                            row.CreateCells(dgvTimetable);

                            // Kiểm tra số lượng cột trước khi truy cập
                            if (dgvTimetable.Columns.Count < 6)
                            {
                                Debug.WriteLine("LoadAttendanceTableAsync: Not enough columns in DataGridView to add schedule row.");
                                continue;
                            }

                            // Gán giá trị cho các cột (đồng bộ với cấu trúc cột trong ConfigureDataGridView)
                            row.Cells[0].Value = session.Teacher?.TeacherName ?? "N/A"; // Cột "Giáo viên"
                            row.Cells[1].Value = school.SchoolName; // Cột "Trường"
                            row.Cells[2].Value = session.ScheduleDate.ToString("dd/MM/yyyy"); // Cột "Ngày"
                            timeSlotDict.TryGetValue(session.TimeSlotId, out var timeSlot);
                            row.Cells[3].Value = timeSlot != null ? $"{timeSlot.SlotName} ({timeSlot.StartTime.ToString(@"hh\:mm")} - {timeSlot.EndTime.ToString(@"hh\:mm")})" : session.TimeSlotId.ToString(); // Cột "Tiết"
                            row.Cells[4].Value = session.Status ?? "Đang dạy"; // Cột "Trạng thái"
                            row.Cells[5].Value = session.OffReason ?? ""; // Cột "Lý do nghỉ"

                            // Lưu ScheduleId vào Tag để sử dụng sau này (ví dụ: khi xóa lịch)
                            row.Tag = session.ScheduleId;

                            rowsToAdd.Add(row);
                        }
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("LoadAttendanceTableAsync: Operation was canceled.");
                ShowWarning("Tải bảng chấm công bị hủy do mất quá nhiều thời gian.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadAttendanceTableAsync: Error - {ex.Message}\nStackTrace: {ex.StackTrace}");
                ShowError($"Lỗi khi tải bảng chấm công: {ex.Message}");
            }

            return rowsToAdd;
        }
        private async Task LoadTimeSlotsAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    _cachedTimeSlots = await context.TimeSlots
                        .AsNoTracking()
                        .OrderBy(ts => ts.StartTime)
                        .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _cachedTimeSlots = new List<TimeSlot>();
                ShowError($"Lỗi khi tải danh sách tiết học: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();
                Debug.WriteLine($"LoadTimeSlotsAsync: Completed in {stopwatch.ElapsedMilliseconds} ms");
            }
        }

        private async Task LoadComboBoxesAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    _cachedSchools = await context.Schools.OrderBy(s => s.SchoolName).ToListAsync();
                    _cachedWeeks = await context.Weeks.OrderBy(w => w.StartDate).ToListAsync();
                    _cachedTeachers = await context.Teachers.OrderBy(t => t.TeacherName).ToListAsync();
                    _cachedClasses = await context.Classes.OrderBy(c => c.ClassName).ToListAsync();

                    // Tải danh sách trường
                    var schoolList = new List<School> { new School { SchoolId = 0, SchoolName = "Tất cả" } };
                    schoolList.AddRange(_cachedSchools);
                    cmbSchool.DataSource = schoolList;
                    cmbSchool.DisplayMember = "SchoolName";
                    cmbSchool.ValueMember = "SchoolId";

                    // Tải danh sách giáo viên
                    var teacherList = new List<Teacher> { new Teacher { TeacherId = 0, TeacherName = "Tất cả" } };
                    teacherList.AddRange(_cachedTeachers);
                    cmbTeacherFilter.DataSource = teacherList;
                    cmbTeacherFilter.DisplayMember = "TeacherName";
                    cmbTeacherFilter.ValueMember = "TeacherId";

                    // Tải danh sách tuần
                    var weekList = new List<Week>(_cachedWeeks);
                    cmbWeek.DataSource = weekList;
                    cmbWeek.DisplayMember = "WeekName"; // Giả định Week có thuộc tính WeekName
                    cmbWeek.ValueMember = "WeekId";

                    // Chọn tuần hiện tại
                    var currentWeek = _cachedWeeks.OrderBy(w => Math.Abs((w.StartDate - DateTime.Today).TotalDays)).FirstOrDefault();
                    if (currentWeek != null)
                    {
                        cmbWeek.SelectedItem = currentWeek;
                        dtpWeekStart.Value = currentWeek.StartDate;
                        UpdateWeekLabels(currentWeek.StartDate);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi tải dữ liệu: {ex.Message}");
            }
            finally
            {
                stopwatch.Stop();
                Debug.WriteLine($"LoadComboBoxesAsync: Completed in {stopwatch.ElapsedMilliseconds} ms");
            }
        }
        private void UpdateWeekLabels(DateTime startDate)
        {
            var endDate = startDate.AddDays(6);
            lblWeekStart.Text = $"Ngày bắt đầu: {startDate:dd/MM/yyyy}";
            lblWeekEnd.Text = $"Ngày kết thúc: {endDate:dd/MM/yyyy}";
        }
        private async void cmbWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isUpdating) return;
            _isUpdating = true;

            try
            {
                if (cmbWeek.SelectedItem is Week selectedWeek)
                {
                    dtpWeekStart.Value = selectedWeek.StartDate;
                    UpdateWeekLabels(selectedWeek.StartDate);
                    _currentPage = 1;
                    await DebounceLoadTimetableAsync(selectedWeek.StartDate);
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi chọn tuần: {ex.Message}");
            }
            finally
            {
                _isUpdating = false;
            }
        }
        private async void dtpWeekStart_ValueChanged(object sender, EventArgs e)
        {
            if (_isUpdating) return;
            _isUpdating = true;

            try
            {
                var selectedDate = dtpWeekStart.Value;
                var startDate = GetMondayOfWeek(selectedDate);
                var selectedWeek = _cachedWeeks.OrderBy(w => Math.Abs((w.StartDate - startDate).TotalDays)).FirstOrDefault();
                if (selectedWeek != null)
                {
                    cmbWeek.SelectedItem = selectedWeek;
                    UpdateWeekLabels(selectedWeek.StartDate);
                    _currentPage = 1;
                    await DebounceLoadTimetableAsync(selectedWeek.StartDate);
                }
                else
                {
                    ShowWarning("Không tìm thấy tuần học phù hợp!");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi thay đổi ngày: {ex.Message}");
            }
            finally
            {
                _isUpdating = false;
            }
        }
        private DateTime GetMondayOfWeek(DateTime date)
        {
            int daysToSubtract = ((int)date.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;
            return date.AddDays(-daysToSubtract).Date;
        }

        private async void ManageData_Click(object sender, EventArgs e)
        {
            await OpenScheduleFormAsync();
        }

        private async Task OpenScheduleFormAsync()
        {
            if (_isUpdating) return;
            _isUpdating = true;

            try
            {
                var selectedSchool = cmbSchool.SelectedItem as School;
                int schoolId = selectedSchool?.SchoolId ?? 0;
                var form = new ScheduleForm(_contextOptions, schoolId);
                form.FormClosed += async (s, ev) =>
                {
                    using (var context = new SchoolTimetableContext(_contextOptions))
                    {
                        _cachedSchools = await context.Schools.ToListAsync();
                        _cachedWeeks = await context.Weeks.ToListAsync();
                        _cachedTimeSlots = await context.TimeSlots.ToListAsync();
                        _cachedClasses = await context.Classes.ToListAsync();
                        await LoadComboBoxesAsync();
                        var startDate = GetMondayOfWeek(dtpWeekStart.Value);
                        await DebounceLoadTimetableAsync(startDate);
                    }
                };

                form.ShowDialog();

            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi mở form quản lý: {ex.Message}");
            }
            finally
            {
                _isUpdating = false;
            }
        }

        private async void BtnManageSchedules_Click(object sender, EventArgs e)
        {
            if (_isUpdating) return;
            _isUpdating = true;

            try
            {
                var selectedSchool = cmbSchool.SelectedItem as School;
                if (selectedSchool == null || selectedSchool.SchoolId == 0)
                {
                    ShowWarning("Vui lòng chọn một trường cụ thể!");
                    return;
                }

                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    var form = new ScheduleForm(_contextOptions, selectedSchool.SchoolId);
                    form.FormClosed += async (s, ev) =>
                    {
                        using (var reloadContext = new SchoolTimetableContext(_contextOptions))
                        {
                            _cachedSchools = await reloadContext.Schools.ToListAsync();
                            _cachedWeeks = await reloadContext.Weeks.ToListAsync();
                            _cachedTimeSlots = await reloadContext.TimeSlots.ToListAsync();
                            _cachedClasses = await reloadContext.Classes.ToListAsync();
                        }

                        await LoadComboBoxesAsync();
                        await DebounceLoadTimetableAsync(DateTime.Today);
                    };

                    form.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi mở form quản lý: {ex.Message}");
            }
            finally
            {
                _isUpdating = false;
            }
        }

        private void UpdateColumnHeaders(DateTime startDate)
        {
            Debug.WriteLine($"UpdateColumnHeaders: Setting headers with startDate={startDate:dd/MM/yyyy}");
            if (dgvTimetable.Columns.Count < _daysOfWeek.Length + 3)
            {
                Debug.WriteLine("UpdateColumnHeaders: Not enough columns to update headers.");
                return;
            }
            for (int i = 0; i < _daysOfWeek.Length; i++)
            {
                var date = startDate.AddDays(i);
                dgvTimetable.Columns[i + 3].HeaderText = $"{_daysOfWeek[i]} {date:dd/MM}";
                Debug.WriteLine($"Header {i + 3} set to: {dgvTimetable.Columns[i + 3].HeaderText}");
            }
        }

        private async void cmbSchool_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentPage = 1;
            var startDate = GetMondayOfWeek(dtpWeekStart.Value);
            await DebounceLoadTimetableAsync(startDate);
        }

        private async void cmbTeacherFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentPage = 1;
            var startDate = GetMondayOfWeek(dtpWeekStart.Value);
            await DebounceLoadTimetableAsync(startDate);
        }
        // Trong class MainForm
        private async void btnPreviousPage_Click(object sender, EventArgs e)
        {
            if (_currentPage <= 1)
            {
                Debug.WriteLine("btnPreviousPage_Click: Already on the first page.");
                btnPreviousPage.Enabled = false;
                return;
            }

            try
            {
                _currentPage--;
                Debug.WriteLine($"btnPreviousPage_Click: Moving to page {_currentPage}/{_totalPages}");
                var startDate = GetMondayOfWeek(dtpWeekStart.Value);
                await DebounceLoadTimetableAsync(startDate);
                Debug.WriteLine($"btnPreviousPage_Click: Page {_currentPage} loaded successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"btnPreviousPage_Click: Error - {ex.Message}\nStackTrace: {ex.StackTrace}");
                ShowError($"Lỗi khi chuyển trang trước: {ex.Message}");
            }
        }

        private async void btnNextPage_Click(object sender, EventArgs e)
        {
            if (_currentPage >= _totalPages)
            {
                Debug.WriteLine("btnNextPage_Click: Already on the last page.");
                btnNextPage.Enabled = false;
                return;
            }

            try
            {
                _currentPage++;
                Debug.WriteLine($"btnNextPage_Click: Moving to page {_currentPage}/{_totalPages}");
                var startDate = GetMondayOfWeek(dtpWeekStart.Value);
                await DebounceLoadTimetableAsync(startDate);
                Debug.WriteLine($"btnNextPage_Click: Page {_currentPage} loaded successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"btnNextPage_Click: Error - {ex.Message}\nStackTrace: {ex.StackTrace}");
                ShowError($"Lỗi khi chuyển trang tiếp: {ex.Message}");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnToggleView_Click(object sender, EventArgs e)
        {
            _isTimetableMode = !_isTimetableMode;
            btnToggleView.Text = _isTimetableMode ? "📋Xem Bảng Chấm Công" : "🗓️Xem Thời Khóa Biểu";
            _currentPage = 1;
            ConfigureDataGridView();
            var startDate = GetMondayOfWeek(dtpWeekStart.Value);
            await DebounceLoadTimetableAsync(startDate);
        }
        private async void quảnLýBảngCôngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await OpenBangChamCongFormAsync();
        }
        private async Task OpenBangChamCongFormAsync()
        {
            try
            {
                using (var context = new SchoolTimetableContext(_contextOptions))
                using (var form = new BangChamCongForm(context)) // Truyền context thay vì options
                {
                    var result = form.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        await Task.WhenAll(
                            LoadComboBoxesAsync()

                        ).ConfigureAwait(true);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"OpenBangChamCongFormAsync: {ex.Message}");
                MessageBox.Show("Đã xảy ra lỗi khi mở Bảng Chấm Công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowWarning(string message)
        {
            if (!_isUpdating)
                MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowInfo(string message)
        {
            if (!_isUpdating)
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message)
        {
            if (!_isUpdating)
                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            await OpenScheduleFormAsync1();
        }

        private async Task OpenScheduleFormAsync1()
        {
            if (_isUpdating) return;
            _isUpdating = true;

            try
            {
                var selectedSchool = cmbSchool.SelectedItem as School;
                int schoolId = selectedSchool?.SchoolId ?? 0;
                var form = new AddScheduleForm(_contextOptions, schoolId);
                form.StartPosition = FormStartPosition.CenterScreen;
                form.FormClosed += async (s, ev) =>
                {
                    using (var context = new SchoolTimetableContext(_contextOptions))
                    {
                        _cachedSchools = await context.Schools.OrderBy(s1 => s1.SchoolName).ToListAsync();
                        _cachedWeeks = await context.Weeks.OrderBy(w => w.StartDate).ToListAsync();
                        _cachedTimeSlots = await context.TimeSlots.OrderBy(ts => ts.StartTime).ToListAsync();
                        _cachedClasses = await context.Classes.OrderBy(c => c.ClassName).ToListAsync();
                        await LoadComboBoxesAsync();
                        var startDate = GetMondayOfWeek(dtpWeekStart.Value);
                        await DebounceLoadTimetableAsync(startDate);
                    }

                };
                form.ShowDialog();

            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi mở form quản lý: {ex.Message}");
            }
            finally
            {
                _isUpdating = false;
            }
        }

        private async void BtnManageSchedules1_Click(object sender, EventArgs e)
        {
            if (_isUpdating) return;
            _isUpdating = true;

            try
            {
                var selectedSchool = cmbSchool.SelectedItem as School;
                if (selectedSchool == null || selectedSchool.SchoolId == 0)
                {
                    ShowWarning("Vui lòng chọn một trường cụ thể!");
                    return;
                }

                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    var form = new AddScheduleForm(_contextOptions, selectedSchool.SchoolId);
                    form.FormClosed += async (s, ev) =>
                    {
                        using (var reloadContext = new SchoolTimetableContext(_contextOptions))
                        {
                            _cachedSchools = await reloadContext.Schools.OrderBy(s1 => s1.SchoolName).ToListAsync();
                            _cachedWeeks = await reloadContext.Weeks.OrderBy(w => w.StartDate).ToListAsync();
                            _cachedTimeSlots = await reloadContext.TimeSlots.OrderBy(ts => ts.StartTime).ToListAsync();
                            _cachedClasses = await reloadContext.Classes.OrderBy(c => c.ClassName).ToListAsync();
                        }

                        await LoadComboBoxesAsync();
                        await DebounceLoadTimetableAsync(DateTime.Today);
                    };

                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi mở form quản lý: {ex.Message}");
            }
            finally
            {
                _isUpdating = false;
            }
        }
        private async void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (var package = new ExcelPackage(new FileInfo(openFileDialog.FileName)))
                        {
                            if (package.Workbook.Worksheets.Count < 2)
                            {
                                ShowError("File Excel không có đủ sheet. Cần ít nhất 2 sheet.");
                                return;
                            }

                            using (var context = new SchoolTimetableContext(_contextOptions))
                            {
                                using (var transaction = await context.Database.BeginTransactionAsync())
                                {
                                    try
                                    {
                                        // Ghi log vào file
                                        string logFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "import_log.txt");
                                        using (StreamWriter logWriter = new StreamWriter(logFile, true))
                                        {
                                            logWriter.WriteLine($"[{DateTime.Now}] Bắt đầu import file: {openFileDialog.FileName}");

                                            for (int sheetIndex = 1; sheetIndex < package.Workbook.Worksheets.Count; sheetIndex++)
                                            {
                                                var worksheet = package.Workbook.Worksheets[sheetIndex];
                                                int maxRows = worksheet.Dimension?.Rows ?? 0;
                                                logWriter.WriteLine($"[{DateTime.Now}] Processing Sheet {sheetIndex + 1} ({worksheet.Name}), Total Rows: {maxRows}");

                                                // Tìm dòng chứa "THE SCHEDULE"
                                                int scheduleStartRow = 1;
                                                while (scheduleStartRow <= maxRows)
                                                {
                                                    string cellValue = worksheet.Cells[scheduleStartRow, 1].Text?.Trim().ToUpperInvariant() ?? "";
                                                    logWriter.WriteLine($"[{DateTime.Now}] Row {scheduleStartRow}, Cell A: '{cellValue}'");
                                                    if (cellValue.Contains("THE SCHEDULE"))
                                                    {
                                                        scheduleStartRow++;
                                                        break;
                                                    }
                                                    scheduleStartRow++;
                                                }

                                                if (scheduleStartRow > maxRows)
                                                {
                                                    logWriter.WriteLine($"[{DateTime.Now}] Sheet {sheetIndex + 1} does not contain 'THE SCHEDULE' section. Skipping...");
                                                    continue;
                                                }

                                                // Xử lý tuần
                                                string weekCellValue = worksheet.Cells[scheduleStartRow, 1].Text?.Trim() ?? "";
                                                logWriter.WriteLine($"[{DateTime.Now}] Row {scheduleStartRow}, Week Cell: '{weekCellValue}'");

                                                var weekInfo = ParseWeek(weekCellValue, logWriter);
                                                if (!weekInfo.HasValue)
                                                {
                                                    logWriter.WriteLine($"[{DateTime.Now}] Invalid week format. Skipping sheet...");
                                                    continue;
                                                }

                                                var (weekName, startDate, endDate) = weekInfo.Value;
                                                var week = await context.Weeks.FirstOrDefaultAsync(w => w.WeekName == weekName);
                                                if (week == null)
                                                {
                                                    week = new Week
                                                    {
                                                        WeekName = weekName,
                                                        StartDate = startDate,
                                                        EndDate = endDate
                                                    };
                                                    context.Weeks.Add(week);
                                                    await context.SaveChangesAsync();
                                                    logWriter.WriteLine($"[{DateTime.Now}] Added Week: {week.WeekName}, Start: {week.StartDate:yyyy-MM-dd}, End: {week.EndDate:yyyy-MM-dd}");
                                                }

                                                // Bỏ qua hàng tiêu đề "TRƯỜNG (SCHOOL)"
                                                scheduleStartRow += 2;

                                                // Xử lý danh sách trường và dữ liệu lịch
                                                while (scheduleStartRow <= maxRows)
                                                {
                                                    string schoolName = worksheet.Cells[scheduleStartRow, 1].Text?.Trim() ?? "";
                                                    logWriter.WriteLine($"[{DateTime.Now}] Row {scheduleStartRow}, School Name: '{schoolName}'");

                                                    // Kiểm tra tên trường (bắt đầu bằng số và dấu chấm, ví dụ: "1.VĂN GIANG")
                                                    if (Regex.IsMatch(schoolName, @"^\d+\.\w+"))
                                                    {
                                                        schoolName = schoolName.Substring(schoolName.IndexOf('.') + 1).Trim();
                                                        var school = await context.Schools.FirstOrDefaultAsync(s => s.SchoolName == schoolName);
                                                        if (school == null)
                                                        {
                                                            school = new School
                                                            {
                                                                SchoolName = schoolName,
                                                                SchoolCode = schoolName.Substring(0, Math.Min(3, schoolName.Length)).ToUpper()
                                                            };
                                                            context.Schools.Add(school);
                                                            await context.SaveChangesAsync();
                                                            logWriter.WriteLine($"[{DateTime.Now}] Added School: {school.SchoolName}, Code: {school.SchoolCode}");
                                                        }

                                                        // Xử lý dữ liệu lịch sau tên trường
                                                        scheduleStartRow++;
                                                        while (scheduleStartRow <= maxRows && !Regex.IsMatch(worksheet.Cells[scheduleStartRow, 1].Text?.Trim() ?? "", @"^\d+\.\w+"))
                                                        {
                                                            if (worksheet.Cells[scheduleStartRow, 1].Text?.Trim() == "" &&
                                                                worksheet.Cells[scheduleStartRow, 2].Text?.Trim() == "" &&
                                                                worksheet.Cells[scheduleStartRow, 3].Text?.Trim() == "")
                                                            {
                                                                scheduleStartRow++;
                                                                continue;
                                                            }

                                                            // Xử lý thời gian
                                                            string timeSlotText = worksheet.Cells[scheduleStartRow, 2].Text?.Trim() ?? "";
                                                            var timeSlotInfo = ParseTimeSlot(timeSlotText, logWriter);
                                                            if (!timeSlotInfo.HasValue)
                                                            {
                                                                logWriter.WriteLine($"[{DateTime.Now}] Invalid time slot in row {scheduleStartRow}: '{timeSlotText}'. Skipping...");
                                                                scheduleStartRow++;
                                                                continue;
                                                            }

                                                            var (slotName, startTime, endTime) = timeSlotInfo.Value;
                                                            var timeSlot = await context.TimeSlots.FirstOrDefaultAsync(ts => ts.SlotName == slotName && ts.StartTime == startTime && ts.EndTime == endTime);
                                                            if (timeSlot == null)
                                                            {
                                                                timeSlot = new TimeSlot
                                                                {
                                                                    SlotName = slotName,
                                                                    StartTime = startTime,
                                                                    EndTime = endTime
                                                                };
                                                                context.TimeSlots.Add(timeSlot);
                                                                await context.SaveChangesAsync();
                                                                logWriter.WriteLine($"[{DateTime.Now}] Added TimeSlot: {timeSlot.SlotName}, Start: {startTime}, End: {endTime}");
                                                            }

                                                            // Xử lý giáo viên và lớp cho từng ngày
                                                            for (int day = 1; day <= 7; day++)
                                                            {
                                                                int col = 4 + (day - 1) * 3;
                                                                string teacherName = worksheet.Cells[scheduleStartRow, col].Text?.Trim() ?? "";
                                                                string className = worksheet.Cells[scheduleStartRow, col + 2].Text?.Trim() ?? "";
                                                                logWriter.WriteLine($"[{DateTime.Now}] Day {day} - Teacher: '{teacherName}', Class: '{className}'");

                                                                if (!string.IsNullOrEmpty(teacherName) && teacherName != "OFF")
                                                                {
                                                                    var teacher = await context.Teachers.FirstOrDefaultAsync(t => t.TeacherName == teacherName);
                                                                    if (teacher == null)
                                                                    {
                                                                        teacher = new Teacher
                                                                        {
                                                                            TeacherName = teacherName,
                                                                            SchoolId = school.SchoolId,
                                                                            TeacherCode = "GV" + Guid.NewGuid().ToString().Substring(0, 8)
                                                                        };
                                                                        context.Teachers.Add(teacher);
                                                                        await context.SaveChangesAsync();
                                                                        logWriter.WriteLine($"[{DateTime.Now}] Added Teacher: {teacher.TeacherName}, Code: {teacher.TeacherCode}");
                                                                    }

                                                                    Class classEntity = null;
                                                                    if (!string.IsNullOrEmpty(className))
                                                                    {
                                                                        classEntity = await context.Classes.FirstOrDefaultAsync(c => c.ClassName == className && c.SchoolId == school.SchoolId);
                                                                        if (classEntity == null)
                                                                        {
                                                                            classEntity = new Class
                                                                            {
                                                                                ClassName = className,
                                                                                SchoolId = school.SchoolId
                                                                            };
                                                                            context.Classes.Add(classEntity);
                                                                            await context.SaveChangesAsync();
                                                                            logWriter.WriteLine($"[{DateTime.Now}] Added Class: {classEntity.ClassName}");
                                                                        }
                                                                    }

                                                                    DateTime scheduleDate = week.StartDate.AddDays(day - 1);
                                                                    var schedule = new Schedule
                                                                    {
                                                                        TeacherId = teacher.TeacherId,
                                                                        SchoolId = school.SchoolId,
                                                                        WeekId = week.WeekId,
                                                                        TimeSlotId = timeSlot.TimeSlotId,
                                                                        ScheduleDate = scheduleDate,
                                                                        ClassId = classEntity?.ClassId,
                                                                        Status = "Đang dạy"
                                                                    };
                                                                    context.Schedules.Add(schedule);
                                                                    logWriter.WriteLine($"[{DateTime.Now}] Added Schedule: Date {scheduleDate:yyyy-MM-dd}, Teacher: {teacher.TeacherName}, Class: {classEntity?.ClassName}");
                                                                }
                                                            }

                                                            scheduleStartRow++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        scheduleStartRow++;
                                                    }
                                                }
                                            }

                                            logWriter.WriteLine($"[{DateTime.Now}] Hoàn tất import file: {openFileDialog.FileName}");
                                        }

                                        await context.SaveChangesAsync();
                                        await transaction.CommitAsync();
                                        ShowInfo("Import dữ liệu thành công!");
                                    }
                                    catch (DbUpdateException ex)
                                    {
                                        await transaction.RollbackAsync();
                                        ShowError($"Lỗi khi cập nhật cơ sở dữ liệu: {ex.InnerException?.Message ?? ex.Message}\nChi tiết: {ex.StackTrace}");
                                    }
                                    catch (Exception ex)
                                    {
                                        await transaction.RollbackAsync();
                                        ShowError($"Lỗi khi import dữ liệu: {ex.Message}\nChi tiết: {ex.StackTrace}");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowError($"Lỗi khi mở file Excel: {ex.Message}\nChi tiết: {ex.StackTrace}");
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private (string WeekName, DateTime StartDate, DateTime EndDate)? ParseWeek(string weekCellValue, StreamWriter logWriter)
        {
            if (string.IsNullOrEmpty(weekCellValue))
                return null;

            // Regex để phân tích: "Week X: from DD/MM to DD/MM/YYYY"
            var match = Regex.Match(weekCellValue, @"Week (\d+): from (\d{2}/\d{2}) to (\d{2}/\d{2}/\d{4})");
            if (match.Success)
            {
                string weekNumber = match.Groups[1].Value;
                string startDateStr = match.Groups[2].Value + "/2025"; // Giả sử năm 2025
                string endDateStr = match.Groups[3].Value;

                if (DateTime.TryParseExact(startDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate) &&
                    DateTime.TryParseExact(endDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
                {
                    string weekName = $"T{weekNumber}";
                    logWriter.WriteLine($"[{DateTime.Now}] Parsed Week: {weekName}, Start: {startDate:yyyy-MM-dd}, End: {endDate:yyyy-MM-dd}");
                    return (weekName, startDate, endDate);
                }
            }

            logWriter.WriteLine($"[{DateTime.Now}] Invalid week format: '{weekCellValue}'. Skipping...");
            return null;
        }

        private (string SlotName, TimeSpan StartTime, TimeSpan EndTime)? ParseTimeSlot(string timeSlotText, StreamWriter logWriter)
        {
            if (string.IsNullOrEmpty(timeSlotText))
                return null;

            // Regex để phân tích: "HH:mm-HH:mm"
            var match = Regex.Match(timeSlotText, @"(\d{1,2}:\d{2})-(\d{1,2}:\d{2})");
            if (match.Success)
            {
                string startTimeStr = match.Groups[1].Value;
                string endTimeStr = match.Groups[2].Value;

                if (TimeSpan.TryParse(startTimeStr, out TimeSpan startTime) &&
                    TimeSpan.TryParse(endTimeStr, out TimeSpan endTime))
                {
                    string slotName = $"{startTime:hh\\:mm}-{endTime:hh\\:mm}";
                    return (slotName, startTime, endTime);
                }
            }

            logWriter.WriteLine($"[{DateTime.Now}] Invalid time slot format: '{timeSlotText}'");
            return null;
        }
    } 
}