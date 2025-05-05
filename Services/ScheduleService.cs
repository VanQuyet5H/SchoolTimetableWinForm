using Microsoft.EntityFrameworkCore;
using SchoolTimetableWinForm.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolTimetableWinForm.Services
{
    /// <summary>
    /// Service for managing school schedules and related data.
    /// </summary>
    public class ScheduleService
    {
        private readonly SchoolTimetableContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduleService"/> class.
        /// </summary>
        /// <param name="context">The database context for accessing school timetable data.</param>
        public ScheduleService(SchoolTimetableContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Retrieves all schools from the database.
        /// </summary>
        /// <returns>A list of schools.</returns>
        public async Task<List<School>> GetSchoolsAsync()
        {
            var schools = await _context.Schools.ToListAsync();
            Debug.WriteLine($"GetSchoolsAsync: {schools.Count} schools loaded");
            return schools;
        }

        /// <summary>
        /// Retrieves all weeks from the database.
        /// </summary>
        /// <returns>A list of weeks.</returns>
        public async Task<List<Week>> GetWeeksAsync()
        {
            try
            {
                return await _context.Weeks
                    .AsNoTracking()
                    .ToListAsync()
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve weeks.", ex);
            }
        }

        /// <summary>
        /// Retrieves morning and afternoon time slots from the database.
        /// </summary>
        /// <returns>A tuple containing lists of morning and afternoon time slots.</returns>
        public async Task<(List<TimeSlot> morningSlots, List<TimeSlot> afternoonSlots)> GetTimeSlotsAsync()
        {
            try
            {
                var timeSlots = await _context.TimeSlots
                    .AsNoTracking()
                    .OrderBy(ts => ts.StartTime)
                    .ToListAsync()
                    .ConfigureAwait(false);

                var morningSlots = timeSlots.Where(ts => ts.StartTime < TimeSpan.FromHours(12)).ToList();
                var afternoonSlots = timeSlots.Where(ts => ts.StartTime >= TimeSpan.FromHours(12)).ToList();

                return (morningSlots, afternoonSlots);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve time slots.", ex);
            }
        }

        /// <summary>
        /// Retrieves schedules for a specific school and week, including formatted text for display.
        /// </summary>
        /// <param name="schoolId">The ID of the school.</param>
        /// <param name="weekId">The ID of the week.</param>
        /// <returns>A list of tuples containing schedules and their formatted text (TeacherName - ClassName).</returns>
        public async Task<(List<(Schedule schedule, string formattedText)> schedules, int totalCount)> GetSchedulesAsync(int schoolId, int weekId, int page = 1, int pageSize = 100)
        {
            try
            {
                if (schoolId <= 0) throw new ArgumentException("SchoolId must be greater than 0.", nameof(schoolId));
                if (weekId <= 0) throw new ArgumentException("WeekId must be greater than 0.", nameof(weekId));
                if (page < 1) page = 1;
                if (pageSize < 1) pageSize = 100;

                Debug.WriteLine($"GetSchedulesAsync: Querying schedules for SchoolId={schoolId}, WeekId={weekId}, Page={page}, PageSize={pageSize}");

                var query = _context.Schedules
                    .AsNoTracking()
                    .Where(s => s.SchoolId == schoolId && s.WeekId == weekId)
                    .Include(s => s.Teacher)
                    .Include(s => s.Class)
                    .Include(s => s.TeachingAssistant); // Thêm cả trợ giảng luôn

                int totalCount = await query.CountAsync();
                Debug.WriteLine($"GetSchedulesAsync: Total schedules count={totalCount}");

                if (totalCount == 0)
                {
                    Debug.WriteLine("GetSchedulesAsync: No schedules found for the given criteria.");
                    return (new List<(Schedule schedule, string formattedText)>(), totalCount);
                }

                var schedules = await query
                    .OrderBy(s => s.ScheduleDate)
                    .ThenBy(s => s.TimeSlotId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                Debug.WriteLine($"GetSchedulesAsync: Retrieved {schedules.Count} schedules for page {page}");

                var result = schedules.Select(s =>
                {
                    string teacherName = s.Teacher?.TeacherName ?? "Không rõ";
                    string className = s.Class?.ClassName ?? "Không rõ";
                    string assistantCode = s.TeachingAssistant?.TeachingAssistantCode ?? "Không có";

                    string formattedText = $"{teacherName} - {className} - {assistantCode}";

                    return (s, formattedText);
                }).ToList();

                return (result, totalCount);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GetSchedulesAsync: Error - {ex.Message}");
                throw new InvalidOperationException($"Failed to retrieve schedules for SchoolId: {schoolId}, WeekId: {weekId}, Page: {page}, PageSize: {pageSize}.", ex);
            }
        }


        /// <summary>
        /// Checks if the specified entity types have data in the database.
        /// </summary>
        /// <param name="entityTypes">The types of entities to check (e.g., "Schools", "Weeks"). If empty, checks all entities.</param>
        /// <returns>True if all specified entities have data; otherwise, false.</returns>
        public async Task<bool> HasDataAsync(params string[] entityTypes)
        {
            try
            {
                if (entityTypes == null || !entityTypes.Any())
                {
                    entityTypes = new[] { "Schools", "Weeks", "TimeSlots", "Classes", "Teachers", "TeachingAssistants" };
                }

                foreach (var entityType in entityTypes)
                {
                    bool hasData;
                    switch (entityType.ToLower())
                    {
                        case "schools":
                            hasData = await _context.Schools.AnyAsync().ConfigureAwait(false);
                            break;
                        case "weeks":
                            hasData = await _context.Weeks.AnyAsync().ConfigureAwait(false);
                            break;
                        case "timeslots":
                            hasData = await _context.TimeSlots.AnyAsync().ConfigureAwait(false);
                            break;
                        case "classes":
                            hasData = await _context.Classes.AnyAsync().ConfigureAwait(false);
                            break;
                        case "teachers":
                            hasData = await _context.Teachers.AnyAsync().ConfigureAwait(false);
                            break;
                        case "teachingassistants":
                            hasData = await _context.TeachingAssistants.AnyAsync().ConfigureAwait(false);
                            break;
                        default:
                            throw new ArgumentException($"Invalid entity type: {entityType}");
                    }

                    if (!hasData)
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to check data existence.", ex);
            }
        }
       
    }
}