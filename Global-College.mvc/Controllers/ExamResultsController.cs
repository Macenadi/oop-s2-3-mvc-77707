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
    public class ExamResultsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ExamResultsController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ExamResults
        public async Task<IActionResult> Index(int? examId = null)
        {
            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var query = _context.ExamResults
                .Include(e => e.Exam)
                    .ThenInclude(ex => ex.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(e => e.Exam)
                    .ThenInclude(ex => ex.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(e => e.StudentProfile)
                .Where(er => allowedBranchCourseIds.Contains(er.Exam.BranchCourseId))
                .AsQueryable();

            if (examId.HasValue)
            {
                var selectedExam = await _context.Exams
                    .Include(e => e.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                    .FirstOrDefaultAsync(e =>
                        e.Id == examId.Value &&
                        allowedBranchCourseIds.Contains(e.BranchCourseId));

                if (selectedExam == null)
                {
                    return Forbid();
                }

                query = query.Where(er => er.ExamId == examId.Value);

                ViewBag.SelectedExamTitle = selectedExam.Title;
                ViewBag.SelectedClassCode = selectedExam.BranchCourse?.ClassCode;
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

            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var examResult = await _context.ExamResults
                .Include(e => e.Exam)
                    .ThenInclude(ex => ex.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(e => e.Exam)
                    .ThenInclude(ex => ex.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(e => e.StudentProfile)
                .FirstOrDefaultAsync(m =>
                    m.Id == id &&
                    allowedBranchCourseIds.Contains(m.Exam.BranchCourseId));

            if (examResult == null)
            {
                return NotFound();
            }

            return View(examResult);
        }

        private string CalculateGradeValue(decimal score, int maxScore)
        {
            if (maxScore <= 0)
            {
                return "0.00";
            }

            var percentage = (score / maxScore) * 100;
            return percentage.ToString("0.00");
        }

        // GET: ExamResults/Create
        public async Task<IActionResult> Create(int? examId = null)
        {
            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();
            var model = new ExamResult();

            if (examId.HasValue)
            {
                var examAllowed = await _context.Exams
                    .AnyAsync(e =>
                        e.Id == examId.Value &&
                        allowedBranchCourseIds.Contains(e.BranchCourseId));

                if (!examAllowed)
                {
                    return Forbid();
                }

                model.ExamId = examId.Value;
            }

            await LoadDropDowns(model.ExamId, model.StudentProfileId);
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

            await LoadDropDowns(examResult.ExamId, examResult.StudentProfileId);
            return View(examResult);
        }

        // GET: ExamResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var examResult = await _context.ExamResults
                .Include(er => er.Exam)
                .FirstOrDefaultAsync(er =>
                    er.Id == id &&
                    allowedBranchCourseIds.Contains(er.Exam.BranchCourseId));

            if (examResult == null)
            {
                return NotFound();
            }

            await LoadDropDowns(examResult.ExamId, examResult.StudentProfileId);
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

            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var existingResult = await _context.ExamResults
                .Include(er => er.Exam)
                .FirstOrDefaultAsync(er =>
                    er.Id == id &&
                    allowedBranchCourseIds.Contains(er.Exam.BranchCourseId));

            if (existingResult == null)
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

                    existingResult.Score = examResult.Score;
                    existingResult.ExamId = examResult.ExamId;
                    existingResult.StudentProfileId = examResult.StudentProfileId;
                    existingResult.Grade = examResult.Grade;

                    _context.Update(existingResult);
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

            await LoadDropDowns(examResult.ExamId, examResult.StudentProfileId);
            return View(examResult);
        }

        // GET: ExamResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var examResult = await _context.ExamResults
                .Include(e => e.Exam)
                    .ThenInclude(ex => ex.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(e => e.Exam)
                    .ThenInclude(ex => ex.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(e => e.StudentProfile)
                .FirstOrDefaultAsync(m =>
                    m.Id == id &&
                    allowedBranchCourseIds.Contains(m.Exam.BranchCourseId));

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
            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var examResult = await _context.ExamResults
                .Include(er => er.Exam)
                .FirstOrDefaultAsync(er =>
                    er.Id == id &&
                    allowedBranchCourseIds.Contains(er.Exam.BranchCourseId));

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
            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var exam = await _context.Exams
                .Include(e => e.BranchCourse)
                .FirstOrDefaultAsync(e => e.Id == examResult.ExamId);

            if (exam == null)
            {
                ModelState.AddModelError("ExamId", "Invalid exam selected.");
                return;
            }

            if (!allowedBranchCourseIds.Contains(exam.BranchCourseId))
            {
                ModelState.AddModelError("ExamId", "You do not have access to this exam.");
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

        private async Task LoadDropDowns(int? selectedExamId = null, int? selectedStudentProfileId = null)
        {
            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var exams = await _context.Exams
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(e => e.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .Where(e => allowedBranchCourseIds.Contains(e.BranchCourseId))
                .Select(e => new
                {
                    e.Id,
                    Display = e.Title + " - " +
                              e.BranchCourse.ClassCode + " - " +
                              e.BranchCourse.Course.Name + " (" +
                              e.BranchCourse.Branch.Name + ")"
                })
                .ToListAsync();

            var studentsQuery = _context.StudentProfiles.AsQueryable();

            if (selectedExamId.HasValue)
            {
                var exam = await _context.Exams
                    .Include(e => e.BranchCourse)
                    .FirstOrDefaultAsync(e =>
                        e.Id == selectedExamId.Value &&
                        allowedBranchCourseIds.Contains(e.BranchCourseId));

                if (exam != null)
                {
                    studentsQuery = _context.CourseEnrolments
                        .Where(ce => ce.BranchCourseId == exam.BranchCourseId)
                        .Include(ce => ce.StudentProfile)
                        .Select(ce => ce.StudentProfile!)
                        .Distinct();
                }
                else
                {
                    studentsQuery = _context.StudentProfiles.Where(sp => false);
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