using Microsoft.EntityFrameworkCore;
using SchoolTimetableWinForm.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolTimetableWinForm
{
    public partial class ScheduleForm : Form
    {
        private readonly DbContextOptions<SchoolTimetableContext> _contextOptions;
        private readonly int _schoolId;
        private int? _scheduleId;
        private SchoolTimetableContext _context;

        public ScheduleForm(DbContextOptions<SchoolTimetableContext> contextOptions)
        {
            _contextOptions = contextOptions ?? throw new ArgumentNullException(nameof(contextOptions));
            _schoolId = 0; // Allow managing all schools
            InitializeComponent();
            InitializeData();
        }

        public ScheduleForm(DbContextOptions<SchoolTimetableContext> contextOptions, int schoolId)
        {
            _contextOptions = contextOptions ?? throw new ArgumentNullException(nameof(contextOptions));
            _schoolId = schoolId;
            InitializeComponent();
            InitializeData();
        }

        public ScheduleForm(DbContextOptions<SchoolTimetableContext> contextOptions, int schoolId, int? scheduleId)
        {
            _contextOptions = contextOptions ?? throw new ArgumentNullException(nameof(contextOptions));
            _schoolId = schoolId;
            _scheduleId = scheduleId;
            InitializeComponent();
            InitializeData();
        }

        private async void InitializeData()
        {
            _context = new SchoolTimetableContext(_contextOptions);
            await LoadSchoolsAsync();
            await LoadWeeksAsync();
            await LoadTimeSlotsAsync();
            await LoadClassesAsync();
        }

        private async void ManagementForm_Load(object sender, EventArgs e)
        {
            await LoadSchoolsAsync();
            await LoadWeeksAsync();
            await LoadTimeSlotsAsync();
            await LoadClassesAsync();
        }

        #region School Management
        private async Task LoadSchoolsAsync()
        {
            try
            {
                var schools = await _context.Schools
                    .AsNoTracking()
                    .OrderBy(s => s.SchoolName)
                    .ToListAsync();
                dgvSchool.DataSource = schools;
                cmbSchool.DataSource = schools;
                cmbSchool.DisplayMember = "SchoolName";
                cmbSchool.ValueMember = "SchoolId";
                ClearSchoolInputs();
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi tải danh sách trường: {ex.Message}");
            }
        }

        private void DgvSchool_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSchool.SelectedRows.Count > 0)
            {
                var selectedSchool = dgvSchool.SelectedRows[0].DataBoundItem as School;
                if (selectedSchool != null)
                {
                    txtSchoolName.Text = selectedSchool.SchoolName;
                }
            }
        }

        private async void BtnAddSchool_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSchoolName.Text))
                {
                    ShowWarning("Vui lòng nhập tên trường!");
                    return;
                }

                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    var school = new School
                    {
                        SchoolName = txtSchoolName.Text.Trim()
                    };

                    if (await context.Schools.AnyAsync(s => s.SchoolName == school.SchoolName))
                    {
                        ShowWarning("Trường đã tồn tại!");
                        return;
                    }

                    context.Schools.Add(school);
                    await context.SaveChangesAsync();
                    ShowInfo("Thêm trường thành công!");
                    await LoadSchoolsAsync();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi thêm trường: {ex.Message}");
            }
        }

        private async void BtnUpdateSchool_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSchool.SelectedRows.Count == 0)
                {
                    ShowWarning("Vui lòng chọn một trường để sửa!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSchoolName.Text))
                {
                    ShowWarning("Vui lòng nhập tên trường!");
                    return;
                }

                var selectedSchool = dgvSchool.SelectedRows[0].DataBoundItem as School;
                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    var school = await context.Schools.FindAsync(selectedSchool.SchoolId);
                    if (school == null)
                    {
                        ShowWarning("Trường không tồn tại!");
                        return;
                    }

                    school.SchoolName = txtSchoolName.Text.Trim();
                    if (await context.Schools.AnyAsync(s => s.SchoolName == school.SchoolName && s.SchoolId != school.SchoolId))
                    {
                        ShowWarning("Tên trường đã tồn tại!");
                        return;
                    }

                    await context.SaveChangesAsync();
                    ShowInfo("Cập nhật trường thành công!");
                    await LoadSchoolsAsync();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi cập nhật trường: {ex.Message}");
            }
        }

        private async void BtnDeleteSchool_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSchool.SelectedRows.Count == 0)
                {
                    ShowWarning("Vui lòng chọn một trường để xóa!");
                    return;
                }

                var selectedSchool = dgvSchool.SelectedRows[0].DataBoundItem as School;
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa trường '{selectedSchool.SchoolName}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    var school = await context.Schools
                        .Include(s => s.Classes)
                        .FirstOrDefaultAsync(s => s.SchoolId == selectedSchool.SchoolId);
                    if (school == null)
                    {
                        ShowWarning("Trường không tồn tại!");
                        return;
                    }

                    if (school.Classes.Any())
                    {
                        ShowWarning("Không thể xóa trường vì có lớp đang liên kết!");
                        return;
                    }

                    context.Schools.Remove(school);
                    await context.SaveChangesAsync();
                    ShowInfo("Xóa trường thành công!");
                    await LoadSchoolsAsync();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi xóa trường: {ex.Message}");
            }
        }

        private void ClearSchoolInputs()
        {
            txtSchoolName.Clear();
            dgvSchool.ClearSelection();
        }
        #endregion

        #region Week Management
        private async Task LoadWeeksAsync()
        {
            try
            {
                var weeks = await _context.Weeks
                    .AsNoTracking()
                    .OrderBy(w => w.StartDate)
                    .ToListAsync();
                dgvWeek.DataSource = weeks;
                ClearWeekInputs();
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi tải danh sách tuần: {ex.Message}");
            }
        }

        private void DgvWeek_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvWeek.SelectedRows.Count > 0)
            {
                var selectedWeek = dgvWeek.SelectedRows[0].DataBoundItem as Week;
                if (selectedWeek != null)
                {
                    dtpWeekStart.Value = selectedWeek.StartDate;
                }
            }
        }

        private async void BtnAddWeek_Click(object sender, EventArgs e)
        {
            try
            {
                var startDate = dtpWeekStart.Value.Date;
                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    if (await context.Weeks.AnyAsync(w => w.StartDate == startDate))
                    {
                        ShowWarning("Tuần với ngày bắt đầu này đã tồn tại!");
                        return;
                    }

                    var week = new Week
                    {
                        StartDate = startDate
                    };

                    context.Weeks.Add(week);
                    await context.SaveChangesAsync();
                    ShowInfo("Thêm tuần thành công!");
                    await LoadWeeksAsync();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi thêm tuần: {ex.Message}");
            }
        }

        private async void BtnUpdateWeek_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvWeek.SelectedRows.Count == 0)
                {
                    ShowWarning("Vui lòng chọn một tuần để sửa!");
                    return;
                }

                var selectedWeek = dgvWeek.SelectedRows[0].DataBoundItem as Week;
                var newStartDate = dtpWeekStart.Value.Date;
                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    var week = await context.Weeks.FindAsync(selectedWeek.WeekId);
                    if (week == null)
                    {
                        ShowWarning("Tuần không tồn tại!");
                        return;
                    }

                    if (await context.Weeks.AnyAsync(w => w.StartDate == newStartDate && w.WeekId != week.WeekId))
                    {
                        ShowWarning("Tuần với ngày bắt đầu này đã tồn tại!");
                        return;
                    }

                    week.StartDate = newStartDate;
                    await context.SaveChangesAsync();
                    ShowInfo("Cập nhật tuần thành công!");
                    await LoadWeeksAsync();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi cập nhật tuần: {ex.Message}");
            }
        }

        private async void BtnDeleteWeek_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvWeek.SelectedRows.Count == 0)
                {
                    ShowWarning("Vui lòng chọn một tuần để xóa!");
                    return;
                }

                var selectedWeek = dgvWeek.SelectedRows[0].DataBoundItem as Week;
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa tuần bắt đầu từ {selectedWeek.StartDate:dd/MM/yyyy}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    var week = await context.Weeks
                        .Include(w => w.Schedules)
                        .FirstOrDefaultAsync(w => w.WeekId == selectedWeek.WeekId);
                    if (week == null)
                    {
                        ShowWarning("Tuần không tồn tại!");
                        return;
                    }

                    if (week.Schedules.Any())
                    {
                        ShowWarning("Không thể xóa tuần vì có lịch giảng dạy liên kết!");
                        return;
                    }

                    context.Weeks.Remove(week);
                    await context.SaveChangesAsync();
                    ShowInfo("Xóa tuần thành công!");
                    await LoadWeeksAsync();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi xóa tuần: {ex.Message}");
            }
        }

        private void ClearWeekInputs()
        {
            dtpWeekStart.Value = DateTime.Today;
            dgvWeek.ClearSelection();
        }
        #endregion

        #region TimeSlot Management
        private async Task LoadTimeSlotsAsync()
        {
            try
            {
                var timeSlots = await _context.TimeSlots
                    .AsNoTracking()
                    .OrderBy(ts => ts.StartTime)
                    .ToListAsync();
                dgvTimeSlot.DataSource = timeSlots;
                ClearTimeSlotInputs();
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi tải danh sách tiết: {ex.Message}");
            }
        }

        private void DgvTimeSlot_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTimeSlot.SelectedRows.Count > 0)
            {
                var selectedTimeSlot = dgvTimeSlot.SelectedRows[0].DataBoundItem as TimeSlot;
                if (selectedTimeSlot != null)
                {
                    txtSlotName.Text = selectedTimeSlot.SlotName;
                    dtpStartTime.Value = DateTime.Today.Add(selectedTimeSlot.StartTime);
                    dtpEndTime.Value = DateTime.Today.Add(selectedTimeSlot.EndTime);
                }
            }
        }

        private async void BtnAddTimeSlot_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSlotName.Text))
                {
                    ShowWarning("Vui lòng nhập tên tiết!");
                    return;
                }

                var startTime = dtpStartTime.Value.TimeOfDay;
                var endTime = dtpEndTime.Value.TimeOfDay;
                if (endTime <= startTime)
                {
                    ShowWarning("Thời gian kết thúc phải lớn hơn thời gian bắt đầu!");
                    return;
                }

                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    var timeSlot = new TimeSlot
                    {
                        SlotName = txtSlotName.Text.Trim(),
                        StartTime = startTime,
                        EndTime = endTime
                    };

                    if (await context.TimeSlots.AnyAsync(ts => ts.SlotName == timeSlot.SlotName))
                    {
                        ShowWarning("Tên tiết đã tồn tại!");
                        return;
                    }

                    context.TimeSlots.Add(timeSlot);
                    await context.SaveChangesAsync();
                    ShowInfo("Thêm tiết thành công!");
                    await LoadTimeSlotsAsync();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi thêm tiết: {ex.Message}");
            }
        }

        private async void BtnUpdateTimeSlot_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTimeSlot.SelectedRows.Count == 0)
                {
                    ShowWarning("Vui lòng chọn một tiết để sửa!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSlotName.Text))
                {
                    ShowWarning("Vui lòng nhập tên tiết!");
                    return;
                }

                var startTime = dtpStartTime.Value.TimeOfDay;
                var endTime = dtpEndTime.Value.TimeOfDay;
                if (endTime <= startTime)
                {
                    ShowWarning("Thời gian kết thúc phải lớn hơn thời gian bắt đầu!");
                    return;
                }

                var selectedTimeSlot = dgvTimeSlot.SelectedRows[0].DataBoundItem as TimeSlot;
                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    var timeSlot = await context.TimeSlots.FindAsync(selectedTimeSlot.TimeSlotId);
                    if (timeSlot == null)
                    {
                        ShowWarning("Tiết không tồn tại!");
                        return;
                    }

                    timeSlot.SlotName = txtSlotName.Text.Trim();
                    timeSlot.StartTime = startTime;
                    timeSlot.EndTime = endTime;

                    if (await context.TimeSlots.AnyAsync(ts => ts.SlotName == timeSlot.SlotName && ts.TimeSlotId != timeSlot.TimeSlotId))
                    {
                        ShowWarning("Tên tiết đã tồn tại!");
                        return;
                    }

                    await context.SaveChangesAsync();
                    ShowInfo("Cập nhật tiết thành công!");
                    await LoadTimeSlotsAsync();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi cập nhật tiết: {ex.Message}");
            }
        }

        private async void BtnDeleteTimeSlot_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTimeSlot.SelectedRows.Count == 0)
                {
                    ShowWarning("Vui lòng chọn một tiết để xóa!");
                    return;
                }

                var selectedTimeSlot = dgvTimeSlot.SelectedRows[0].DataBoundItem as TimeSlot;
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa tiết '{selectedTimeSlot.SlotName}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    var timeSlot = await context.TimeSlots
                        .Include(ts => ts.Schedules)
                        .FirstOrDefaultAsync(ts => ts.TimeSlotId == selectedTimeSlot.TimeSlotId);
                    if (timeSlot == null)
                    {
                        ShowWarning("Tiết không tồn tại!");
                        return;
                    }

                    if (timeSlot.Schedules.Any())
                    {
                        ShowWarning("Không thể xóa tiết vì có lịch giảng dạy liên kết!");
                        return;
                    }

                    context.TimeSlots.Remove(timeSlot);
                    await context.SaveChangesAsync();
                    ShowInfo("Xóa tiết thành công!");
                    await LoadTimeSlotsAsync();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi xóa tiết: {ex.Message}");
            }
        }

        private void ClearTimeSlotInputs()
        {
            txtSlotName.Clear();
            dtpStartTime.Value = DateTime.Today.Add(new TimeSpan(8, 0, 0));
            dtpEndTime.Value = DateTime.Today.Add(new TimeSpan(9, 0, 0));
            dgvTimeSlot.ClearSelection();
        }
        #endregion

        private async Task LoadClassesAsync()
        {
            try
            {
                IQueryable<Class> query = _context.Classes
                    .AsNoTracking()
                    .Include(c => c.School);

                if (_schoolId != 0)
                {
                    query = query.Where(c => c.SchoolId == _schoolId);
                }

                var classes = await query
                    .OrderBy(c => c.ClassName)
                    .ToListAsync();

                dgvClass.DataSource = classes;
                ClearClassInputs();
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi tải danh sách lớp: {ex.Message}");
            }
        }

        private void DgvClass_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvClass.SelectedRows.Count > 0)
            {
                var selectedClass = dgvClass.SelectedRows[0].DataBoundItem as Class;
                if (selectedClass != null)
                {
                    txtClassName.Text = selectedClass.ClassName;
                    cmbSchool.SelectedValue = selectedClass.SchoolId;
                }
            }
        }

        private async void BtnAddClass_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtClassName.Text))
                {
                    ShowWarning("Vui lòng nhập tên lớp!");
                    return;
                }

                if (cmbSchool.SelectedValue == null)
                {
                    ShowWarning("Vui lòng chọn trường!");
                    return;
                }

                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    var classObj = new Class
                    {
                        ClassName = txtClassName.Text.Trim(),
                        SchoolId = (int)cmbSchool.SelectedValue
                    };

                    if (await context.Classes.AnyAsync(c => c.ClassName == classObj.ClassName && c.SchoolId == classObj.SchoolId))
                    {
                        ShowWarning("Lớp đã tồn tại trong trường này!");
                        return;
                    }

                    context.Classes.Add(classObj);
                    await context.SaveChangesAsync();
                    ShowInfo("Thêm lớp thành công!");
                    await LoadClassesAsync();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi thêm lớp: {ex.Message}");
            }
        }

        private async void BtnUpdateClass_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClass.SelectedRows.Count == 0)
                {
                    ShowWarning("Vui lòng chọn một lớp để sửa!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtClassName.Text))
                {
                    ShowWarning("Vui lòng nhập tên lớp!");
                    return;
                }

                if (cmbSchool.SelectedValue == null)
                {
                    ShowWarning("Vui lòng chọn trường!");
                    return;
                }

                var selectedClass = dgvClass.SelectedRows[0].DataBoundItem as Class;
                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    var classObj = await context.Classes.FindAsync(selectedClass.ClassId);
                    if (classObj == null)
                    {
                        ShowWarning("Lớp không tồn tại!");
                        return;
                    }

                    classObj.ClassName = txtClassName.Text.Trim();
                    classObj.SchoolId = (int)cmbSchool.SelectedValue;

                    if (await context.Classes.AnyAsync(c => c.ClassName == classObj.ClassName && c.SchoolId == classObj.SchoolId && c.ClassId != classObj.ClassId))
                    {
                        ShowWarning("Lớp đã tồn tại trong trường này!");
                        return;
                    }

                    await context.SaveChangesAsync();
                    ShowInfo("Cập nhật lớp thành công!");
                    await LoadClassesAsync();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi cập nhật lớp: {ex.Message}");
            }
        }

        private async void BtnDeleteClass_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClass.SelectedRows.Count == 0)
                {
                    ShowWarning("Vui lòng chọn một lớp để xóa!");
                    return;
                }

                var selectedClass = dgvClass.SelectedRows[0].DataBoundItem as Class;
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa lớp '{selectedClass.ClassName}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    var classObj = await context.Classes
                        .Include(c => c.Schedules)
                        .FirstOrDefaultAsync(c => c.ClassId == selectedClass.ClassId);
                    if (classObj == null)
                    {
                        ShowWarning("Lớp không tồn tại!");
                        return;
                    }

                    if (classObj.Schedules.Any())
                    {
                        ShowWarning("Không thể xóa lớp vì có lịch giảng dạy liên kết!");
                        return;
                    }

                    context.Classes.Remove(classObj);
                    await context.SaveChangesAsync();
                    ShowInfo("Xóa lớp thành công!");
                    await LoadClassesAsync();
                }
            }
            catch (Exception ex)
            {
                ShowError($"Lỗi khi xóa lớp: {ex.Message}");
            }
        }

        private void ClearClassInputs()
        {
            txtClassName.Clear();
            cmbSchool.SelectedIndex = -1;
            dgvClass.ClearSelection();
        }
        

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowInfo(string message)
        {
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}