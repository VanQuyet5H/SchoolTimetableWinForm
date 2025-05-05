using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SchoolTimetableWinForm.Data
{
    public class SchoolTimetableContextFactory : IDesignTimeDbContextFactory<SchoolTimetableContext>
    {
        public SchoolTimetableContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchoolTimetableContext>();
            optionsBuilder.EnableSensitiveDataLogging().UseSqlServer("Server=.;Database=SchoolTimetable;Trusted_Connection=True;TrustServerCertificate=true;MultipleActiveResultSets=true");

            return new SchoolTimetableContext(optionsBuilder.Options);
        }
    }
}