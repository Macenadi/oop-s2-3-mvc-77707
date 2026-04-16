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
    public class ExamResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExamResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExamResults
        public async Task<IActionResult> Index(int? examId = null)
        {
            var query = _context.ExamResults
                .Include(e => e.Exam)
                    .ThenInclude(ex => ex.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(e => e.Exam)
                    .ThenInclude(ex => ex.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(e => e.StudentProfile)
                .AsQueryable();

            if (examId.HasValue)
            {
                query = query.Where(er => er.ExamId == examId.Value);

                var selectedExam = await _context.Exams
                    .Include(e => e.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                    .FirstOrDefaultAsync(e => e.Id == examId.Value);

                ViewBag.SelectedExamTitle = selectedExam?.Title;
                ViewBag.SelectedClassCode = selectedExam?.BranchCourse?.ClassCode;
            }

            return View(await query.ToListAsync());
        }

        // GET: ExamResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examResult = await _context.ExamResults
                .Include(e => e.Exam)
                    .ThenInclude(ex => ex.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(e => e.Exam)
                    .ThenInclude(ex => ex.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(e => e.StudentProfile)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (examResult == null)
            {
                return NotFound();
            }

            return View(examResult);
        }

        // GET: ExamResults/Create

        private string CalculateGradeValue(decimal score, int maxScore)
        {
            if (maxScore <= 0)
            {
                return "0.00";
            }

            var percentage = (score / maxScore) * 100;
            return percentage.ToString("0.00");
        }
        public IActionResult Create(int? examId = null)
        {
            var model = new ExamResult();

            if (examId.HasValue)
            {
                model.ExamId = examId.Value;
            }

            LoadDropDowns(model.ExamId, model.StudentProfileId);
            return View(model);
        }

        // POST: ExamResults/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Score,ExamId,StudentProfileId")] ExamResult examResult)
        {
            await ValidateExamResultAsync(examResult);

            if (ModelState.IsValid)
            {
                try
                {
                    var exam = await _context.Exams.FirstOrDefaultAsync(e => e.Id == examResult.ExamId);
                    if (exam != null)
                    {
                        examResult.Grade = CalculateGradeValue(examResult.Score, exam.MaxScore);
                    }

                    _context.Add(examResult);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { examId = examResult.ExamId });
                }

                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "This student already has a result for this exam.");
                }
            }

            LoadDropDowns(examResult.ExamId, examResult.StudentProfileId);
            return View(examResult);
        }

        // GET: ExamResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examResult = await _context.ExamResults.FindAsync(id);
            if (examResult == null)
            {
                return NotFound();
            }

            LoadDropDowns(examResult.ExamId, examResult.StudentProfileId);
            return View(examResult);
        }

        // POST: ExamResults/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Score,ExamId,StudentProfileId")] ExamResult examResult)
        {
            if (id != examResult.Id)
            {
                return NotFound();
            }

            await ValidateExamResultAsync(examResult);

            if (ModelState.IsValid)
            {
                try
                {
                    var exam = await _context.Exams.FirstOrDefaultAsync(e => e.Id == examResult.ExamId);
                    if (exam != null)
                    {
                        examResult.Grade = CalculateGradeValue(examResult.Score, exam.MaxScore);
                    }

                    _context.Update(examResult);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { examId = examResult.ExamId });
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamResultExists(examResult.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "This student already has a result for this exam.");
                }
            }

            LoadDropDowns(examResult.ExamId, examResult.StudentProfileId);
            return View(examResult);
        }

        // GET: ExamResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examResult = await _context.ExamResults
                .Include(e => e.Exam)
                    .ThenInclude(ex => ex.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(e => e.Exam)
                    .ThenInclude(ex => ex.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(e => e.StudentProfile)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (examResult == null)
            {
                return NotFound();
            }

            return View(examResult);
        }

        // POST: ExamResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examResult = await _context.ExamResults.FindAsync(id);

            if (examResult != null)
            {
                _context.ExamResults.Remove(examResult);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ExamResultExists(int id)
        {
            return _context.ExamResults.Any(e => e.Id == id);
        }

        private async Task ValidateExamResultAsync(ExamResult examResult)
        {
            var exam = await _context.Exams
                .Include(e => e.BranchCourse)
                .FirstOrDefaultAsync(e => e.Id == examResult.ExamId);

            if (exam == null)
            {
                ModelState.AddModelError("ExamId", "Invalid exam selected.");
                return;
            }

            if (examResult.Score < 0)
            {
                ModelState.AddModelError("Score", "Score cannot be negative.");
            }

            if (examResult.Score > exam.MaxScore)
            {
                ModelState.AddModelError("Score", $"Score cannot be greater than the exam max score ({exam.MaxScore}).");
            }

            var studentIsInClass = await _context.CourseEnrolments
                .AnyAsync(ce => ce.BranchCourseId == exam.BranchCourseId &&
                                ce.StudentProfileId == examResult.StudentProfileId);

            if (!studentIsInClass)
            {
                ModelState.AddModelError("StudentProfileId", "This student is not enrolled in the selected class.");
            }

            var duplicate = await _context.ExamResults.AnyAsync(er =>
                er.ExamId == examResult.ExamId &&
                er.StudentProfileId == examResult.StudentProfileId &&
                er.Id != examResult.Id);

            if (duplicate)
            {
                ModelState.AddModelError("", "This student already has a result for this exam.");
            }
        }

        private void LoadDropDowns(int? selectedExamId = null, int? selectedStudentProfileId = null)
        {
            var exams = _context.Exams
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .Select(e => new
                {
                    e.Id,
                    Display = e.Title + " - " +
                              e.BranchCourse.ClassCode + " - " +
                              e.BranchCourse.Course.Name + " (" +
                              e.BranchCourse.Branch.Name + ")"
                })
                .ToList();

            var studentsQuery = _context.StudentProfiles.AsQueryable();

            if (selectedExamId.HasValue)
            {
                var exam = _context.Exams
                    .Include(e => e.BranchCourse)
                    .FirstOrDefault(e => e.Id == selectedExamId.Value);

                if (exam != null)
                {
                    studentsQuery = _context.CourseEnrolments
                        .Where(ce => ce.BranchCourseId == exam.BranchCourseId)
                        .Include(ce => ce.StudentProfile)
                        .Select(ce => ce.StudentProfile!)
                        .Distinct();
                }
            }

            var students = studentsQuery
                .Select(sp => new
                {
                    sp.Id,
                    sp.FullName
                })
                .ToList();

            ViewData["ExamId"] = new SelectList(exams, "Id", "Display", selectedExamId);
            ViewData["StudentProfileId"] = new SelectList(students, "Id", "FullName", selectedStudentProfileId);
        }
    }
}