using System.Drawing;
using System.Windows.Forms;

namespace SchoolTimetableWinForm
{
    partial class AddScheduleForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.mainContainer = new System.Windows.Forms.TableLayoutPanel();
            this.lblTeacher = new System.Windows.Forms.Label();
            this.cmbTeacher = new SchoolTimetableWinForm.BorderedComboBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.cmbClass = new SchoolTimetableWinForm.BorderedComboBox();
            this.lblTimeSlot = new System.Windows.Forms.Label();
            this.cmbTimeSlot = new SchoolTimetableWinForm.BorderedComboBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpScheduleDate = new System.Windows.Forms.DateTimePicker();
            this.lblAssistant = new System.Windows.Forms.Label();
            this.cmbTeachingAssistant = new SchoolTimetableWinForm.BorderedComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.mainContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.ColumnCount = 2;
            this.mainContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.mainContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainContainer.Controls.Add(this.lblTeacher, 0, 0);
            this.mainContainer.Controls.Add(this.cmbTeacher, 1, 0);
            this.mainContainer.Controls.Add(this.lblClass, 0, 1);
            this.mainContainer.Controls.Add(this.cmbClass, 1, 1);
            this.mainContainer.Controls.Add(this.lblTimeSlot, 0, 2);
            this.mainContainer.Controls.Add(this.cmbTimeSlot, 1, 2);
            this.mainContainer.Controls.Add(this.lblDate, 0, 3);
            this.mainContainer.Controls.Add(this.dtpScheduleDate, 1, 3);
            this.mainContainer.Controls.Add(this.lblAssistant, 0, 4);
            this.mainContainer.Controls.Add(this.cmbTeachingAssistant, 1, 4);
            this.mainContainer.Controls.Add(this.btnSave, 0, 5);
            this.mainContainer.Controls.Add(this.btnCancel, 1, 5);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Padding = new System.Windows.Forms.Padding(27, 24, 27, 24);
            this.mainContainer.RowCount = 6;
            this.mainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.mainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.mainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.mainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.mainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.mainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.mainContainer.Size = new System.Drawing.Size(580, 496);
            this.mainContainer.TabIndex = 0;
            // 
            // lblTeacher
            // 
            this.lblTeacher.AutoSize = true;
            this.lblTeacher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTeacher.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblTeacher.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblTeacher.Location = new System.Drawing.Point(31, 24);
            this.lblTeacher.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTeacher.Name = "lblTeacher";
            this.lblTeacher.Size = new System.Drawing.Size(152, 80);
            this.lblTeacher.TabIndex = 0;
            this.lblTeacher.Text = "GIÁO VIÊN:";
            this.lblTeacher.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbTeacher
            // 
            this.cmbTeacher.BorderColor = System.Drawing.Color.Black;
            this.cmbTeacher.BorderThickness = 1;
            this.cmbTeacher.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTeacher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTeacher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTeacher.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbTeacher.FormattingEnabled = true;
            this.cmbTeacher.Location = new System.Drawing.Point(190, 42);
            this.cmbTeacher.Margin = new System.Windows.Forms.Padding(3, 18, 3, 2);
            this.cmbTeacher.Name = "cmbTeacher";
            this.cmbTeacher.Size = new System.Drawing.Size(360, 38);
            this.cmbTeacher.TabIndex = 1;
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblClass.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblClass.Location = new System.Drawing.Point(31, 104);
            this.lblClass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(152, 80);
            this.lblClass.TabIndex = 2;
            this.lblClass.Text = "LỚP:";
            this.lblClass.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbClass
            // 
            this.cmbClass.BorderColor = System.Drawing.Color.Black;
            this.cmbClass.BorderThickness = 1;
            this.cmbClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbClass.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbClass.FormattingEnabled = true;
            this.cmbClass.Location = new System.Drawing.Point(190, 119);
            this.cmbClass.Margin = new System.Windows.Forms.Padding(3, 15, 3, 2);
            this.cmbClass.Name = "cmbClass";
            this.cmbClass.Size = new System.Drawing.Size(360, 38);
            this.cmbClass.TabIndex = 3;
            // 
            // lblTimeSlot
            // 
            this.lblTimeSlot.AutoSize = true;
            this.lblTimeSlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTimeSlot.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblTimeSlot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblTimeSlot.Location = new System.Drawing.Point(31, 184);
            this.lblTimeSlot.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimeSlot.Name = "lblTimeSlot";
            this.lblTimeSlot.Size = new System.Drawing.Size(152, 80);
            this.lblTimeSlot.TabIndex = 4;
            this.lblTimeSlot.Text = "TIẾT:";
            this.lblTimeSlot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbTimeSlot
            // 
            this.cmbTimeSlot.BorderColor = System.Drawing.Color.Black;
            this.cmbTimeSlot.BorderThickness = 1;
            this.cmbTimeSlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTimeSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimeSlot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTimeSlot.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbTimeSlot.FormattingEnabled = true;
            this.cmbTimeSlot.Location = new System.Drawing.Point(190, 199);
            this.cmbTimeSlot.Margin = new System.Windows.Forms.Padding(3, 15, 3, 2);
            this.cmbTimeSlot.Name = "cmbTimeSlot";
            this.cmbTimeSlot.Size = new System.Drawing.Size(360, 38);
            this.cmbTimeSlot.TabIndex = 5;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblDate.Location = new System.Drawing.Point(31, 264);
            this.lblDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(152, 80);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "NGÀY:";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpScheduleDate
            // 
            this.dtpScheduleDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpScheduleDate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpScheduleDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpScheduleDate.Location = new System.Drawing.Point(191, 279);
            this.dtpScheduleDate.Margin = new System.Windows.Forms.Padding(4, 15, 4, 4);
            this.dtpScheduleDate.Name = "dtpScheduleDate";
            this.dtpScheduleDate.Size = new System.Drawing.Size(358, 37);
            this.dtpScheduleDate.TabIndex = 7;
            // 
            // lblAssistant
            // 
            this.lblAssistant.AutoSize = true;
            this.lblAssistant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAssistant.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblAssistant.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblAssistant.Location = new System.Drawing.Point(31, 344);
            this.lblAssistant.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAssistant.Name = "lblAssistant";
            this.lblAssistant.Size = new System.Drawing.Size(152, 80);
            this.lblAssistant.TabIndex = 8;
            this.lblAssistant.Text = "TRỢ GIẢNG:";
            this.lblAssistant.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbTeachingAssistant
            // 
            this.cmbTeachingAssistant.BorderColor = System.Drawing.Color.Black;
            this.cmbTeachingAssistant.BorderThickness = 1;
            this.cmbTeachingAssistant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTeachingAssistant.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTeachingAssistant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTeachingAssistant.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbTeachingAssistant.FormattingEnabled = true;
            this.cmbTeachingAssistant.Location = new System.Drawing.Point(190, 359);
            this.cmbTeachingAssistant.Margin = new System.Windows.Forms.Padding(3, 15, 3, 2);
            this.cmbTeachingAssistant.Name = "cmbTeachingAssistant";
            this.cmbTeachingAssistant.Size = new System.Drawing.Size(360, 38);
            this.cmbTeachingAssistant.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(30, 426);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 44);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "LƯU";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(190, 426);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 44);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "HỦY";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(580, 496);
            this.Controls.Add(this.mainContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "AddScheduleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tạo Lịch Mới";
            this.mainContainer.ResumeLayout(false);
            this.mainContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainContainer;
        private System.Windows.Forms.Label lblTeacher;
        private SchoolTimetableWinForm.BorderedComboBox cmbTeacher;
        private System.Windows.Forms.Label lblClass;
        private SchoolTimetableWinForm.BorderedComboBox cmbClass;
        private System.Windows.Forms.Label lblTimeSlot;
        private SchoolTimetableWinForm.BorderedComboBox cmbTimeSlot;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpScheduleDate;
        private System.Windows.Forms.Label lblAssistant;
        private SchoolTimetableWinForm.BorderedComboBox cmbTeachingAssistant;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}