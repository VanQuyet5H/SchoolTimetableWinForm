using Microsoft.EntityFrameworkCore;
using SchoolTimetableWinForm.Data;
using System;
using System.Windows.Forms;

namespace SchoolTimetableWinForm
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Configure DbContextOptions for SchoolTimetableContext
            var optionsBuilder = new DbContextOptionsBuilder<SchoolTimetableContext>();
            optionsBuilder.UseSqlServer(
                "Server=.;Database=SchoolTimetable;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true");

            // Pass options to MainForm
            Application.Run(new MainForm(optionsBuilder.Options));
        }
    }
}