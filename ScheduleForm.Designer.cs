namespace SchoolTimetableWinForm
{
    partial class ScheduleForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSchool = new System.Windows.Forms.TabPage();
            this.tabWeek = new System.Windows.Forms.TabPage();
            this.tabTimeSlot = new System.Windows.Forms.TabPage();
            this.tabClass = new System.Windows.Forms.TabPage();
            this.dgvSchool = new System.Windows.Forms.DataGridView();
            this.txtSchoolName = new System.Windows.Forms.TextBox();
            this.lblSchoolName = new System.Windows.Forms.Label();
            this.btnAddSchool = new System.Windows.Forms.Button();
            this.btnUpdateSchool = new System.Windows.Forms.Button();
            this.btnDeleteSchool = new System.Windows.Forms.Button();
            this.dgvWeek = new System.Windows.Forms.DataGridView();
            this.dtpWeekStart = new System.Windows.Forms.DateTimePicker();
            this.lblWeekStart = new System.Windows.Forms.Label();
            this.btnAddWeek = new System.Windows.Forms.Button();
            this.btnUpdateWeek = new System.Windows.Forms.Button();
            this.btnDeleteWeek = new System.Windows.Forms.Button();
            this.dgvTimeSlot = new System.Windows.Forms.DataGridView();
            this.txtSlotName = new System.Windows.Forms.TextBox();
            this.lblSlotName = new System.Windows.Forms.Label();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.btnAddTimeSlot = new System.Windows.Forms.Button();
            this.btnUpdateTimeSlot = new System.Windows.Forms.Button();
            this.btnDeleteTimeSlot = new System.Windows.Forms.Button();
            this.dgvClass = new System.Windows.Forms.DataGridView();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.lblClassName = new System.Windows.Forms.Label();
            this.cmbSchool = new System.Windows.Forms.ComboBox();
            this.lblClassSchool = new System.Windows.Forms.Label();
            this.btnAddClass = new System.Windows.Forms.Button();
            this.btnUpdateClass = new System.Windows.Forms.Button();
            this.btnDeleteClass = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.tabControl.SuspendLayout();
            this.tabSchool.SuspendLayout();
            this.tabWeek.SuspendLayout();
            this.tabTimeSlot.SuspendLayout();
            this.tabClass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchool)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeSlot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClass)).BeginInit();
            this.SuspendLayout();

            // tabControl
            this.tabControl.Controls.Add(this.tabSchool);
            this.tabControl.Controls.Add(this.tabWeek);
            this.tabControl.Controls.Add(this.tabTimeSlot);
            this.tabControl.Controls.Add(this.tabClass);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(760, 400);
            this.tabControl.TabIndex = 0;

            // tabSchool
            this.tabSchool.Controls.Add(this.dgvSchool);
            this.tabSchool.Controls.Add(this.txtSchoolName);
            this.tabSchool.Controls.Add(this.lblSchoolName);
            this.tabSchool.Controls.Add(this.btnAddSchool);
            this.tabSchool.Controls.Add(this.btnUpdateSchool);
            this.tabSchool.Controls.Add(this.btnDeleteSchool);
            this.tabSchool.Location = new System.Drawing.Point(4, 22);
            this.tabSchool.Name = "tabSchool";
            this.tabSchool.Padding = new System.Windows.Forms.Padding(3);
            this.tabSchool.Size = new System.Drawing.Size(752, 374);
            this.tabSchool.TabIndex = 0;
            this.tabSchool.Text = "Trường";

            // dgvSchool
            this.dgvSchool.AllowUserToAddRows = false;
            this.dgvSchool.AllowUserToDeleteRows = false;
            this.dgvSchool.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchool.Location = new System.Drawing.Point(10, 10);
            this.dgvSchool.Name = "dgvSchool";
            this.dgvSchool.ReadOnly = true;
            this.dgvSchool.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSchool.Size = new System.Drawing.Size(400, 350);
            this.dgvSchool.TabIndex = 0;
            this.dgvSchool.SelectionChanged += new System.EventHandler(this.DgvSchool_SelectionChanged);

            // lblSchoolName
            this.lblSchoolName.AutoSize = true;
            this.lblSchoolName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSchoolName.Location = new System.Drawing.Point(420, 20);
            this.lblSchoolName.Name = "lblSchoolName";
            this.lblSchoolName.Size = new System.Drawing.Size(70, 15);
            this.lblSchoolName.TabIndex = 1;
            this.lblSchoolName.Text = "Tên trường:";

            // txtSchoolName
            this.txtSchoolName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSchoolName.Location = new System.Drawing.Point(420, 40);
            this.txtSchoolName.Name = "txtSchoolName";
            this.txtSchoolName.Size = new System.Drawing.Size(300, 23);
            this.txtSchoolName.TabIndex = 2;

            // btnAddSchool
            this.btnAddSchool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnAddSchool.FlatAppearance.BorderSize = 0;
            this.btnAddSchool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSchool.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddSchool.ForeColor = System.Drawing.Color.White;
            this.btnAddSchool.Location = new System.Drawing.Point(420, 80);
            this.btnAddSchool.Name = "btnAddSchool";
            this.btnAddSchool.Size = new System.Drawing.Size(90, 30);
            this.btnAddSchool.TabIndex = 3;
            this.btnAddSchool.Text = "Thêm";
            this.btnAddSchool.UseVisualStyleBackColor = false;
            this.btnAddSchool.Click += new System.EventHandler(this.BtnAddSchool_Click);

            // btnUpdateSchool
            this.btnUpdateSchool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnUpdateSchool.FlatAppearance.BorderSize = 0;
            this.btnUpdateSchool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateSchool.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnUpdateSchool.ForeColor = System.Drawing.Color.White;
            this.btnUpdateSchool.Location = new System.Drawing.Point(520, 80);
            this.btnUpdateSchool.Name = "btnUpdateSchool";
            this.btnUpdateSchool.Size = new System.Drawing.Size(90, 30);
            this.btnUpdateSchool.TabIndex = 4;
            this.btnUpdateSchool.Text = "Sửa";
            this.btnUpdateSchool.UseVisualStyleBackColor = false;
            this.btnUpdateSchool.Click += new System.EventHandler(this.BtnUpdateSchool_Click);

            // btnDeleteSchool
            this.btnDeleteSchool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDeleteSchool.FlatAppearance.BorderSize = 0;
            this.btnDeleteSchool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteSchool.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteSchool.ForeColor = System.Drawing.Color.White;
            this.btnDeleteSchool.Location = new System.Drawing.Point(620, 80);
            this.btnDeleteSchool.Name = "btnDeleteSchool";
            this.btnDeleteSchool.Size = new System.Drawing.Size(90, 30);
            this.btnDeleteSchool.TabIndex = 5;
            this.btnDeleteSchool.Text = "Xóa";
            this.btnDeleteSchool.UseVisualStyleBackColor = false;
            this.btnDeleteSchool.Click += new System.EventHandler(this.BtnDeleteSchool_Click);

            // tabWeek
            this.tabWeek.Controls.Add(this.dgvWeek);
            this.tabWeek.Controls.Add(this.dtpWeekStart);
            this.tabWeek.Controls.Add(this.lblWeekStart);
            this.tabWeek.Controls.Add(this.btnAddWeek);
            this.tabWeek.Controls.Add(this.btnUpdateWeek);
            this.tabWeek.Controls.Add(this.btnDeleteWeek);
            this.tabWeek.Location = new System.Drawing.Point(4, 22);
            this.tabWeek.Name = "tabWeek";
            this.tabWeek.Padding = new System.Windows.Forms.Padding(3);
            this.tabWeek.Size = new System.Drawing.Size(752, 374);
            this.tabWeek.TabIndex = 1;
            this.tabWeek.Text = "Tuần";

            // dgvWeek
            this.dgvWeek.AllowUserToAddRows = false;
            this.dgvWeek.AllowUserToDeleteRows = false;
            this.dgvWeek.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWeek.Location = new System.Drawing.Point(10, 10);
            this.dgvWeek.Name = "dgvWeek";
            this.dgvWeek.ReadOnly = true;
            this.dgvWeek.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWeek.Size = new System.Drawing.Size(400, 350);
            this.dgvWeek.TabIndex = 0;
            this.dgvWeek.SelectionChanged += new System.EventHandler(this.DgvWeek_SelectionChanged);

            // lblWeekStart
            this.lblWeekStart.AutoSize = true;
            this.lblWeekStart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWeekStart.Location = new System.Drawing.Point(420, 20);
            this.lblWeekStart.Name = "lblWeekStart";
            this.lblWeekStart.Size = new System.Drawing.Size(70, 15);
            this.lblWeekStart.TabIndex = 1;
            this.lblWeekStart.Text = "Ngày bắt đầu:";

            // dtpWeekStart
            this.dtpWeekStart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpWeekStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpWeekStart.Location = new System.Drawing.Point(420, 40);
            this.dtpWeekStart.Name = "dtpWeekStart";
            this.dtpWeekStart.Size = new System.Drawing.Size(300, 23);
            this.dtpWeekStart.TabIndex = 2;

            // btnAddWeek
            this.btnAddWeek.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnAddWeek.FlatAppearance.BorderSize = 0;
            this.btnAddWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddWeek.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddWeek.ForeColor = System.Drawing.Color.White;
            this.btnAddWeek.Location = new System.Drawing.Point(420, 80);
            this.btnAddWeek.Name = "btnAddWeek";
            this.btnAddWeek.Size = new System.Drawing.Size(90, 30);
            this.btnAddWeek.TabIndex = 3;
            this.btnAddWeek.Text = "Thêm";
            this.btnAddWeek.UseVisualStyleBackColor = false;
            this.btnAddWeek.Click += new System.EventHandler(this.BtnAddWeek_Click);

            // btnUpdateWeek
            this.btnUpdateWeek.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnUpdateWeek.FlatAppearance.BorderSize = 0;
            this.btnUpdateWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateWeek.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnUpdateWeek.ForeColor = System.Drawing.Color.White;
            this.btnUpdateWeek.Location = new System.Drawing.Point(520, 80);
            this.btnUpdateWeek.Name = "btnUpdateWeek";
            this.btnUpdateWeek.Size = new System.Drawing.Size(90, 30);
            this.btnUpdateWeek.TabIndex = 4;
            this.btnUpdateWeek.Text = "Sửa";
            this.btnUpdateWeek.UseVisualStyleBackColor = false;
            this.btnUpdateWeek.Click += new System.EventHandler(this.BtnUpdateWeek_Click);

            // btnDeleteWeek
            this.btnDeleteWeek.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDeleteWeek.FlatAppearance.BorderSize = 0;
            this.btnDeleteWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteWeek.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteWeek.ForeColor = System.Drawing.Color.White;
            this.btnDeleteWeek.Location = new System.Drawing.Point(620, 80);
            this.btnDeleteWeek.Name = "btnDeleteWeek";
            this.btnDeleteWeek.Size = new System.Drawing.Size(90, 30);
            this.btnDeleteWeek.TabIndex = 5;
            this.btnDeleteWeek.Text = "Xóa";
            this.btnDeleteWeek.UseVisualStyleBackColor = false;
            this.btnDeleteWeek.Click += new System.EventHandler(this.BtnDeleteWeek_Click);

            // tabTimeSlot
            this.tabTimeSlot.Controls.Add(this.dgvTimeSlot);
            this.tabTimeSlot.Controls.Add(this.txtSlotName);
            this.tabTimeSlot.Controls.Add(this.lblSlotName);
            this.tabTimeSlot.Controls.Add(this.dtpStartTime);
            this.tabTimeSlot.Controls.Add(this.lblStartTime);
            this.tabTimeSlot.Controls.Add(this.dtpEndTime);
            this.tabTimeSlot.Controls.Add(this.lblEndTime);
            this.tabTimeSlot.Controls.Add(this.btnAddTimeSlot);
            this.tabTimeSlot.Controls.Add(this.btnUpdateTimeSlot);
            this.tabTimeSlot.Controls.Add(this.btnDeleteTimeSlot);
            this.tabTimeSlot.Location = new System.Drawing.Point(4, 22);
            this.tabTimeSlot.Name = "tabTimeSlot";
            this.tabTimeSlot.Padding = new System.Windows.Forms.Padding(3);
            this.tabTimeSlot.Size = new System.Drawing.Size(752, 374);
            this.tabTimeSlot.TabIndex = 2;
            this.tabTimeSlot.Text = "Tiết";

            // dgvTimeSlot
            this.dgvTimeSlot.AllowUserToAddRows = false;
            this.dgvTimeSlot.AllowUserToDeleteRows = false;
            this.dgvTimeSlot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimeSlot.Location = new System.Drawing.Point(10, 10);
            this.dgvTimeSlot.Name = "dgvTimeSlot";
            this.dgvTimeSlot.ReadOnly = true;
            this.dgvTimeSlot.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTimeSlot.Size = new System.Drawing.Size(400, 350);
            this.dgvTimeSlot.TabIndex = 0;
            this.dgvTimeSlot.SelectionChanged += new System.EventHandler(this.DgvTimeSlot_SelectionChanged);

            // lblSlotName
            this.lblSlotName.AutoSize = true;
            this.lblSlotName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSlotName.Location = new System.Drawing.Point(420, 20);
            this.lblSlotName.Name = "lblSlotName";
            this.lblSlotName.Size = new System.Drawing.Size(70, 15);
            this.lblSlotName.TabIndex = 1;
            this.lblSlotName.Text = "Tên tiết:";

            // txtSlotName
            this.txtSlotName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSlotName.Location = new System.Drawing.Point(420, 40);
            this.txtSlotName.Name = "txtSlotName";
            this.txtSlotName.Size = new System.Drawing.Size(300, 23);
            this.txtSlotName.TabIndex = 2;

            // lblStartTime
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStartTime.Location = new System.Drawing.Point(420, 70);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(70, 15);
            this.lblStartTime.TabIndex = 3;
            this.lblStartTime.Text = "Bắt đầu:";

            // dtpStartTime
            this.dtpStartTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Location = new System.Drawing.Point(420, 90);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(300, 23);
            this.dtpStartTime.TabIndex = 4;

            // lblEndTime
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEndTime.Location = new System.Drawing.Point(420, 120);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(70, 15);
            this.lblEndTime.TabIndex = 5;
            this.lblEndTime.Text = "Kết thúc:";

            // dtpEndTime
            this.dtpEndTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpEndTime.ShowUpDown = true;
            this.dtpEndTime.Location = new System.Drawing.Point(420, 140);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(300, 23);
            this.dtpEndTime.TabIndex = 6;

            // btnAddTimeSlot
            this.btnAddTimeSlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnAddTimeSlot.FlatAppearance.BorderSize = 0;
            this.btnAddTimeSlot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTimeSlot.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddTimeSlot.ForeColor = System.Drawing.Color.White;
            this.btnAddTimeSlot.Location = new System.Drawing.Point(420, 180);
            this.btnAddTimeSlot.Name = "btnAddTimeSlot";
            this.btnAddTimeSlot.Size = new System.Drawing.Size(90, 30);
            this.btnAddTimeSlot.TabIndex = 7;
            this.btnAddTimeSlot.Text = "Thêm";
            this.btnAddTimeSlot.UseVisualStyleBackColor = false;
            this.btnAddTimeSlot.Click += new System.EventHandler(this.BtnAddTimeSlot_Click);

            // btnUpdateTimeSlot
            this.btnUpdateTimeSlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnUpdateTimeSlot.FlatAppearance.BorderSize = 0;
            this.btnUpdateTimeSlot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateTimeSlot.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnUpdateTimeSlot.ForeColor = System.Drawing.Color.White;
            this.btnUpdateTimeSlot.Location = new System.Drawing.Point(520, 180);
            this.btnUpdateTimeSlot.Name = "btnUpdateTimeSlot";
            this.btnUpdateTimeSlot.Size = new System.Drawing.Size(90, 30);
            this.btnUpdateTimeSlot.TabIndex = 8;
            this.btnUpdateTimeSlot.Text = "Sửa";
            this.btnUpdateTimeSlot.UseVisualStyleBackColor = false;
            this.btnUpdateTimeSlot.Click += new System.EventHandler(this.BtnUpdateTimeSlot_Click);

            // btnDeleteTimeSlot
            this.btnDeleteTimeSlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDeleteTimeSlot.FlatAppearance.BorderSize = 0;
            this.btnDeleteTimeSlot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteTimeSlot.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteTimeSlot.ForeColor = System.Drawing.Color.White;
            this.btnDeleteTimeSlot.Location = new System.Drawing.Point(620, 180);
            this.btnDeleteTimeSlot.Name = "btnDeleteTimeSlot";
            this.btnDeleteTimeSlot.Size = new System.Drawing.Size(90, 30);
            this.btnDeleteTimeSlot.TabIndex = 9;
            this.btnDeleteTimeSlot.Text = "Xóa";
            this.btnDeleteTimeSlot.UseVisualStyleBackColor = false;
            this.btnDeleteTimeSlot.Click += new System.EventHandler(this.BtnDeleteTimeSlot_Click);

            // tabClass
            this.tabClass.Controls.Add(this.dgvClass);
            this.tabClass.Controls.Add(this.txtClassName);
            this.tabClass.Controls.Add(this.lblClassName);
            this.tabClass.Controls.Add(this.cmbSchool);
            this.tabClass.Controls.Add(this.lblClassSchool);
            this.tabClass.Controls.Add(this.btnAddClass);
            this.tabClass.Controls.Add(this.btnUpdateClass);
            this.tabClass.Controls.Add(this.btnDeleteClass);
            this.tabClass.Location = new System.Drawing.Point(4, 22);
            this.tabClass.Name = "tabClass";
            this.tabClass.Padding = new System.Windows.Forms.Padding(3);
            this.tabClass.Size = new System.Drawing.Size(752, 374);
            this.tabClass.TabIndex = 3;
            this.tabClass.Text = "Lớp";

            // dgvClass
            this.dgvClass.AllowUserToAddRows = false;
            this.dgvClass.AllowUserToDeleteRows = false;
            this.dgvClass.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClass.Location = new System.Drawing.Point(10, 10);
            this.dgvClass.Name = "dgvClass";
            this.dgvClass.ReadOnly = true;
            this.dgvClass.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClass.Size = new System.Drawing.Size(400, 350);
            this.dgvClass.TabIndex = 0;
            this.dgvClass.SelectionChanged += new System.EventHandler(this.DgvClass_SelectionChanged);

            // lblClassName
            this.lblClassName.AutoSize = true;
            this.lblClassName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblClassName.Location = new System.Drawing.Point(420, 20);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(70, 15);
            this.lblClassName.TabIndex = 1;
            this.lblClassName.Text = "Tên lớp:";

            // txtClassName
            this.txtClassName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtClassName.Location = new System.Drawing.Point(420, 40);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(300, 23);
            this.txtClassName.TabIndex = 2;

            // lblClassSchool
            this.lblClassSchool.AutoSize = true;
            this.lblClassSchool.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblClassSchool.Location = new System.Drawing.Point(420, 70);
            this.lblClassSchool.Name = "lblClassSchool";
            this.lblClassSchool.Size = new System.Drawing.Size(70, 15);
            this.lblClassSchool.TabIndex = 3;
            this.lblClassSchool.Text = "Trường:";

            // cmbSchool
            this.cmbSchool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSchool.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbSchool.Location = new System.Drawing.Point(420, 90);
            this.cmbSchool.Name = "cmbSchool";
            this.cmbSchool.Size = new System.Drawing.Size(300, 23);
            this.cmbSchool.TabIndex = 4;

            // btnAddClass
            this.btnAddClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnAddClass.FlatAppearance.BorderSize = 0;
            this.btnAddClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddClass.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddClass.ForeColor = System.Drawing.Color.White;
            this.btnAddClass.Location = new System.Drawing.Point(420, 130);
            this.btnAddClass.Name = "btnAddClass";
            this.btnAddClass.Size = new System.Drawing.Size(90, 30);
            this.btnAddClass.TabIndex = 5;
            this.btnAddClass.Text = "Thêm";
            this.btnAddClass.UseVisualStyleBackColor = false;
            this.btnAddClass.Click += new System.EventHandler(this.BtnAddClass_Click);

            // btnUpdateClass
            this.btnUpdateClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnUpdateClass.FlatAppearance.BorderSize = 0;
            this.btnUpdateClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateClass.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnUpdateClass.ForeColor = System.Drawing.Color.White;
            this.btnUpdateClass.Location = new System.Drawing.Point(520, 130);
            this.btnUpdateClass.Name = "btnUpdateClass";
            this.btnUpdateClass.Size = new System.Drawing.Size(90, 30);
            this.btnUpdateClass.TabIndex = 6;
            this.btnUpdateClass.Text = "Sửa";
            this.btnUpdateClass.UseVisualStyleBackColor = false;
            this.btnUpdateClass.Click += new System.EventHandler(this.BtnUpdateClass_Click);

            // btnDeleteClass
            this.btnDeleteClass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDeleteClass.FlatAppearance.BorderSize = 0;
            this.btnDeleteClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteClass.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeleteClass.ForeColor = System.Drawing.Color.White;
            this.btnDeleteClass.Location = new System.Drawing.Point(620, 130);
            this.btnDeleteClass.Name = "btnDeleteClass";
            this.btnDeleteClass.Size = new System.Drawing.Size(90, 30);
            this.btnDeleteClass.TabIndex = 7;
            this.btnDeleteClass.Text = "Xóa";
            this.btnDeleteClass.UseVisualStyleBackColor = false;
            this.btnDeleteClass.Click += new System.EventHandler(this.BtnDeleteClass_Click);

            // btnClose
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(672, 418);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);

            // ManagementForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản Lý Dữ Liệu";
            //this.Load += new System.EventHandler(this.ScheduleForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabSchool.ResumeLayout(false);
            this.tabSchool.PerformLayout();
            this.tabWeek.ResumeLayout(false);
            this.tabWeek.PerformLayout();
            this.tabTimeSlot.ResumeLayout(false);
            this.tabTimeSlot.PerformLayout();
            this.tabClass.ResumeLayout(false);
            this.tabClass.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchool)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWeek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimeSlot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClass)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabSchool;
        private System.Windows.Forms.TabPage tabWeek;
        private System.Windows.Forms.TabPage tabTimeSlot;
        private System.Windows.Forms.TabPage tabClass;
        private System.Windows.Forms.DataGridView dgvSchool;
        private System.Windows.Forms.TextBox txtSchoolName;
        private System.Windows.Forms.Label lblSchoolName;
        private System.Windows.Forms.Button btnAddSchool;
        private System.Windows.Forms.Button btnUpdateSchool;
        private System.Windows.Forms.Button btnDeleteSchool;
        private System.Windows.Forms.DataGridView dgvWeek;
        private System.Windows.Forms.DateTimePicker dtpWeekStart;
        private System.Windows.Forms.Label lblWeekStart;
        private System.Windows.Forms.Button btnAddWeek;
        private System.Windows.Forms.Button btnUpdateWeek;
        private System.Windows.Forms.Button btnDeleteWeek;
        private System.Windows.Forms.DataGridView dgvTimeSlot;
        private System.Windows.Forms.TextBox txtSlotName;
        private System.Windows.Forms.Label lblSlotName;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.Button btnAddTimeSlot;
        private System.Windows.Forms.Button btnUpdateTimeSlot;
        private System.Windows.Forms.Button btnDeleteTimeSlot;
        private System.Windows.Forms.DataGridView dgvClass;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label lblClassName;
        private System.Windows.Forms.ComboBox cmbSchool;
        private System.Windows.Forms.Label lblClassSchool;
        private System.Windows.Forms.Button btnAddClass;
        private System.Windows.Forms.Button btnUpdateClass;
        private System.Windows.Forms.Button btnDeleteClass;
        private System.Windows.Forms.Button btnClose;
    }
}