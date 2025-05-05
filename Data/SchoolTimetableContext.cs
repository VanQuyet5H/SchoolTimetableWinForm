using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SchoolTimetableWinForm.Data
{
    public class SchoolTimetableContext : DbContext
    {
        public SchoolTimetableContext(DbContextOptions<SchoolTimetableContext> options) : base(options) { }

        public DbSet<School> Schools { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeachingAssistant> TeachingAssistants { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>()
                .HasCheckConstraint(
                    "CHK_Schedule_OffReason",
                    "([Status] = 'OFF' AND [OffReason] IS NOT NULL) OR ([Status] != 'OFF')"
                );

            // Cấu hình quan hệ và xóa CASCADE
            modelBuilder.Entity<Class>()
                .HasOne(c => c.School)
                .WithMany(s => s.Classes)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Teacher>()
                .HasOne(t => t.School)
                .WithMany(s => s.Teachers)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TeachingAssistant>()
                .HasOne(ta => ta.School)
                .WithMany(s => s.TeachingAssistants)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
    public class School
    {
        public int SchoolId { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolName { get; set; }

        public ICollection<Class> Classes { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<TeachingAssistant> TeachingAssistants { get; set; }
    }
    public class TeachingAssistant
    {
        public int TeachingAssistantId { get; set; }
        public string TeachingAssistantCode { get; set; }  // Ví dụ: "TA001"
        public string TeachingAssistantName { get; set; }
        public int SchoolId { get; set; }

        public School School { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
    public class Week
    {
        public int WeekId { get; set; }
        public string WeekName { get; set; }  // Ví dụ: "Tuần 1 - HK1 2023"
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int WeekNumber => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
            StartDate,
            CalendarWeekRule.FirstFourDayWeek,
            DayOfWeek.Monday
        );
        public ICollection<Schedule> Schedules { get; set; }
    }
    public class TimeSlot
    {
        public int TimeSlotId { get; set; }
        public string SlotName { get; set; }  // Ví dụ: "Tiết 1", "7:30-9:00"
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public ICollection<Schedule> Schedules { get; set; }
    }
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }  // Ví dụ: "10A1", "12C3"
        public int SchoolId { get; set; }

        public School School { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string TeacherCode { get; set; }  // Ví dụ: "GV001"
        public string TeacherName { get; set; }
        public int SchoolId { get; set; }

        public School School { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
    public class Schedule
    {
        public int ScheduleId { get; set; }

        public int SchoolId { get; set; }
        public int WeekId { get; set; }
        public int TimeSlotId { get; set; }
        public int? ClassId { get; set; }
        public int? TeacherId { get; set; }
        public int? TeachingAssistantId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Status { get; set; } = "ACTIVE";
        public string OffReason { get; set; }

        public School School { get; set; }
        public Week Week { get; set; }
        public TimeSlot TimeSlot { get; set; }
        public Class Class { get; set; }
        public Teacher Teacher { get; set; }
        public TeachingAssistant TeachingAssistant { get; set; }
    }
}