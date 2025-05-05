using SchoolTimetableWinForm.Data;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SchoolTimetableWinForm
{
    public partial class BangChamCongForm : Form
    {
        private readonly SchoolTimetableContext _context;

        public BangChamCongForm(SchoolTimetableContext context)
        {
            InitializeComponent();
            _context = context;
        }

        private void BangChamCongForm_Load(object sender, EventArgs e)
        {
            // Đặt mặc định khoảng thời gian là tháng hiện tại
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpToDate.Value = dtpFromDate.Value.AddMonths(1).AddDays(-1);

            // Đặt mặc định loại báo cáo
            cbReportType.SelectedIndex = 0; // "Tổng hợp giảng dạy"

            LoadTimetableSummary();
        }

        private void LoadTimetableSummary()
        {
            if (cbReportType.SelectedIndex == 0)
            {
                // Báo cáo "Tổng hợp giảng dạy"
                LoadSummaryReport();
            }
            else
            {
                // Báo cáo "Chấm công giáo viên nước ngoài"
                LoadWeeklyReport();
            }
        }

        // Báo cáo "Tổng hợp giảng dạy" (định dạng gốc: theo ngày trong tháng)
        private void LoadSummaryReport()
        {
            try
            {
                // Lấy khoảng thời gian
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date;

                if (fromDate > toDate)
                {
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cập nhật tiêu đề form
                this.Text = $"BẢNG TỔNG HỢP GIẢNG DẠY - Tháng {fromDate.Month} năm {fromDate.Year}";

                // Tính số ngày trong khoảng thời gian
                int daysCount = (toDate - fromDate).Days + 1;

                // Lấy danh sách giáo viên, trường, lịch giảng dạy và tiết học
                var teachers = _context.Teachers.ToList();
                var schools = _context.Schools.ToList();
                var schedules = _context.Schedules
                    .Where(s => s.ScheduleDate >= fromDate && s.ScheduleDate <= toDate && s.Status == "ACTIVE")
                    .ToList();
                var timeSlots = _context.TimeSlots.ToList();

                // Cấu hình DataGridView
                ConfigureSummaryDataGridView(daysCount, fromDate);

                // Thêm dữ liệu cho từng giáo viên
                int rowIndex = 0;
                foreach (var teacher in teachers)
                {
                    rowIndex = dgvTimetable.Rows.Add();
                    var school = schools.FirstOrDefault(s => s.SchoolId == teacher.SchoolId);

                    // Điền thông tin giáo viên
                    dgvTimetable.Rows[rowIndex].Cells["MaNV"].Value = teacher.TeacherId; // Mã NV
                    dgvTimetable.Rows[rowIndex].Cells["TenTruong"].Value = school?.SchoolName ?? "N/A"; // Tên trường
                    dgvTimetable.Rows[rowIndex].Cells["TenGiaoVien"].Value = teacher.TeacherName; // Tên giáo viên

                    // Tính số buổi dạy và số giờ
                    double totalClasses = 0; // Tổng số lớp (buổi dạy)
                    double totalHours = 0;   // Tổng số giờ (dựa trên EndTime - StartTime)
                    for (int dayOffset = 0; dayOffset < daysCount; dayOffset++)
                    {
                        DateTime currentDate = fromDate.AddDays(dayOffset);
                        var teacherSchedules = schedules
                            .Where(s => s.TeacherId == teacher.TeacherId && s.ScheduleDate.Date == currentDate.Date)
                            .ToList();

                        double sessions = teacherSchedules.Count; // Số buổi dạy trong ngày
                        if (sessions > 0)
                        {
                            dgvTimetable.Rows[rowIndex].Cells[$"Day_{dayOffset}"].Value = sessions.ToString("0.0");
                            totalClasses += sessions;

                            // Tính số giờ dựa trên EndTime - StartTime
                            double hours = teacherSchedules.Sum(s =>
                            {
                                var timeSlot = timeSlots.FirstOrDefault(ts => ts.TimeSlotId == s.TimeSlotId);
                                if (timeSlot == null) return 0;
                                return (timeSlot.EndTime - timeSlot.StartTime).TotalHours;
                            });
                            totalHours += hours;
                        }
                        else
                        {
                            dgvTimetable.Rows[rowIndex].Cells[$"Day_{dayOffset}"].Value = "-";
                        }
                    }

                    // Tính tổng số lớp và số giờ
                    dgvTimetable.Rows[rowIndex].Cells["SoLop"].Value = totalClasses.ToString("0.00"); // Tổng số lớp
                    dgvTimetable.Rows[rowIndex].Cells["SoGio"].Value = totalHours.ToString("0.00");   // Tổng số giờ
                }

                // Thêm hàng tổng cộng
                rowIndex = dgvTimetable.Rows.Add();
                dgvTimetable.Rows[rowIndex].Cells["TenGiaoVien"].Value = "Tổng cộng:";
                dgvTimetable.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                dgvTimetable.Rows[rowIndex].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);

                double totalClassesOverall = 0;
                double totalHoursOverall = 0;

                for (int dayOffset = 0; dayOffset < daysCount; dayOffset++)
                {
                    DateTime currentDate = fromDate.AddDays(dayOffset);
                    var daySchedules = schedules
                        .Where(s => s.ScheduleDate.Date == currentDate.Date)
                        .ToList();

                    double sessions = daySchedules.Count;
                    if (sessions > 0)
                    {
                        dgvTimetable.Rows[rowIndex].Cells[$"Day_{dayOffset}"].Value = sessions.ToString("0.0");
                    }
                    else
                    {
                        dgvTimetable.Rows[rowIndex].Cells[$"Day_{dayOffset}"].Value = "0.0";
                    }
                    totalClassesOverall += sessions;

                    // Tính số giờ tổng cộng trong ngày
                    double hours = daySchedules.Sum(s =>
                    {
                        var timeSlot = timeSlots.FirstOrDefault(ts => ts.TimeSlotId == s.TimeSlotId);
                        if (timeSlot == null) return 0;
                        return (timeSlot.EndTime - timeSlot.StartTime).TotalHours;
                    });
                    totalHoursOverall += hours;
                }

                dgvTimetable.Rows[rowIndex].Cells["SoLop"].Value = totalClassesOverall.ToString("0.00");
                dgvTimetable.Rows[rowIndex].Cells["SoGio"].Value = totalHoursOverall.ToString("0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải bảng tổng hợp: {ex.Message}\nStack Trace: {ex.StackTrace}",
                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureSummaryDataGridView(int daysCount, DateTime fromDate)
        {
            dgvTimetable.Columns.Clear();
            dgvTimetable.Rows.Clear();

            // Thêm cột "Mã NV", "Tên trường", "Tên giáo viên"
            dgvTimetable.Columns.Add("MaNV", "Mã NV");
            dgvTimetable.Columns.Add("TenTruong", "Tên trường");
            dgvTimetable.Columns.Add("TenGiaoVien", "Tên giáo viên");

            dgvTimetable.Columns["MaNV"].Width = 50;
            dgvTimetable.Columns["TenTruong"].Width = 150;
            dgvTimetable.Columns["TenGiaoVien"].Width = 150;

            // Thêm cột cho từng ngày
            for (int dayOffset = 0; dayOffset < daysCount; dayOffset++)
            {
                DateTime currentDate = fromDate.AddDays(dayOffset);
                string dayOfWeek = GetDayOfWeekShort(currentDate.DayOfWeek);
                string columnName = $"Day_{dayOffset}";
                // Hiển thị ngày/tháng\nthứ
                dgvTimetable.Columns.Add(columnName, $"{currentDate.ToString("dd/MM")}\n{dayOfWeek}");
                dgvTimetable.Columns[columnName].Width = 50;

                // Tô màu cột Chủ Nhật
                if (dayOfWeek == "CN")
                {
                    dgvTimetable.Columns[columnName].DefaultCellStyle.BackColor = Color.FromArgb(255, 204, 204);
                    dgvTimetable.Columns[columnName].HeaderCell.Style.BackColor = Color.FromArgb(255, 204, 204);
                }
            }

            // Thêm cột "Số lớp" và "Số giờ"
            dgvTimetable.Columns.Add("SoLop", "Số lớp");
            dgvTimetable.Columns.Add("SoGio", "Số giờ");
            dgvTimetable.Columns["SoLop"].Width = 80;
            dgvTimetable.Columns["SoGio"].Width = 80;

            // Căn giữa tất cả cột
            foreach (DataGridViewColumn col in dgvTimetable.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // Báo cáo "Chấm công giáo viên nước ngoài" (định dạng mới: theo tuần)
        private void LoadWeeklyReport()
        {
            try
            {
                // Lấy khoảng thời gian
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date;

                if (fromDate > toDate)
                {
                    MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cập nhật tiêu đề form
                this.Text = $"BẢNG CHẤM CÔNG GIÁO VIÊN NƯỚC NGOÀI - Tháng {fromDate.Month} năm {fromDate.Year}";

                // Tính số ngày trong khoảng thời gian
                int daysCount = (toDate - fromDate).Days + 1;

                // Tính số tuần (mỗi tuần từ T2 đến CN)
                DateTime startOfWeek = fromDate;
                while (startOfWeek.DayOfWeek != DayOfWeek.Monday)
                {
                    startOfWeek = startOfWeek.AddDays(-1);
                }
                DateTime endOfWeek = toDate;
                while (endOfWeek.DayOfWeek != DayOfWeek.Sunday)
                {
                    endOfWeek = endOfWeek.AddDays(1);
                }
                int weeksCount = (int)Math.Ceiling((endOfWeek - startOfWeek).Days / 7.0);

                // Lấy danh sách giáo viên, trường, lịch giảng dạy và tiết học
                var teachers = _context.Teachers.ToList();
                var schools = _context.Schools.ToList();
                var schedules = _context.Schedules
                    .Where(s => s.ScheduleDate >= fromDate && s.ScheduleDate <= toDate && s.Status == "ACTIVE")
                    .ToList();
                var timeSlots = _context.TimeSlots.ToList();

                // Cấu hình DataGridView
                ConfigureWeeklyDataGridView(weeksCount, startOfWeek, fromDate, toDate);

                // Thêm dữ liệu cho từng giáo viên
                int rowIndex = 1; // Bắt đầu từ hàng 1 vì hàng 0 là tiêu đề tuần
                foreach (var teacher in teachers)
                {
                    rowIndex = dgvTimetable.Rows.Add();
                    var school = schools.FirstOrDefault(s => s.SchoolId == teacher.SchoolId);

                    // Điền thông tin giáo viên
                    dgvTimetable.Rows[rowIndex].Cells["MaNV"].Value = teacher.TeacherId; // Mã NV
                    dgvTimetable.Rows[rowIndex].Cells["HoTen"].Value = teacher.TeacherName; // Họ tên

                    // Tính số buổi dạy và số giờ
                    double totalClasses = 0; // Tổng số lớp (buổi dạy)
                    double totalHours = 0;   // Tổng số giờ (dựa trên EndTime - StartTime)
                    for (int week = 0; week < weeksCount; week++)
                    {
                        DateTime weekStart = startOfWeek.AddDays(week * 7);
                        for (int dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++) // T2-CN
                        {
                            DateTime currentDate = weekStart.AddDays(dayOfWeek);
                            if (currentDate < fromDate || currentDate > toDate)
                            {
                                dgvTimetable.Rows[rowIndex].Cells[$"Week_{week}_Day_{dayOfWeek}"].Value = "-";
                                continue;
                            }

                            var teacherSchedules = schedules
                                .Where(s => s.TeacherId == teacher.TeacherId && s.ScheduleDate.Date == currentDate.Date)
                                .ToList();

                            double sessions = teacherSchedules.Count; // Số buổi dạy trong ngày
                            if (sessions > 0)
                            {
                                dgvTimetable.Rows[rowIndex].Cells[$"Week_{week}_Day_{dayOfWeek}"].Value = sessions.ToString("0.0");
                                totalClasses += sessions;

                                // Tính số giờ dựa trên EndTime - StartTime
                                double hours = teacherSchedules.Sum(s =>
                                {
                                    var timeSlot = timeSlots.FirstOrDefault(ts => ts.TimeSlotId == s.TimeSlotId);
                                    if (timeSlot == null) return 0;
                                    return (timeSlot.EndTime - timeSlot.StartTime).TotalHours;
                                });
                                totalHours += hours;
                            }
                            else
                            {
                                dgvTimetable.Rows[rowIndex].Cells[$"Week_{week}_Day_{dayOfWeek}"].Value = "-";
                            }
                        }
                    }

                    // Tính tổng số lớp và số giờ
                    dgvTimetable.Rows[rowIndex].Cells["SoLop"].Value = totalClasses.ToString("0.00"); // Tổng số lớp
                    dgvTimetable.Rows[rowIndex].Cells["SoGio"].Value = totalHours.ToString("0.00");   // Tổng số giờ
                }

                // Thêm hàng tổng cộng
                rowIndex = dgvTimetable.Rows.Add();
                dgvTimetable.Rows[rowIndex].Cells["HoTen"].Value = "Tổng cộng:";
                dgvTimetable.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                dgvTimetable.Rows[rowIndex].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);

                double totalClassesOverall = 0;
                double totalHoursOverall = 0;

                for (int week = 0; week < weeksCount; week++)
                {
                    DateTime weekStart = startOfWeek.AddDays(week * 7);
                    for (int dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++)
                    {
                        DateTime currentDate = weekStart.AddDays(dayOfWeek);
                        if (currentDate < fromDate || currentDate > toDate)
                        {
                            dgvTimetable.Rows[rowIndex].Cells[$"Week_{week}_Day_{dayOfWeek}"].Value = "0.0";
                            continue;
                        }

                        var daySchedules = schedules
                            .Where(s => s.ScheduleDate.Date == currentDate.Date)
                            .ToList();

                        double sessions = daySchedules.Count;
                        if (sessions > 0)
                        {
                            dgvTimetable.Rows[rowIndex].Cells[$"Week_{week}_Day_{dayOfWeek}"].Value = sessions.ToString("0.0");
                        }
                        else
                        {
                            dgvTimetable.Rows[rowIndex].Cells[$"Week_{week}_Day_{dayOfWeek}"].Value = "0.0";
                        }
                        totalClassesOverall += sessions;

                        // Tính số giờ tổng cộng trong ngày
                        double hours = daySchedules.Sum(s =>
                        {
                            var timeSlot = timeSlots.FirstOrDefault(ts => ts.TimeSlotId == s.TimeSlotId);
                            if (timeSlot == null) return 0;
                            return (timeSlot.EndTime - timeSlot.StartTime).TotalHours;
                        });
                        totalHoursOverall += hours;
                    }
                }

                dgvTimetable.Rows[rowIndex].Cells["SoLop"].Value = totalClassesOverall.ToString("0.00");
                dgvTimetable.Rows[rowIndex].Cells["SoGio"].Value = totalHoursOverall.ToString("0.00");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải bảng chấm công: {ex.Message}\nStack Trace: {ex.StackTrace}",
                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureWeeklyDataGridView(int weeksCount, DateTime startOfWeek, DateTime fromDate, DateTime toDate)
        {
            dgvTimetable.Columns.Clear();
            dgvTimetable.Rows.Clear();

            // Thêm cột "Mã NV", "Họ tên"
            dgvTimetable.Columns.Add("MaNV", "Mã NV");
            dgvTimetable.Columns.Add("HoTen", "Họ tên");

            dgvTimetable.Columns["MaNV"].Width = 50;
            dgvTimetable.Columns["HoTen"].Width = 150;

            // Màu nền cho từng tuần
            Color[] weekColors = new Color[]
            {
                Color.FromArgb(204, 255, 204), // Xanh lá nhạt
                Color.FromArgb(255, 255, 204), // Vàng nhạt
                Color.FromArgb(204, 229, 255), // Xanh dương nhạt
                Color.FromArgb(255, 204, 204), // Đỏ nhạt
                Color.FromArgb(204, 255, 255)  // Thanh nhạt
            };

            // Thêm cột cho từng tuần
            string[] daysOfWeek = { "T2", "T3", "T4", "T5", "T6", "T7", "CN" };
            for (int week = 0; week < weeksCount; week++)
            {
                DateTime weekStart = startOfWeek.AddDays(week * 7);
                for (int dayOfWeek = 0; dayOfWeek < 7; dayOfWeek++)
                {
                    DateTime currentDate = weekStart.AddDays(dayOfWeek);
                    string columnName = $"Week_{week}_Day_{dayOfWeek}";
                    string headerText = daysOfWeek[dayOfWeek];
                    if (currentDate >= fromDate && currentDate <= toDate)
                    {
                        // Hiển thị ngày/tháng\nthứ
                        headerText = $"{currentDate.ToString("dd/MM")}\n{headerText}";
                    }
                    dgvTimetable.Columns.Add(columnName, headerText);
                    dgvTimetable.Columns[columnName].Width = 50;

                    // Tô màu cột Chủ Nhật
                    if (daysOfWeek[dayOfWeek] == "CN")
                    {
                        dgvTimetable.Columns[columnName].DefaultCellStyle.BackColor = Color.FromArgb(255, 204, 204);
                        dgvTimetable.Columns[columnName].HeaderCell.Style.BackColor = Color.FromArgb(255, 204, 204);
                    }
                    else
                    {
                        // Tô màu nền cho tuần
                        Color weekColor = weekColors[week % weekColors.Length];
                        dgvTimetable.Columns[columnName].DefaultCellStyle.BackColor = weekColor;
                        dgvTimetable.Columns[columnName].HeaderCell.Style.BackColor = weekColor;
                    }
                }
            }

            // Thêm cột "Số lớp" và "Số giờ"
            dgvTimetable.Columns.Add("SoLop", "Số lớp");
            dgvTimetable.Columns.Add("SoGio", "Số giờ");
            dgvTimetable.Columns["SoLop"].Width = 80;
            dgvTimetable.Columns["SoGio"].Width = 80;

            // Căn giữa tất cả cột
            foreach (DataGridViewColumn col in dgvTimetable.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Thêm tiêu đề tuần
            int rowIndex = dgvTimetable.Rows.Add();
            for (int week = 0; week < weeksCount; week++)
            {
                int startCol = 2 + week * 7; // Bắt đầu từ cột thứ 2 (sau "Mã NV", "Họ tên")
                if (startCol < dgvTimetable.Columns.Count)
                {
                    dgvTimetable.Rows[rowIndex].Cells[startCol].Value = $"Tuần {week + 1}";
                    for (int day = 0; day < 7; day++)
                    {
                        if (startCol + day < dgvTimetable.Columns.Count)
                        {
                            dgvTimetable.Rows[rowIndex].Cells[startCol + day].Style.BackColor = weekColors[week % weekColors.Length];
                        }
                    }
                }
            }
            dgvTimetable.Rows[rowIndex].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            dgvTimetable.Rows[rowIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private string GetDayOfWeekShort(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday: return "T2";
                case DayOfWeek.Tuesday: return "T3";
                case DayOfWeek.Wednesday: return "T4";
                case DayOfWeek.Thursday: return "T5";
                case DayOfWeek.Friday: return "T6";
                case DayOfWeek.Saturday: return "T7";
                case DayOfWeek.Sunday: return "CN";
                default: return "";
            }
        }

        private void cbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTimetableSummary();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadTimetableSummary();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}