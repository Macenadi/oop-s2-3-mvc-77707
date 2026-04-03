using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Global_College.domain.Models.Student;
using Global_College.mvc.Data;

namespace Global_College.mvc.Controllers
{
    public class ExamResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExamResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExamResults
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ExamResults
                .Include(e => e.Exam)
                    .ThenInclude(ex => ex.Course)
                .Include(e => e.StudentProfile);

            return View(await applicationDbContext.ToListAsync());
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
                    .ThenInclude(ex => ex.Course)
                .Include(e => e.StudentProfile)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (examResult == null)
            {
                return NotFound();
            }

            return View(examResult);
        }

        // GET: ExamResults/Create
        public IActionResult Create()
        {
            LoadDropDowns();
            return View();
        }

        // POST: ExamResults/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Score,Grade,ExamId,StudentProfileId")] ExamResult examResult)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(examResult);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Score,Grade,ExamId,StudentProfileId")] ExamResult examResult)
        {
            if (id != examResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examResult);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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
                    .ThenInclude(ex => ex.Course)
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

        private void LoadDropDowns(int? selectedExamId = null, int? selectedStudentProfileId = null)
        {
            var exams = _context.Exams
                .Include(e => e.Course)
                .Select(e => new
                {
                    e.Id,
                    Display = e.Title + " - " + e.Course.Name
                })
                .ToList();

            var students = _context.StudentProfiles
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
