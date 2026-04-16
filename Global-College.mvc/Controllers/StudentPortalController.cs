using Global_College.domain.Models.Administrator;
using Global_College.domain.Models.Student;
using Global_College.mvc.Data;
using Global_College.mvc.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Global_College.mvc.Controllers
{
    [Authorize(Roles = "Administrator,Student")]
    public class StudentPortalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentPortalController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(MyProfile));
        }

        public async Task<IActionResult> MyProfile()
        {
            var studentProfile = await GetCurrentStudentAsync();

            if (studentProfile == null)
                return NotFound("Student profile not found.");

            var header = await BuildHeaderAsync(studentProfile.Id);

            var model = new MyProfileViewModel
            {
                Header = header,
                FullName = studentProfile.FullName,
                Email = studentProfile.Email,
                Phone = studentProfile.Phone,
                Address = studentProfile.Address,
                SystemEmail = studentProfile.SystemEmail
            };

            return View(model);
        }

        public async Task<IActionResult> MyEnrolments()
        {
            var studentProfile = await GetCurrentStudentAsync();

            if (studentProfile == null)
                return NotFound("Student profile not found.");

            var header = await BuildHeaderAsync(studentProfile.Id);

            var items = await _context.CourseEnrolments
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .Where(e => e.StudentProfileId == studentProfile.Id)
                .OrderByDescending(e => e.EnrolDate)
                .Select(e => new MyEnrolmentItemViewModel
                {
                    CourseName = e.BranchCourse.Course.Name,
                    BranchName = e.BranchCourse.Branch.Name,
                    ClassCode = e.BranchCourse.ClassCode,
                    EnrolDate = e.EnrolDate,
                    Status = e.Status
                })
                .ToListAsync();

            var model = new MyEnrolmentsViewModel
            {
                Header = header,
                Items = items
            };

            return View(model);
        }

        public async Task<IActionResult> MyAttendance()
        {
            var studentProfile = await GetCurrentStudentAsync();

            if (studentProfile == null)
                return NotFound("Student profile not found.");

            var header = await BuildHeaderAsync(studentProfile.Id);

            var items = await _context.AttendanceRecords
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Where(a => a.CourseEnrolment.StudentProfileId == studentProfile.Id)
                .OrderByDescending(a => a.Date)
                .Select(a => new MyAttendanceItemViewModel
                {
                    CourseName = a.CourseEnrolment.BranchCourse.Course.Name,
                    BranchName = a.CourseEnrolment.BranchCourse.Branch.Name,
                    ClassCode = a.CourseEnrolment.BranchCourse.ClassCode,
                    Date = a.Date,
                    Present = a.Present
                })
                .ToListAsync();

            var model = new MyAttendanceViewModel
            {
                Header = header,
                Items = items
            };

            return View(model);
        }

        public async Task<IActionResult> MyAssignments()
        {
            var studentProfile = await GetCurrentStudentAsync();

            if (studentProfile == null)
                return NotFound("Student profile not found.");

            var header = await BuildHeaderAsync(studentProfile.Id);

            var assignments = await _context.Assignments
                .Include(a => a.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(a => a.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .Where(a => _context.CourseEnrolments.Any(ce =>
                    ce.StudentProfileId == studentProfile.Id &&
                    ce.BranchCourseId == a.BranchCourseId &&
                    ce.Status == "Enrolled"))
                .OrderByDescending(a => a.DueDate)
                .ToListAsync();

            var assignmentIds = assignments.Select(a => a.Id).ToList();
            var branchCourseIds = assignments.Select(a => a.BranchCourseId).Distinct().ToList();

            var results = await _context.AssignmentResults
                .Where(r => r.StudentProfileId == studentProfile.Id && assignmentIds.Contains(r.AssignmentId))
                .ToListAsync();

            var facultyAssignments = await _context.FacultyCourseAssignments
                .Include(f => f.FacultyProfile)
                .Where(f => branchCourseIds.Contains(f.BranchCourseId))
                .ToListAsync();

            var model = new MyAssignmentsViewModel
            {
                Header = header,
                Items = assignments.Select(a =>
                {
                    var result = results.FirstOrDefault(r => r.AssignmentId == a.Id);

                    var teacherNames = facultyAssignments
                        .Where(f => f.BranchCourseId == a.BranchCourseId)
                        .Select(f => f.FacultyProfile.FullName ?? "")
                        .Where(n => !string.IsNullOrWhiteSpace(n))
                        .Distinct()
                        .ToList();

                    return new MyAssignmentItemViewModel
                    {
                        AssignmentId = a.Id,
                        Title = a.Title,
                        CourseName = a.BranchCourse.Course.Name,
                        BranchName = a.BranchCourse.Branch.Name,
                        ClassCode = a.BranchCourse.ClassCode,
                        DueDate = a.DueDate,
                        MaxScore = a.MaxScore,
                        Score = result?.Score,
                        Feedback = result?.Feedback,
                        Grade = result?.Grade,
                        TeacherNames = teacherNames.Any() ? string.Join(", ", teacherNames) : "Not assigned"
                    };
                }).ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> MyExamResults()
        {
            var studentProfile = await GetCurrentStudentAsync();

            if (studentProfile == null)
                return NotFound("Student profile not found.");

            var header = await BuildHeaderAsync(studentProfile.Id);

            var results = await _context.ExamResults
                .Include(er => er.Exam)
                    .ThenInclude(e => e.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(er => er.Exam)
                    .ThenInclude(e => e.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Where(er => er.StudentProfileId == studentProfile.Id &&
                             er.Exam != null &&
                             er.Exam.ResultsReleased)
                .OrderByDescending(er => er.Exam.Date)
                .ToListAsync();

            var branchCourseIds = results
                .Select(r => r.Exam.BranchCourseId)
                .Distinct()
                .ToList();

            var facultyAssignments = await _context.FacultyCourseAssignments
                .Include(f => f.FacultyProfile)
                .Where(f => branchCourseIds.Contains(f.BranchCourseId))
                .ToListAsync();

            var model = new MyExamResultsViewModel
            {
                Header = header,
                Items = results.Select(r =>
                {
                    var teacherNames = facultyAssignments
                        .Where(f => f.BranchCourseId == r.Exam.BranchCourseId)
                        .Select(f => f.FacultyProfile.FullName ?? "")
                        .Where(n => !string.IsNullOrWhiteSpace(n))
                        .Distinct()
                        .ToList();

                    return new MyExamResultItemViewModel
                    {
                        ExamId = r.ExamId,
                        Title = r.Exam.Title,
                        CourseName = r.Exam.BranchCourse.Course.Name,
                        BranchName = r.Exam.BranchCourse.Branch.Name,
                        ClassCode = r.Exam.BranchCourse.ClassCode,
                        Date = r.Exam.Date,
                        MaxScore = r.Exam.MaxScore,
                        Score = r.Score,
                        Grade = r.Grade,
                        TeacherNames = teacherNames.Any() ? string.Join(", ", teacherNames) : "Not assigned"
                    };
                }).ToList()
            };

            return View(model);
        }

        private async Task<StudentProfile?> GetCurrentStudentAsync()
        {
            var userId = _userManager.GetUserId(User);

            return await _context.StudentProfiles
                .FirstOrDefaultAsync(s => s.IdentityUserId == userId);
        }

        private async Task<StudentPortalHeaderViewModel> BuildHeaderAsync(int studentProfileId)
        {
            var student = await _context.StudentProfiles
                .FirstOrDefaultAsync(s => s.Id == studentProfileId);

            var currentEnrolment = await _context.CourseEnrolments
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .Where(e => e.StudentProfileId == studentProfileId)
                .OrderByDescending(e => e.Status == "Enrolled" ? 1 : 0)
                .ThenByDescending(e => e.EnrolDate)
                .FirstOrDefaultAsync();

            return new StudentPortalHeaderViewModel
            {
                StudentName = student?.FullName ?? "",
                StudentNumber = student?.StudentNumber ?? "",
                CurrentCourseName = currentEnrolment?.BranchCourse?.Course?.Name ?? "No active course",
                CurrentBranchName = currentEnrolment?.BranchCourse?.Branch?.Name ?? "",
                CurrentClassCode = currentEnrolment?.BranchCourse?.ClassCode ?? ""
            };
        }
    }
}