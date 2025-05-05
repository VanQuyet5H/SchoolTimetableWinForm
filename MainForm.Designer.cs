using System.Drawing;
using System.Windows.Forms;

namespace SchoolTimetableWinForm
{
    partial class MainForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýBảngCôngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainContainer = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSchoolInfo = new System.Windows.Forms.Label();
            this.cmbSchool = new SchoolTimetableWinForm.BorderedComboBox();
            this.panel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblWeekInfo = new System.Windows.Forms.Label();
            this.cmbWeek = new SchoolTimetableWinForm.BorderedComboBox();
            this.lblWeekStart = new System.Windows.Forms.Label();
            this.lblWeekEnd = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTeacherFilter = new SchoolTimetableWinForm.BorderedComboBox();
            this.lblWeekStartPicker = new System.Windows.Forms.Label();
            this.dtpWeekStart = new System.Windows.Forms.DateTimePicker();
            this.dgvTimetable = new System.Windows.Forms.DataGridView();
            this.panelPagination = new System.Windows.Forms.Panel();
            this.btnPreviousPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnToggleView = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimetable)).BeginInit();
            this.panelPagination.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1200, 36);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // manageToolStripMenuItem
            // 
            this.manageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLýBảngCôngToolStripMenuItem,
            this.quanToolStripMenuItem});
            this.manageToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            this.manageToolStripMenuItem.Size = new System.Drawing.Size(101, 32);
            this.manageToolStripMenuItem.Text = "Quản lý";
            // 
            // quảnLýBảngCôngToolStripMenuItem
            // 
            this.quảnLýBảngCôngToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.quảnLýBảngCôngToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.quảnLýBảngCôngToolStripMenuItem.Name = "quảnLýBảngCôngToolStripMenuItem";
            this.quảnLýBảngCôngToolStripMenuItem.Size = new System.Drawing.Size(392, 36);
            this.quảnLýBảngCôngToolStripMenuItem.Text = "Báo cáo cuối tháng";
            this.quảnLýBảngCôngToolStripMenuItem.Click += new System.EventHandler(this.quảnLýBảngCôngToolStripMenuItem_Click);
            // 
            // quanToolStripMenuItem
            // 
            this.quanToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.quanToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.quanToolStripMenuItem.Name = "quanToolStripMenuItem";
            this.quanToolStripMenuItem.Size = new System.Drawing.Size(392, 36);
            this.quanToolStripMenuItem.Text = "Quản lý thông tin của trường";
            this.quanToolStripMenuItem.Click += new System.EventHandler(this.ManageData_Click);
            // 
            // mainContainer
            // 
            this.mainContainer.BackColor = System.Drawing.Color.Transparent;
            this.mainContainer.ColumnCount = 1;
            this.mainContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainContainer.Controls.Add(this.panel1, 0, 0);
            this.mainContainer.Controls.Add(this.panel2, 0, 1);
            this.mainContainer.Controls.Add(this.panel3, 0, 2);
            this.mainContainer.Controls.Add(this.dgvTimetable, 0, 3);
            this.mainContainer.Controls.Add(this.panelPagination, 0, 4);
            this.mainContainer.Controls.Add(this.panel4, 0, 5);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 36);
            this.mainContainer.Margin = new System.Windows.Forms.Padding(5);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.RowCount = 6;
            this.mainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.mainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.mainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.mainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.mainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.mainContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.mainContainer.Size = new System.Drawing.Size(1200, 764);
            this.mainContainer.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.ColumnCount = 2;
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.panel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel1.Controls.Add(this.lblSchoolInfo, 0, 0);
            this.panel1.Controls.Add(this.cmbSchool, 1, 0);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(15);
            this.panel1.RowCount = 1;
            this.panel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel1.Size = new System.Drawing.Size(1194, 70);
            this.panel1.TabIndex = 1;
            // 
            // lblSchoolInfo
            // 
            this.lblSchoolInfo.AutoSize = true;
            this.lblSchoolInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSchoolInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblSchoolInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblSchoolInfo.Location = new System.Drawing.Point(18, 15);
            this.lblSchoolInfo.Margin = new System.Windows.Forms.Padding(3, 0, 5, 0);
            this.lblSchoolInfo.Name = "lblSchoolInfo";
            this.lblSchoolInfo.Size = new System.Drawing.Size(112, 38);
            this.lblSchoolInfo.TabIndex = 1;
            this.lblSchoolInfo.Text = "TRƯỜNG:";
            this.lblSchoolInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbSchool
            // 
            this.cmbSchool.BorderColor = System.Drawing.Color.Black;
            this.cmbSchool.BorderThickness = 1;
            this.cmbSchool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSchool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSchool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSchool.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbSchool.FormattingEnabled = true;
            this.cmbSchool.Location = new System.Drawing.Point(138, 18);
            this.cmbSchool.Name = "cmbSchool";
            this.cmbSchool.Size = new System.Drawing.Size(1036, 36);
            this.cmbSchool.TabIndex = 0;
            this.cmbSchool.SelectedIndexChanged += new System.EventHandler(this.cmbSchool_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.ColumnCount = 4;
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.panel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.panel2.Controls.Add(this.lblWeekInfo, 0, 0);
            this.panel2.Controls.Add(this.cmbWeek, 1, 0);
            this.panel2.Controls.Add(this.lblWeekStart, 2, 0);
            this.panel2.Controls.Add(this.lblWeekEnd, 3, 0);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 79);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(15);
            this.panel2.RowCount = 1;
            this.panel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel2.Size = new System.Drawing.Size(1194, 70);
            this.panel2.TabIndex = 2;
            // 
            // lblWeekInfo
            // 
            this.lblWeekInfo.AutoSize = true;
            this.lblWeekInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWeekInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblWeekInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblWeekInfo.Location = new System.Drawing.Point(18, 15);
            this.lblWeekInfo.Margin = new System.Windows.Forms.Padding(3, 0, 5, 0);
            this.lblWeekInfo.Name = "lblWeekInfo";
            this.lblWeekInfo.Size = new System.Drawing.Size(112, 38);
            this.lblWeekInfo.TabIndex = 1;
            this.lblWeekInfo.Text = "TUẦN:";
            this.lblWeekInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbWeek
            // 
            this.cmbWeek.BorderColor = System.Drawing.Color.Black;
            this.cmbWeek.BorderThickness = 1;
            this.cmbWeek.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbWeek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWeek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbWeek.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbWeek.FormattingEnabled = true;
            this.cmbWeek.Location = new System.Drawing.Point(138, 18);
            this.cmbWeek.Name = "cmbWeek";
            this.cmbWeek.Size = new System.Drawing.Size(306, 36);
            this.cmbWeek.TabIndex = 2;
            this.cmbWeek.SelectedIndexChanged += new System.EventHandler(this.cmbWeek_SelectedIndexChanged);
            // 
            // lblWeekStart
            // 
            this.lblWeekStart.AutoSize = true;
            this.lblWeekStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWeekStart.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWeekStart.Location = new System.Drawing.Point(450, 15);
            this.lblWeekStart.Name = "lblWeekStart";
            this.lblWeekStart.Size = new System.Drawing.Size(306, 38);
            this.lblWeekStart.TabIndex = 3;
            this.lblWeekStart.Text = "Ngày bắt đầu: ";
            this.lblWeekStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWeekEnd
            // 
            this.lblWeekEnd.AutoSize = true;
            this.lblWeekEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWeekEnd.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblWeekEnd.Location = new System.Drawing.Point(762, 15);
            this.lblWeekEnd.Name = "lblWeekEnd";
            this.lblWeekEnd.Size = new System.Drawing.Size(412, 38);
            this.lblWeekEnd.TabIndex = 4;
            this.lblWeekEnd.Text = "Ngày kết thúc: ";
            this.lblWeekEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.ColumnCount = 5;
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.panel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.panel3.Controls.Add(this.label1, 0, 0);
            this.panel3.Controls.Add(this.cmbTeacherFilter, 1, 0);
            this.panel3.Controls.Add(this.lblWeekStartPicker, 3, 0);
            this.panel3.Controls.Add(this.dtpWeekStart, 4, 0);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 155);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(15);
            this.panel3.RowCount = 1;
            this.panel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel3.Size = new System.Drawing.Size(1194, 70);
            this.panel3.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.label1.Location = new System.Drawing.Point(18, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "GIÁO VIÊN:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbTeacherFilter
            // 
            this.cmbTeacherFilter.BorderColor = System.Drawing.Color.Black;
            this.cmbTeacherFilter.BorderThickness = 1;
            this.cmbTeacherFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTeacherFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTeacherFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTeacherFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTeacherFilter.FormattingEnabled = true;
            this.cmbTeacherFilter.Location = new System.Drawing.Point(138, 18);
            this.cmbTeacherFilter.Name = "cmbTeacherFilter";
            this.cmbTeacherFilter.Size = new System.Drawing.Size(365, 36);
            this.cmbTeacherFilter.TabIndex = 1;
            this.cmbTeacherFilter.SelectedIndexChanged += new System.EventHandler(this.cmbTeacherFilter_SelectedIndexChanged);
            // 
            // lblWeekStartPicker
            // 
            this.lblWeekStartPicker.AutoSize = true;
            this.lblWeekStartPicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWeekStartPicker.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblWeekStartPicker.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblWeekStartPicker.Location = new System.Drawing.Point(880, 15);
            this.lblWeekStartPicker.Margin = new System.Windows.Forms.Padding(3, 0, 5, 0);
            this.lblWeekStartPicker.Name = "lblWeekStartPicker";
            this.lblWeekStartPicker.Size = new System.Drawing.Size(112, 38);
            this.lblWeekStartPicker.TabIndex = 2;
            this.lblWeekStartPicker.Text = "CHỌN NGÀY:";
            this.lblWeekStartPicker.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpWeekStart
            // 
            this.dtpWeekStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpWeekStart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpWeekStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpWeekStart.Location = new System.Drawing.Point(1002, 20);
            this.dtpWeekStart.Margin = new System.Windows.Forms.Padding(5);
            this.dtpWeekStart.Name = "dtpWeekStart";
            this.dtpWeekStart.Size = new System.Drawing.Size(170, 31);
            this.dtpWeekStart.TabIndex = 3;
            // 
            // dgvTimetable
            // 
            this.dgvTimetable.AllowUserToAddRows = false;
            this.dgvTimetable.AllowUserToDeleteRows = false;
            this.dgvTimetable.AllowUserToResizeRows = false;
            this.dgvTimetable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTimetable.BackgroundColor = System.Drawing.Color.White;
            this.dgvTimetable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.dgvTimetable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTimetable.ColumnHeadersHeight = 40;
            this.dgvTimetable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTimetable.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTimetable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTimetable.EnableHeadersVisualStyles = false;
            this.dgvTimetable.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvTimetable.Location = new System.Drawing.Point(3, 231);
            this.dgvTimetable.MultiSelect = false;
            this.dgvTimetable.Name = "dgvTimetable";
            this.dgvTimetable.RowHeadersVisible = false;
            this.dgvTimetable.RowHeadersWidth = 30;
            this.dgvTimetable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvTimetable.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTimetable.RowTemplate.Height = 35;
            this.dgvTimetable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvTimetable.Size = new System.Drawing.Size(1194, 414);
            this.dgvTimetable.TabIndex = 4;
            // 
            // panelPagination
            // 
            this.panelPagination.BackColor = System.Drawing.Color.White;
            this.panelPagination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPagination.Controls.Add(this.btnPreviousPage);
            this.panelPagination.Controls.Add(this.btnNextPage);
            this.panelPagination.Controls.Add(this.lblPageInfo);
            this.panelPagination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPagination.Location = new System.Drawing.Point(3, 651);
            this.panelPagination.Name = "panelPagination";
            this.panelPagination.Size = new System.Drawing.Size(1194, 32);
            this.panelPagination.TabIndex = 5;
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnPreviousPage.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPreviousPage.FlatAppearance.BorderSize = 0;
            this.btnPreviousPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviousPage.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnPreviousPage.ForeColor = System.Drawing.Color.White;
            this.btnPreviousPage.Location = new System.Drawing.Point(934, 0);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(125, 30);
            this.btnPreviousPage.TabIndex = 10;
            this.btnPreviousPage.Text = "Trang trước";
            this.btnPreviousPage.UseVisualStyleBackColor = false;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnNextPage.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNextPage.FlatAppearance.BorderSize = 0;
            this.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextPage.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnNextPage.ForeColor = System.Drawing.Color.White;
            this.btnNextPage.Location = new System.Drawing.Point(1059, 0);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(133, 30);
            this.btnNextPage.TabIndex = 11;
            this.btnNextPage.Text = "Trang sau";
            this.btnNextPage.UseVisualStyleBackColor = false;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPageInfo.Location = new System.Drawing.Point(3, 0);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(105, 28);
            this.lblPageInfo.TabIndex = 12;
            this.lblPageInfo.Text = "Trang 1 / 1";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnImport);
            this.panel4.Controls.Add(this.btnToggleView);
            this.panel4.Controls.Add(this.btnSave);
            this.panel4.Controls.Add(this.btnDelete);
            this.panel4.Controls.Add(this.btnExit);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 689);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1194, 72);
            this.panel4.TabIndex = 6;
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.Location = new System.Drawing.Point(249, 10);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(220, 45);
            this.btnImport.TabIndex = 4;
            this.btnImport.Text = "📥 Import dữ liệu";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnToggleView
            // 
            this.btnToggleView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnToggleView.FlatAppearance.BorderSize = 0;
            this.btnToggleView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleView.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnToggleView.ForeColor = System.Drawing.Color.White;
            this.btnToggleView.Location = new System.Drawing.Point(23, 10);
            this.btnToggleView.Name = "btnToggleView";
            this.btnToggleView.Size = new System.Drawing.Size(220, 45);
            this.btnToggleView.TabIndex = 3;
            this.btnToggleView.Text = "📋Xem Bảng Chấm Công";
            this.btnToggleView.UseVisualStyleBackColor = false;
            this.btnToggleView.Click += new System.EventHandler(this.btnToggleView_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(732, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 45);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "✏️ Tạo lịch";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(888, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(150, 45);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "🗑️ Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(1044, 10);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 45);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "❌ Thoát";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.mainContainer);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý thời khóa biểu của trường";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimetable)).EndInit();
            this.panelPagination.ResumeLayout(false);
            this.panelPagination.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem manageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quảnLýBảngCôngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quanToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel mainContainer;
        private System.Windows.Forms.TableLayoutPanel panel1;
        private System.Windows.Forms.Label lblSchoolInfo;
        private BorderedComboBox cmbSchool;
        private System.Windows.Forms.TableLayoutPanel panel2;
        private System.Windows.Forms.Label lblWeekInfo;
        private BorderedComboBox cmbWeek;
        private System.Windows.Forms.Label lblWeekStart;
        private System.Windows.Forms.Label lblWeekEnd;
        private System.Windows.Forms.TableLayoutPanel panel3;
        private System.Windows.Forms.Label label1;
        private BorderedComboBox cmbTeacherFilter;
        private System.Windows.Forms.Label lblWeekStartPicker;
        private System.Windows.Forms.DateTimePicker dtpWeekStart;
        private System.Windows.Forms.DataGridView dgvTimetable;
        private System.Windows.Forms.Panel panelPagination;
        private System.Windows.Forms.Button btnPreviousPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnToggleView;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExit;
    }
}