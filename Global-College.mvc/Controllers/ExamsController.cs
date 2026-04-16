using Global_College.domain.Models.Student;
using Global_College.mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Global_College.mvc.Controllers
{
    [Authorize(Roles = "Administrator,Faculty")]
    public class ExamsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ExamsController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Exams
        public async Task<IActionResult> Index()
        {
            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var applicationDbContext = _context.Exams
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .Where(e => allowedBranchCourseIds.Contains(e.BranchCourseId));

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Exams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var exam = await _context.Exams
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .FirstOrDefaultAsync(m => m.Id == id && allowedBranchCourseIds.Contains(m.BranchCourseId));

            if (exam == null)
            {
                return NotFound();
            }

            var totalResults = await _context.ExamResults
                .CountAsync(er => er.ExamId == exam.Id);

            var totalStudentsInClass = await _context.CourseEnrolments
                .CountAsync(ce => ce.BranchCourseId == exam.BranchCourseId);

            ViewBag.TotalResults = totalResults;
            ViewBag.TotalStudentsInClass = totalStudentsInClass;
            ViewBag.PendingResults = totalStudentsInClass - totalResults;

            return View(exam);
        }

        // GET: Exams/Create
        public async Task<IActionResult> Create()
        {
            await LoadDropDowns();
            return View(new Exam());
        }

        // POST: Exams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Date,MaxScore,ResultsReleased,BranchCourseId")] Exam exam)
        {
            await ValidateExamAsync(exam);

            if (ModelState.IsValid)
            {
                _context.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await LoadDropDowns(exam.BranchCourseId);
            return View(exam);
        }

        // GET: Exams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var exam = await _context.Exams
                .FirstOrDefaultAsync(e => e.Id == id && allowedBranchCourseIds.Contains(e.BranchCourseId));

            if (exam == null)
            {
                return NotFound();
            }

            await LoadDropDowns(exam.BranchCourseId);
            return View(exam);
        }

        // POST: Exams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date,MaxScore,ResultsReleased,BranchCourseId")] Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var existingExam = await _context.Exams
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id && allowedBranchCourseIds.Contains(e.BranchCourseId));

            if (existingExam == null)
            {
                return NotFound();
            }

            await ValidateExamAsync(exam);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
            }

            await LoadDropDowns(exam.BranchCourseId);
            return View(exam);
        }

        // GET: Exams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var exam = await _context.Exams
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .FirstOrDefaultAsync(m => m.Id == id && allowedBranchCourseIds.Contains(m.BranchCourseId));

            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var exam = await _context.Exams
                .FirstOrDefaultAsync(e => e.Id == id && allowedBranchCourseIds.Contains(e.BranchCourseId));

            if (exam != null)
            {
                try
                {
                    _context.Exams.Remove(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    TempData["ErrorMessage"] = "This exam cannot be deleted because it is linked to other records.";
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseByClassCode(int branchCourseId)
        {
            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var branchCourse = await _context.BranchCourses
                .Include(bc => bc.Course)
                .FirstOrDefaultAsync(bc => bc.Id == branchCourseId && allowedBranchCourseIds.Contains(bc.Id));

            if (branchCourse == null)
            {
                return NotFound();
            }

            return Json(new
            {
                courseId = branchCourse.CourseId,
                courseName = branchCourse.Course != null ? branchCourse.Course.Name : ""
            });
        }

        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }

        private async Task ValidateExamAsync(Exam exam)
        {
            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var branchCourse = await _context.BranchCourses
                .FirstOrDefaultAsync(bc => bc.Id == exam.BranchCourseId);

            if (branchCourse == null)
            {
                ModelState.AddModelError("BranchCourseId", "Invalid class selected.");
                return;
            }

            if (!allowedBranchCourseIds.Contains(branchCourse.Id))
            {
                ModelState.AddModelError("BranchCourseId", "You do not have access to this class.");
                return;
            }

            if (exam.MaxScore <= 0)
            {
                ModelState.AddModelError("MaxScore", "Max Score must be greater than zero.");
            }

            if (exam.Date < DateOnly.FromDateTime(DateTime.Today))
            {
                ModelState.AddModelError("Date", "Exam date cannot be earlier than today.");
            }

            if (exam.Date < branchCourse.StartDate || exam.Date > branchCourse.EndDate)
            {
                ModelState.AddModelError("Date", "Exam date must be within the class start and end dates.");
            }

            var duplicateExam = await _context.Exams.AnyAsync(e =>
                e.BranchCourseId == exam.BranchCourseId &&
                e.Title == exam.Title &&
                e.Date == exam.Date &&
                e.Id != exam.Id);

            if (duplicateExam)
            {
                ModelState.AddModelError("", "An exam with the same title and date already exists for this class.");
            }
        }

        private async Task LoadDropDowns(int? selectedBranchCourseId = null)
        {
            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var branchCourses = await _context.BranchCourses
                .Include(bc => bc.Course)
                .Include(bc => bc.Branch)
                .Where(bc => allowedBranchCourseIds.Contains(bc.Id))
                .Select(bc => new
                {
                    bc.Id,
                    Display = bc.ClassCode + " - " + bc.Course.Name + " (" + bc.Branch.Name + ")"
                })
                .ToListAsync();

            ViewData["BranchCourseId"] = new SelectList(branchCourses, "Id", "Display", selectedBranchCourseId);
        }

        private async Task<List<int>> GetAllowedBranchCourseIdsAsync()
        {
            if (User.IsInRole("Administrator"))
            {
                return await _context.BranchCourses
                    .Select(bc => bc.Id)
                    .ToListAsync();
            }

            var userId = _userManager.GetUserId(User);

            return await _context.FacultyProfiles
                .Where(f => f.IdentityUserId == userId)
                .SelectMany(f => f.FacultyCourseAssignments.Select(a => a.BranchCourseId))
                .Distinct()
                .ToListAsync();
        }
    }
}