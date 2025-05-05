using SchoolTimetableWinForm.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SchoolTimetableWinForm
{
    public partial class TeacherSelectionForm : Form
    {
        public Teacher SelectedTeacher { get; private set; }

        public TeacherSelectionForm(List<Teacher> availableTeachers, string message)
        {
            InitializeComponent();
            PopulateTeachers(availableTeachers);
            lblMessage.Text = message;
            Debug.WriteLine($"TeacherSelectionForm initialized with {availableTeachers?.Count ?? 0} teachers.");
        }

        private void PopulateTeachers(List<Teacher> teachers)
        {
            if (teachers == null || !teachers.Any())
            {
                Debug.WriteLine("PopulateTeachers: No teachers available.");
                cmbTeachers.Enabled = false;
                lblMessage.Text += "\nKhông có giáo viên trống để chọn.";
                return;
            }

            cmbTeachers.DataSource = teachers;
            cmbTeachers.DisplayMember = "TeacherName";
            cmbTeachers.ValueMember = "TeacherId";
            cmbTeachers.SelectedIndex = 0;
            btnConfirm.Enabled = true;
            Debug.WriteLine($"PopulateTeachers: Loaded {teachers.Count} teachers.");
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (cmbTeachers.SelectedItem is Teacher selectedTeacher)
            {
                SelectedTeacher = selectedTeacher;
                DialogResult = DialogResult.OK;
                Debug.WriteLine($"Teacher selected: ID={selectedTeacher.TeacherId}, Name={selectedTeacher.TeacherName}");
                Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một giáo viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Debug.WriteLine("BtnConfirm_Click: No teacher selected.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}