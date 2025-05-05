namespace SchoolTimetableWinForm
{
    partial class ImportDataForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtpMonthYear = new System.Windows.Forms.DateTimePicker();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtpMonthYear
            // 
            this.dtpMonthYear.CustomFormat = "MM/yyyy";
            this.dtpMonthYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMonthYear.Location = new System.Drawing.Point(20, 20);
            this.dtpMonthYear.Name = "dtpMonthYear";
            this.dtpMonthYear.ShowUpDown = true;
            this.dtpMonthYear.Size = new System.Drawing.Size(100, 20);
            this.dtpMonthYear.TabIndex = 0;
            this.dtpMonthYear.Value = new System.DateTime(2025, 4, 23, 13, 59, 53, 779);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(20, 60);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(100, 30);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import Dữ Liệu";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(130, 60);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ImportDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 211);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.dtpMonthYear);
            this.Name = "ImportDataForm";
            this.Text = "Import Dữ Liệu";
            this.ResumeLayout(false);

        }

        #endregion

        
    }
}