using Global_College.domain.Models.Administrator;
using Global_College.domain.Models.Faculty;
using Global_College.mvc.Data;
using Global_College.mvc.Models.ViewModel;
using Global_College.mvc.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Global_College.mvc.Controllers
{
    [Authorize(Roles = "Administrator,Faculty")]
    public class FacultyPortalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FacultyPortalController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(MyClasses));
        }

        public async Task<IActionResult> MyClasses()
        {
            var facultyProfile = await GetCurrentFacultyAsync();

            if (facultyProfile == null)
                return NotFound("Faculty profile not found.");

            var header = await BuildHeaderAsync(facultyProfile.Id);

            var items = await _context.FacultyCourseAssignments
                .Include(fca => fca.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(fca => fca.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .Where(fca => fca.FacultyProfileId == facultyProfile.Id)
                .OrderBy(fca => fca.BranchCourse.Course.Name)
                .Select(fca => new FacultyPortalClassItemViewModel
                {
                    BranchCourseId = fca.BranchCourseId,
                    CourseName = fca.BranchCourse.Course.Name,
                    BranchName = fca.BranchCourse.Branch.Name,
                    ClassCode = fca.BranchCourse.ClassCode,
                    StartDate = fca.BranchCourse.StartDate,
                    EndDate = fca.BranchCourse.EndDate
                })
                .ToListAsync();

            var model = new FacultyMyClassesViewModel
            {
                Header = header,
                Items = items
            };

            return View(model);
        }

        public async Task<IActionResult> MyAssignments()
        {
            var facultyProfile = await GetCurrentFacultyAsync();

            if (facultyProfile == null)
                return NotFound("Faculty profile not found.");

            var header = await BuildHeaderAsync(facultyProfile.Id);

            var items = await _context.Assignments
                .Include(a => a.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(a => a.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .Where(a => _context.FacultyCourseAssignments.Any(fca =>
                    fca.FacultyProfileId == facultyProfile.Id &&
                    fca.BranchCourseId == a.BranchCourseId))
                .OrderByDescending(a => a.DueDate)
                .Select(a => new FacultyPortalAssignmentItemViewModel
                {
                    AssignmentId = a.Id,
                    BranchCourseId = a.BranchCourseId,
                    Title = a.Title,
                    CourseName = a.BranchCourse.Course.Name,
                    BranchName = a.BranchCourse.Branch.Name,
                    ClassCode = a.BranchCourse.ClassCode,
                    DueDate = a.DueDate,
                    MaxScore = a.MaxScore
                })
                .ToListAsync();

            var model = new FacultyMyAssignmentsViewModel
            {
                Header = header,
                Items = items
            };

            return View(model);
        }

        public async Task<IActionResult> MyAttendance()
        {
            var facultyProfile = await GetCurrentFacultyAsync();

            if (facultyProfile == null)
                return NotFound("Faculty profile not found.");

            var header = await BuildHeaderAsync(facultyProfile.Id);

            var items = await _context.AttendanceRecords
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.StudentProfile)
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Where(a => _context.FacultyCourseAssignments.Any(fca =>
                    fca.FacultyProfileId == facultyProfile.Id &&
                    fca.BranchCourseId == a.CourseEnrolment.BranchCourseId))
                .OrderByDescending(a => a.Date)
                .Select(a => new FacultyPortalAttendanceItemViewModel
                {
                    AttendanceRecordId = a.Id,
                    BranchCourseId = a.CourseEnrolment.BranchCourseId,
                    StudentName = a.CourseEnrolment.StudentProfile.FullName,
                    StudentNumber = a.CourseEnrolment.StudentProfile.StudentNumber,
                    CourseName = a.CourseEnrolment.BranchCourse.Course.Name,
                    BranchName = a.CourseEnrolment.BranchCourse.Branch.Name,
                    ClassCode = a.CourseEnrolment.BranchCourse.ClassCode,
                    Date = a.Date,
                    Present = a.Present
                })
                .ToListAsync();

            var model = new FacultyMyAttendanceViewModel
            {
                Header = header,
                Items = items
            };

            return View(model);
        }

        public async Task<IActionResult> MyExamResults()
        {
            var facultyProfile = await GetCurrentFacultyAsync();

            if (facultyProfile == null)
                return NotFound("Faculty profile not found.");

            var header = await BuildHeaderAsync(facultyProfile.Id);

            var items = await _context.ExamResults
                .Include(er => er.StudentProfile)
                .Include(er => er.Exam)
                    .ThenInclude(e => e.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(er => er.Exam)
                    .ThenInclude(e => e.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Where(er => _context.FacultyCourseAssignments.Any(fca =>
                    fca.FacultyProfileId == facultyProfile.Id &&
                    fca.BranchCourseId == er.Exam.BranchCourseId))
                .OrderByDescending(er => er.Exam.Date)
                .Select(er => new FacultyPortalExamResultItemViewModel
                {
                    ExamResultId = er.Id,
                    ExamId = er.ExamId,
                    BranchCourseId = er.Exam.BranchCourseId,
                    ExamTitle = er.Exam.Title,
                    StudentName = er.StudentProfile.FullName,
                    StudentNumber = er.StudentProfile.StudentNumber,
                    CourseName = er.Exam.BranchCourse.Course.Name,
                    BranchName = er.Exam.BranchCourse.Branch.Name,
                    ClassCode = er.Exam.BranchCourse.ClassCode,
                    Date = er.Exam.Date,
                    Score = er.Score,
                    Grade = er.Grade,
                    ResultsReleased = er.Exam.ResultsReleased
                })
                .ToListAsync();

            var model = new FacultyMyExamResultsViewModel
            {
                Header = header,
                Items = items
            };

            return View(model);
        }

        private async Task<FacultyProfile?> GetCurrentFacultyAsync()
        {
            var userId = _userManager.GetUserId(User);

            return await _context.FacultyProfiles
                .FirstOrDefaultAsync(f => f.IdentityUserId == userId);
        }

        private async Task<FacultyPortalHeaderViewModel> BuildHeaderAsync(int facultyProfileId)
        {
            var faculty = await _context.FacultyProfiles
                .FirstOrDefaultAsync(f => f.Id == facultyProfileId);

            var assignedClassesCount = await _context.FacultyCourseAssignments
                .CountAsync(fca => fca.FacultyProfileId == facultyProfileId);

            return new FacultyPortalHeaderViewModel
            {
                FacultyName = faculty?.FullName ?? "",
                FacultyNumber = faculty?.FacultyNumber ?? "",
                SystemEmail = faculty?.SystemEmail ?? "",
                AssignedClassesCount = assignedClassesCount
            };
        }
    }
}