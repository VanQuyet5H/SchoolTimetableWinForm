using OfficeOpenXml;
using SchoolTimetableWinForm.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SchoolTimetableWinForm
{
    public partial class ImportDataForm : Form
    {
        private readonly SchoolTimetableContext _context;
        private DateTimePicker dtpMonthYear;
        private Button btnImport;
        private Button btnClose;
        private string selectedExcelFilePath;

        public ImportDataForm(SchoolTimetableContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            InitializeComponent();
        }
        private void BtnImport_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra _context và các DbSet
                if (_context == null || _context.Schools == null || _context.Weeks == null ||
                    _context.Teachers == null || _context.Classes == null ||
                    _context.TeachingAssistants == null || _context.TimeSlots == null ||
                    _context.Schedules == null)
                {
                    throw new Exception("SchoolTimetableContext hoặc DbSet chưa được khởi tạo.");
                }

                // Chọn file Excel
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
                    openFileDialog.Title = "Chọn File Excel";

                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                        return;

                    selectedExcelFilePath = openFileDialog.FileName;
                }

                // Xóa dữ liệu cũ
                _context.Schedules.RemoveRange(_context.Schedules);
                _context.Weeks.RemoveRange(_context.Weeks);
                _context.Teachers.RemoveRange(_context.Teachers);
                _context.Classes.RemoveRange(_context.Classes);
                _context.TeachingAssistants.RemoveRange(_context.TeachingAssistants);
                _context.TimeSlots.RemoveRange(_context.TimeSlots);
                _context.Schools.RemoveRange(_context.Schools);
                _context.SaveChanges();

                // Seed Schools
                var schoolVG = new School { SchoolName = "THPT Văn Giang" };
                var schoolNTT = new School { SchoolName = "THPT Nguyễn Thiện Thuật" };
                var schoolDQH = new School { SchoolName = "THPT Dương Quảng Hàm" };
                var schoolAT = new School { SchoolName = "THPT Ân Thi" };
                var schoolTQK = new School { SchoolName = "THPT Trần Quang Khải" };
                var schoolTQP = new School { SchoolName = "THPT Triệu Quang Phục" };
                var schoolKD = new School { SchoolName = "THPT Kim Động" };
                var schoolNTN = new School { SchoolName = "THPT Nguyễn Trung Ngạn" };
                var schoolNS = new School { SchoolName = "THPT Nguyễn Siêu" };
                var schoolMC = new School { SchoolName = "THPT Minh Châu" };

                _context.Schools.AddRange(schoolVG, schoolNTT, schoolDQH, schoolAT, schoolTQK, schoolTQP, schoolKD, schoolNTN, schoolNS, schoolMC);
                _context.SaveChanges();

                var schools = new List<School>
                {
                    schoolVG, schoolNTT, schoolDQH, schoolAT, schoolTQK,
                    schoolTQP, schoolKD, schoolNTN, schoolNS, schoolMC
                };

                // Seed Classes
                var classList = new List<Class>();
                foreach (var school in schools)
                {
                    for (int grade = 10; grade <= 12; grade++)
                    {
                        for (int i = 1; i <= 3; i++)
                        {
                            classList.Add(new Class
                            {
                                ClassName = $"{grade}A{i}",
                                SchoolId = school.SchoolId
                            });
                        }
                    }
                }
                _context.Classes.AddRange(classList);
                _context.SaveChanges();

                // Seed Teachers
                var teacherHelen = new Teacher { TeacherName = "Helen", SchoolId = schoolVG.SchoolId };
                var teacherMichelle = new Teacher { TeacherName = "Michelle", SchoolId = schoolVG.SchoolId };
                var teacherJohan = new Teacher { TeacherName = "Johan", SchoolId = schoolVG.SchoolId };
                var teacherValentina = new Teacher { TeacherName = "Valentina", SchoolId = schoolVG.SchoolId };
                var teacherMolly = new Teacher { TeacherName = "Molly", SchoolId = schoolVG.SchoolId };
                var teacherHitcham = new Teacher { TeacherName = "Hitcham", SchoolId = schoolVG.SchoolId };
                var teacherWaadNaily = new Teacher { TeacherName = "Waad Naily", SchoolId = schoolVG.SchoolId };
                var teacherAlper = new Teacher { TeacherName = "Alper", SchoolId = schoolVG.SchoolId };
                var teacherVlada = new Teacher { TeacherName = "Vlada", SchoolId = schoolVG.SchoolId };
                var teacherSofia = new Teacher { TeacherName = "Sofia", SchoolId = schoolVG.SchoolId };
                var teacherDavidS = new Teacher { TeacherName = "David S", SchoolId = schoolVG.SchoolId };
                var teacherDaoud = new Teacher { TeacherName = "Daoud", SchoolId = schoolVG.SchoolId };
                var teacherDmitry = new Teacher { TeacherName = "Dmitry", SchoolId = schoolVG.SchoolId };

                _context.Teachers.AddRange(teacherHelen, teacherMichelle, teacherJohan, teacherValentina, teacherMolly, teacherHitcham, teacherWaadNaily, teacherAlper, teacherVlada, teacherSofia, teacherDavidS, teacherDaoud, teacherDmitry);
                _context.SaveChanges();

                var teachers = new List<Teacher>
                {
                    teacherHelen, teacherMichelle, teacherJohan, teacherValentina, teacherMolly,
                    teacherHitcham, teacherWaadNaily, teacherAlper, teacherVlada, teacherSofia,
                    teacherDavidS, teacherDaoud, teacherDmitry
                };

                // Seed Teaching Assistants (TAs)
                var taList = new List<TeachingAssistant>
                {
                    new TeachingAssistant { TeachingAssistantName = "TA1", SchoolId = schoolVG.SchoolId },
                    new TeachingAssistant { TeachingAssistantName = "TA2", SchoolId = schoolVG.SchoolId },
                    new TeachingAssistant { TeachingAssistantName = "TA3", SchoolId = schoolVG.SchoolId },
                    new TeachingAssistant { TeachingAssistantName = "TA4", SchoolId = schoolVG.SchoolId }
                };
                _context.TeachingAssistants.AddRange(taList);
                _context.SaveChanges();
                var week = new Week
                {
                    WeekName = "Tuần 1 - HK1 2025",
                    StartDate = new DateTime(2025, 8, 26),
                    EndDate = new DateTime(2025, 8, 30)
                };
                _context.Weeks.Add(week);
                _context.SaveChanges();
                // Seed TimeSlots
                var timeSlots = new List<TimeSlot>
                {
                    new TimeSlot { SlotName = "Tiết 1", StartTime = new TimeSpan(7, 15, 0), EndTime = new TimeSpan(8, 0, 0) },
                    new TimeSlot { SlotName = "Tiết 2", StartTime = new TimeSpan(8, 5, 0), EndTime = new TimeSpan(8, 50, 0) },
                    new TimeSlot { SlotName = "Tiết 3", StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(9, 45, 0) },
                    new TimeSlot { SlotName = "Tiết 4", StartTime = new TimeSpan(9, 50, 0), EndTime = new TimeSpan(10, 35, 0) },
                    new TimeSlot { SlotName = "Tiết 5", StartTime = new TimeSpan(10, 40, 0), EndTime = new TimeSpan(11, 25, 0) },
                    new TimeSlot { SlotName = "Tiết 6", StartTime = new TimeSpan(13, 30, 0), EndTime = new TimeSpan(14, 15, 0) },
                    new TimeSlot { SlotName = "Tiết 7", StartTime = new TimeSpan(14, 20, 0), EndTime = new TimeSpan(15, 5, 0) },
                    new TimeSlot { SlotName = "Tiết 8", StartTime = new TimeSpan(15, 10, 0), EndTime = new TimeSpan(15, 55, 0) },
                    new TimeSlot { SlotName = "Tiết 9", StartTime = new TimeSpan(16, 0, 0), EndTime = new TimeSpan(16, 45, 0) }
                };
                _context.TimeSlots.AddRange(timeSlots);
                _context.SaveChanges();

                // Seed Weeks based on selected month/year
                DateTime selectedDate = dtpMonthYear.Value;
                DateTime startOfMonth = new DateTime(selectedDate.Year, selectedDate.Month, 1);
                DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);



                // Read and import schedule data from Excel
                ImportScheduleFromExcel(week, schools, teachers, taList, classList, timeSlots);

                MessageBox.Show("Import dữ liệu thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi import dữ liệu: {ex.Message}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "N/A")}",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Week ExtractWeekFromExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(selectedExcelFilePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                // Week information is in row 3, column A (e.g., "Week 1: from 30/12 to 05/01/2025")
                string weekInfo = worksheet.Cells[3, 1].Text;
                if (string.IsNullOrEmpty(weekInfo))
                    throw new Exception("Không tìm thấy thông tin tuần trong file Excel.");

                // Parse the week info (format: "Week X: from DD/MM to DD/MM/YYYY")
                var parts = weekInfo.Split(new[] { "Week ", ": from ", " to " }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 3)
                    throw new Exception("Định dạng thông tin tuần không hợp lệ.");

                // Extract week number
                int weekNumber = int.Parse(parts[0]);

                // Extract start date (DD/MM)
                var startDateParts = parts[1].Split('/');
                int startDay = int.Parse(startDateParts[0]);
                int startMonth = int.Parse(startDateParts[1]);

                // Extract end date (DD/MM/YYYY)
                var endDateParts = parts[2].Split('/');
                int endDay = int.Parse(endDateParts[0]);
                int endMonth = int.Parse(endDateParts[1]);
                int endYear = int.Parse(endDateParts[2]);

                // Determine the year for the start date (assuming it's the same year as the end date if not specified)
                int startYear = endMonth == 1 && startMonth == 12 ? endYear - 1 : endYear;

                DateTime startDate = new DateTime(startYear, startMonth, startDay);
                DateTime endDate = new DateTime(endYear, endMonth, endDay);

                return new Week
                {

                    StartDate = startDate,
                    EndDate = endDate
                };
            }
        }

        private void ImportScheduleFromExcel(Week week, List<School> schools, List<Teacher> teachers,
            List<TeachingAssistant> tas, List<Class> classes, List<TimeSlot> timeSlots)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(selectedExcelFilePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                // Find the starting row of the schedule data (after "TRƯỜNG [SCHOOL]")
                int startRow = 1;
                for (int row = 1; row <= rowCount; row++)
                {
                    if (worksheet.Cells[row, 1].Text.Contains("TRƯỜNG"))
                    {
                        startRow = row + 1;
                        break;
                    }
                }

                // Days of the week mapping
                var daysOfWeek = new Dictionary<int, DateTime>
                {
                    { 5, week.StartDate }, // Thứ 2
                    { 8, week.StartDate.AddDays(1) }, // Thứ 3
                    { 11, week.StartDate.AddDays(2) }, // Thứ 4
                    { 14, week.StartDate.AddDays(3) }, // Thứ 5
                    { 17, week.StartDate.AddDays(4) }, // Thứ 6
                    { 20, week.StartDate.AddDays(5) }, // Thứ 7
                    { 23, week.StartDate.AddDays(6) }  // Chủ nhật
                };


                string currentSchool = "";
                for (int row = startRow; row <= rowCount; row++)
                {
                    string schoolName = worksheet.Cells[row, 1].Text; // Column A
                    string session = worksheet.Cells[row, 2].Text;    // Column B (SÁNG/CHIỀU)
                    string timeSlotText = worksheet.Cells[row, 3].Text; // Column C (Time slot)

                    // Update current school if it's not empty
                    if (!string.IsNullOrEmpty(schoolName))
                        currentSchool = schoolName;

                    // Stop if we reach an empty row or a new section
                    if (string.IsNullOrEmpty(currentSchool) || string.IsNullOrEmpty(timeSlotText))
                        break;

                    // Map school
                    var school = schools.FirstOrDefault(s => s.SchoolName == currentSchool);
                    if (school == null)
                        continue;

                    // Map time slot
                    var timeSlot = timeSlots.FirstOrDefault(ts =>
                        $"{ts.StartTime.Hours:D2}:{ts.StartTime.Minutes:D2} - {ts.EndTime.Hours:D2}:{ts.EndTime.Minutes:D2}" == timeSlotText);
                    if (timeSlot == null)
                        continue;

                    // Read data for each day (Monday to Friday)
                    foreach (var dayEntry in daysOfWeek)
                    {
                        int col = dayEntry.Key; // Starting column for the day
                        DateTime day = dayEntry.Value;

                        string teacherName = worksheet.Cells[row, col].Text;
                        string taName = worksheet.Cells[row, col + 1].Text;
                        string className = worksheet.Cells[row, col + 2].Text;

                        // Skip if no data for this slot
                        if (string.IsNullOrEmpty(teacherName) && string.IsNullOrEmpty(taName) && string.IsNullOrEmpty(className))
                            continue;

                        // Map teacher
                        var teacher = teachers.FirstOrDefault(t => t.TeacherName == teacherName);
                        if (teacher == null)
                            continue;

                        // Map TA
                        var ta = tas.FirstOrDefault(t => t.TeachingAssistantName == taName);
                        if (ta == null)
                            continue;

                        // Map class
                        var classEntity = classes.FirstOrDefault(c => c.ClassName == className && c.SchoolId == school.SchoolId);
                        if (classEntity == null)
                            continue;

                        // Create schedule entry
                        var schedule = new Schedule
                        {
                            SchoolId = school.SchoolId,
                            WeekId = week.WeekId,
                            TimeSlotId = timeSlot.TimeSlotId,
                            ScheduleDate = day,
                            TeacherId = teacher.TeacherId,
                            TeachingAssistantId = ta.TeachingAssistantId,
                            ClassId = classEntity.ClassId
                        };
                        _context.Schedules.Add(schedule);
                    }
                }
                _context.SaveChanges();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}