// AddScheduleForm.cs

using Microsoft.EntityFrameworkCore;
using SchoolTimetableWinForm.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SchoolTimetableWinForm
{
    public partial class AddScheduleForm : Form
    {
        private readonly DbContextOptions<SchoolTimetableContext> _contextOptions;
        private readonly int _schoolId;
        private readonly Schedule _existingSchedule;

        public AddScheduleForm(DbContextOptions<SchoolTimetableContext> contextOptions, int schoolId, Schedule existingSchedule = null)
        {
            _contextOptions = contextOptions;
            _schoolId = schoolId;
            _existingSchedule = existingSchedule;
            InitializeComponent();
            LoadComboBoxes();
            if (_existingSchedule != null)
            {
                PopulateForm();
                this.Text = "Sửa Lịch";
            }
            else
            {
                this.Text = "Tạo Lịch Mới";
            }
        }

        private async void LoadComboBoxes()
        {
            try
            {
                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    int selectedSchoolId = 1;

                    // Truy vấn danh sách giáo viên, lớp học, và khung giờ, liên kết với trường được chọn
                    var teachers = await context.Teachers
                        .Where(t => t.SchoolId == selectedSchoolId)
                        .OrderBy(t => t.TeacherName)
                        .ToListAsync();

                    var classes = await context.Classes
                        .Where(c => c.SchoolId == selectedSchoolId)
                        .OrderBy(c => c.ClassName)
                        .ToListAsync();

                    var timeSlots = await context.TimeSlots
                        .Where(ts => ts.TimeSlotId == selectedSchoolId)
                        .OrderBy(ts => ts.StartTime)
                        .ToListAsync();
                    var teachingAssistants = await context.TeachingAssistants.OrderBy(ta => ta.TeachingAssistantCode).ToListAsync();

                    cmbTeacher.DataSource = teachers;
                    cmbTeacher.DisplayMember = "TeacherName";
                    cmbTeacher.ValueMember = "TeacherId";

                    cmbClass.DataSource = classes;
                    cmbClass.DisplayMember = "ClassName";
                    cmbClass.ValueMember = "ClassId";

                    cmbTimeSlot.DataSource = timeSlots;
                    cmbTimeSlot.DisplayMember = "SlotName";
                    cmbTimeSlot.ValueMember = "TimeSlotId";

                    var assistantList = new List<TeachingAssistant> { new TeachingAssistant { TeachingAssistantId = 0, TeachingAssistantCode = "Không có" } };
                    assistantList.AddRange(teachingAssistants);
                    cmbTeachingAssistant.DataSource = assistantList;
                    cmbTeachingAssistant.DisplayMember = "TeachingAssistantCode";
                    cmbTeachingAssistant.ValueMember = "TeachingAssistantId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateForm()
        {
            cmbTeacher.SelectedValue = _existingSchedule.TeacherId;
            cmbClass.SelectedValue = _existingSchedule.ClassId;
            cmbTimeSlot.SelectedValue = _existingSchedule.TimeSlotId;
            dtpScheduleDate.Value = _existingSchedule.ScheduleDate;
            cmbTeachingAssistant.SelectedValue = _existingSchedule.TeachingAssistantId ?? 0;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTeacher.SelectedValue == null || cmbClass.SelectedValue == null || cmbTimeSlot.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var context = new SchoolTimetableContext(_contextOptions))
                {
                    Schedule schedule = _existingSchedule ?? new Schedule();
                    schedule.SchoolId = _schoolId;
                    schedule.TeacherId = (int)cmbTeacher.SelectedValue;
                    schedule.ClassId = (int)cmbClass.SelectedValue;
                    schedule.TimeSlotId = (int)cmbTimeSlot.SelectedValue;
                    schedule.ScheduleDate = dtpScheduleDate.Value.Date;
                    schedule.TeachingAssistantId = (int?)cmbTeachingAssistant.SelectedValue == 0 ? null : (int?)cmbTeachingAssistant.SelectedValue;
                    schedule.WeekId = context.Weeks
                        .OrderBy(w => Math.Abs((w.StartDate - dtpScheduleDate.Value).TotalDays))
                        .FirstOrDefault()?.WeekId ?? 0;

                    var conflict = await context.Schedules
                        .AnyAsync(s => s.TeacherId == schedule.TeacherId &&
                                         s.ScheduleDate == schedule.ScheduleDate &&
                                         s.TimeSlotId == schedule.TimeSlotId &&
                                         s.ScheduleId != schedule.ScheduleId);

                    if (conflict)
                    {
                        MessageBox.Show("Giáo viên đã có lịch trong tiết này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (_existingSchedule == null)
                    {
                        context.Schedules.Add(schedule);
                    }
                    else
                    {
                        context.Schedules.Update(schedule);
                    }

                    await context.SaveChangesAsync();
                    MessageBox.Show("Lưu lịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu lịch: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}