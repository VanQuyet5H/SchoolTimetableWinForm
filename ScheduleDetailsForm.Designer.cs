using System.Windows.Forms;

namespace SchoolTimetableWinForm
{
    partial class ScheduleDetailsForm
    {
        // Designer-generated code
        private Label lblSchool;
        private Label lblTimeSlot;
        private Label lblClass;
        private Label lblTeacher;
        private Label lblTeachingAssistant;
        private Label lblScheduleDate;
        private Label lblStatus;
        private Label lblOffReason;
        private Button btnClose;

        private void InitializeComponent()
        {
            this.lblSchool = new Label();
            this.lblTimeSlot = new Label();
            this.lblClass = new Label();
            this.lblTeacher = new Label();
            this.lblTeachingAssistant = new Label();
            this.lblScheduleDate = new Label();
            this.lblStatus = new Label();
            this.lblOffReason = new Label();
            this.btnClose = new Button();

            this.SuspendLayout();

            // lblSchool
            this.lblSchool.AutoSize = true;
            this.lblSchool.Location = new System.Drawing.Point(20, 20);
            this.lblSchool.Name = "lblSchool";
            this.lblSchool.Size = new System.Drawing.Size(100, 19);
            this.lblSchool.TabIndex = 0;

            // lblTimeSlot
            this.lblTimeSlot.AutoSize = true;
            this.lblTimeSlot.Location = new System.Drawing.Point(20, 50);
            this.lblTimeSlot.Name = "lblTimeSlot";
            this.lblTimeSlot.Size = new System.Drawing.Size(100, 19);
            this.lblTimeSlot.TabIndex = 1;

            // lblClass
            this.lblClass.AutoSize = true;
            this.lblClass.Location = new System.Drawing.Point(20, 80);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(100, 19);
            this.lblClass.TabIndex = 2;

            // lblTeacher
            this.lblTeacher.AutoSize = true;
            this.lblTeacher.Location = new System.Drawing.Point(20, 110);
            this.lblTeacher.Name = "lblTeacher";
            this.lblTeacher.Size = new System.Drawing.Size(100, 19);
            this.lblTeacher.TabIndex = 3;

            // lblTeachingAssistant
            this.lblTeachingAssistant.AutoSize = true;
            this.lblTeachingAssistant.Location = new System.Drawing.Point(20, 140);
            this.lblTeachingAssistant.Name = "lblTeachingAssistant";
            this.lblTeachingAssistant.Size = new System.Drawing.Size(100, 19);
            this.lblTeachingAssistant.TabIndex = 4;

            // lblScheduleDate
            this.lblScheduleDate.AutoSize = true;
            this.lblScheduleDate.Location = new System.Drawing.Point(20, 170);
            this.lblScheduleDate.Name = "lblScheduleDate";
            this.lblScheduleDate.Size = new System.Drawing.Size(100, 19);
            this.lblScheduleDate.TabIndex = 5;

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 200);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(100, 19);
            this.lblStatus.TabIndex = 6;

            // lblOffReason
            this.lblOffReason.AutoSize = true;
            this.lblOffReason.Location = new System.Drawing.Point(20, 230);
            this.lblOffReason.Name = "lblOffReason";
            this.lblOffReason.Size = new System.Drawing.Size(100, 19);
            this.lblOffReason.TabIndex = 7;

            // btnClose
            this.btnClose.Location = new System.Drawing.Point(200, 260);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);

            // ScheduleDetailsForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.lblSchool);
            this.Controls.Add(this.lblTimeSlot);
            this.Controls.Add(this.lblClass);
            this.Controls.Add(this.lblTeacher);
            this.Controls.Add(this.lblTeachingAssistant);
            this.Controls.Add(this.lblScheduleDate);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblOffReason);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScheduleDetailsForm";
            this.Text = "Chi tiết lịch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}