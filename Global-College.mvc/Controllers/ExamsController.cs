using Global_College.domain.Models.Student;
using Global_College.mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Global_College.mvc.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ExamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exams
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Exams
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Branch);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Exams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);

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
        public IActionResult Create()
        {
            LoadDropDowns();
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


            LoadDropDowns(exam.BranchCourseId);
            return View(exam);
        }

        // GET: Exams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            LoadDropDowns(exam.BranchCourseId);
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

            LoadDropDowns(exam.BranchCourseId);
            return View(exam);
        }

        // GET: Exams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);

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
            var exam = await _context.Exams.FindAsync(id);

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
            var branchCourse = await _context.BranchCourses
                .Include(bc => bc.Course)
                .FirstOrDefaultAsync(bc => bc.Id == branchCourseId);

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
            var branchCourse = await _context.BranchCourses
                .FirstOrDefaultAsync(bc => bc.Id == exam.BranchCourseId);

            if (branchCourse == null)
            {
                ModelState.AddModelError("BranchCourseId", "Invalid class selected.");
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

        private void LoadDropDowns(int? selectedBranchCourseId = null)
        {
            var branchCourses = _context.BranchCourses
                .Include(bc => bc.Course)
                .Include(bc => bc.Branch)
                .Select(bc => new
                {
                    bc.Id,
                    Display = bc.ClassCode + " - " + bc.Course.Name + " (" + bc.Branch.Name + ")"
                })
                .ToList();

            ViewData["BranchCourseId"] = new SelectList(branchCourses, "Id", "Display", selectedBranchCourseId);
        }
    }
}