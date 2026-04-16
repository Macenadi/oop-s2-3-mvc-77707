using Global_College.domain.Models.Faculty;
using Global_College.mvc.Data;
using Global_College.mvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global_College.mvc.Controllers
{
    [Authorize(Roles = "Administrator,Faculty")]
    public class AttendanceRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AttendanceRecords
        public async Task<IActionResult> Index(int? branchId, int? branchCourseId, DateOnly? date)
        {
            var model = new AttendanceIndexViewModel
            {
                BranchId = branchId,
                BranchCourseId = branchCourseId,
                Date = date ?? DateOnly.FromDateTime(DateTime.Today)
            };

            var today = DateOnly.FromDateTime(DateTime.Today);

            if (model.Date > today)
            {
                model.Date = today;
                ModelState.AddModelError("Date", "Future dates are not allowed for attendance.");
            }

            await LoadFilters(model);

            if (model.BranchCourseId.HasValue)
            {
                var enrolments = await _context.CourseEnrolments
                    .Include(ce => ce.StudentProfile)
                    .Include(ce => ce.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                    .Include(ce => ce.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                    .Where(ce => ce.BranchCourseId == model.BranchCourseId.Value
                                 && ce.Status == "Enrolled")
                    .OrderBy(ce => ce.StudentProfile.FullName)
                    .ToListAsync();

                var attendanceRecords = await _context.AttendanceRecords
                    .Where(a => a.Date == model.Date)
                    .ToListAsync();

                model.Students = enrolments.Select(ce =>
                {
                    var existingAttendance = attendanceRecords
                        .FirstOrDefault(a => a.CourseEnrolmentId == ce.Id);

                    return new AttendanceStudentItemViewModel
                    {
                        CourseEnrolmentId = ce.Id,
                        StudentProfileId = ce.StudentProfileId,
                        StudentNumber = ce.StudentProfile.StudentNumber,
                        FullName = ce.StudentProfile.FullName,
                        Present = existingAttendance?.Present ?? false,
                        AttendanceRecordId = existingAttendance?.Id
                    };
                }).ToList();
            }

            return View(model);
        }

        // POST: AttendanceRecords/SaveAttendance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAttendance(AttendanceIndexViewModel model)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            if (model.Date > today)
            {
                ModelState.AddModelError("Date", "Future dates are not allowed for attendance.");
            }

            if (!model.BranchId.HasValue)
            {
                ModelState.AddModelError("BranchId", "Please select a branch.");
            }

            if (!model.BranchCourseId.HasValue)
            {
                ModelState.AddModelError("BranchCourseId", "Please select a class/group.");
            }

            if (model.Students == null || !model.Students.Any())
            {
                ModelState.AddModelError("", "No students found for the selected class/group.");
            }

            var selectedDate = model.Date;

            var courseEnrolmentIds = model.Students?
                .Select(s => s.CourseEnrolmentId)
                .ToList() ?? new List<int>();

            var existingAttendanceRecords = await _context.AttendanceRecords
                .Where(a => courseEnrolmentIds.Contains(a.CourseEnrolmentId) && a.Date == selectedDate)
                .ToListAsync();

            foreach (var student in model.Students ?? new List<AttendanceStudentItemViewModel>())
            {
                var existingRecord = existingAttendanceRecords
                    .FirstOrDefault(a => a.CourseEnrolmentId == student.CourseEnrolmentId);

                if (existingRecord != null && existingRecord.Present != student.Present)
                {
                    if (string.IsNullOrWhiteSpace(student.ChangeJustification))
                    {
                        ModelState.AddModelError("", $"Justification is required to change attendance for {student.FullName}.");
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                await LoadFilters(model);
                return View("Index", model);
            }

            foreach (var student in model.Students)
            {
                var existingRecord = existingAttendanceRecords
                    .FirstOrDefault(a => a.CourseEnrolmentId == student.CourseEnrolmentId);

                if (existingRecord == null)
                {
                    var newRecord = new AttendanceRecord
                    {
                        CourseEnrolmentId = student.CourseEnrolmentId,
                        Date = selectedDate,
                        Present = student.Present,
                        ChangeJustification = null
                    };

                    _context.AttendanceRecords.Add(newRecord);
                }
                else
                {
                    if (existingRecord.Present != student.Present)
                    {
                        existingRecord.Present = student.Present;
                        existingRecord.ChangeJustification = student.ChangeJustification;
                        _context.AttendanceRecords.Update(existingRecord);
                    }
                }
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Attendance saved successfully.";

            return RedirectToAction(nameof(Index), new
            {
                branchId = model.BranchId,
                branchCourseId = model.BranchCourseId,
                date = model.Date.ToString("yyyy-MM-dd")
            });
        }

        // GET: AttendanceRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceRecord = await _context.AttendanceRecords
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.StudentProfile)
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (attendanceRecord == null)
            {
                return NotFound();
            }

            return View(attendanceRecord);
        }

        private async Task LoadFilters(AttendanceIndexViewModel model)
        {
            model.Branches = await _context.Branches
                .OrderBy(b => b.Name)
                .ThenBy(b => b.Address)
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name + " - " + b.Address
                })
                .ToListAsync();

            model.BranchCourses = new List<SelectListItem>();

            if (model.BranchId.HasValue)
            {
                model.BranchCourses = await _context.BranchCourses
                    .Where(bc => bc.BranchId == model.BranchId.Value)
                    .Include(bc => bc.Course)
                    .OrderBy(bc => bc.Course.Name)
                    .ThenBy(bc => bc.StartDate)
                    .Select(bc => new SelectListItem
                    {
                        Value = bc.Id.ToString(),
                        Text = bc.Course.Name + " | " +
                               bc.ClassCode + " | " +
                               bc.StartDate.ToString("dd/MM/yyyy") + " - " +
                               bc.EndDate.ToString("dd/MM/yyyy")
                    })
                    .ToListAsync();
            }
        }
    }
}