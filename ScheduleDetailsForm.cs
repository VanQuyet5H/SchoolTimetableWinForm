using SchoolTimetableWinForm.Data;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SchoolTimetableWinForm
{
    public partial class ScheduleDetailsForm : Form
    {
        private readonly SchoolTimetableContext _context;
        private readonly Schedule _schedule;

        public ScheduleDetailsForm(Schedule schedule, SchoolTimetableContext context)
        {
            InitializeComponent();
            _schedule = schedule;
            _context = context;
            LoadScheduleDetails();
        }

        private void LoadScheduleDetails()
        {
            // Fetch related entities to get display names
            var school = _context.Schools.FirstOrDefault(s => s.SchoolId == _schedule.SchoolId);
            var timeSlot = _context.TimeSlots.FirstOrDefault(t => t.TimeSlotId == _schedule.TimeSlotId);
            var classRoom = _context.Classes.FirstOrDefault(c => c.ClassId == _schedule.ClassId);
            var teacher = _context.Teachers.FirstOrDefault(t => t.TeacherId == _schedule.TeacherId);
            var teachingAssistant = _context.Teachers.FirstOrDefault(t => t.TeacherId == _schedule.TeachingAssistantId);

            // Set values to labels
            lblSchool.Text = $"Trường: {school?.SchoolName ?? "N/A"}";
            lblTimeSlot.Text = $"Thời gian: {timeSlot?.SlotName} ({timeSlot?.StartTime.Hours:D2}:{timeSlot?.StartTime.Minutes:D2} - {timeSlot?.EndTime.Hours:D2}:{timeSlot?.EndTime.Minutes:D2})";
            lblClass.Text = $"Lớp: {classRoom?.ClassName ?? "N/A"}";
            lblTeacher.Text = $"Giáo viên: {teacher?.TeacherName ?? "N/A"}";
            lblTeachingAssistant.Text = $"Trợ giảng: {teachingAssistant?.TeacherName ?? "N/A"}";
            lblScheduleDate.Text = $"Ngày: {_schedule.ScheduleDate.ToString("dd/MM/yyyy")}";
            lblStatus.Text = $"Trạng thái: {_schedule.Status ?? "Có Mặt"}";
            lblOffReason.Text = $"Lý do nghỉ: {_schedule.OffReason ?? "N/A"}";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}