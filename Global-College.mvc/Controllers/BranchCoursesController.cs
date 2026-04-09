using Global_College.domain.Models.Administrator;
using Global_College.mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Global_College.mvc.Models.ViewModel;


namespace Global_College.mvc.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class BranchCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BranchCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BranchCourses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BranchCourses
                .Include(bc => bc.Branch)
                .Include(bc => bc.Course)
                .OrderBy(bc => bc.Branch.Name)
                .ThenBy(bc => bc.Course.Name)
                .ThenBy(bc => bc.StartDate);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BranchCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchCourse = await _context.BranchCourses
                .Include(bc => bc.Branch)
                .Include(bc => bc.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (branchCourse == null)
            {
                return NotFound();
            }

            var relatedBranchCourses = await _context.BranchCourses
                .Include(bc => bc.Course)
                .Where(bc => bc.BranchId == branchCourse.BranchId)
                .OrderBy(bc => bc.Course.Name)
                .ThenBy(bc => bc.StartDate)
                .ToListAsync();

            var viewModel = new BranchCourseDetailsViewModel
            {
                BranchCourse = branchCourse,
                RelatedBranchCourses = relatedBranchCourses
            };

            return View(viewModel);
        }

        // GET: BranchCourses/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(
                _context.Branches
                    .Select(b => new
                    {
                        b.Id,
                        Display = b.Name + " - " + b.Address
                    }),
                "Id",
                "Display");

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");

            return View();
        }

        // POST: BranchCourses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BranchId,CourseId,StartDate,EndDate")] BranchCourse branchCourse)
        {
            if (branchCourse.EndDate < branchCourse.StartDate)
            {
                ModelState.AddModelError("EndDate", "End date cannot be earlier than start date.");
            }

            // Generate ClassCode automatically before final validation
            branchCourse.ClassCode = await GenerateClassCodeAsync(
                branchCourse.BranchId,
                branchCourse.CourseId,
                branchCourse.StartDate
            );

            // Remove any old validation error for ClassCode
            ModelState.Remove("ClassCode");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(branchCourse);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Branch course created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "This branch course could not be saved. Please try again.");
                }
            }

            ViewData["BranchId"] = new SelectList(
                _context.Branches
                    .Select(b => new
                    {
                        b.Id,
                        Display = b.Name + " - " + b.Address
                    }),
                "Id",
                "Display",
                branchCourse.BranchId);

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", branchCourse.CourseId);

            return View(branchCourse);
        }

        // GET: BranchCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchCourse = await _context.BranchCourses.FindAsync(id);
            if (branchCourse == null)
            {
                return NotFound();
            }

            ViewData["BranchId"] = new SelectList(
                _context.Branches
                    .Select(b => new
                    {
                        b.Id,
                        Display = b.Name + " - " + b.Address
                    }),
                "Id",
                "Display",
                branchCourse.BranchId);

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", branchCourse.CourseId);

            return View(branchCourse);
        }

        // POST: BranchCourses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BranchId,CourseId,ClassCode,StartDate,EndDate")] BranchCourse branchCourse)
        {
            if (id != branchCourse.Id)
            {
                return NotFound();
            }

            if (branchCourse.EndDate < branchCourse.StartDate)
            {
                ModelState.AddModelError("EndDate", "End date cannot be earlier than start date.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(branchCourse);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Branch course updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchCourseExists(branchCourse.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "This branch course could not be updated. Please check the values and try again.");
                }
            }

            ViewData["BranchId"] = new SelectList(
                _context.Branches
                    .Select(b => new
                    {
                        b.Id,
                        Display = b.Name + " - " + b.Address
                    }),
                "Id",
                "Display",
                branchCourse.BranchId);

            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", branchCourse.CourseId);

            return View(branchCourse);
        }

        // GET: BranchCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branchCourse = await _context.BranchCourses
                .Include(bc => bc.Branch)
                .Include(bc => bc.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (branchCourse == null)
            {
                return NotFound();
            }

            return View(branchCourse);
        }

        // POST: BranchCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branchCourse = await _context.BranchCourses.FindAsync(id);

            if (branchCourse == null)
            {
                return NotFound();
            }

            try
            {
                _context.BranchCourses.Remove(branchCourse);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Branch course deleted successfully.";
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "This branch course could not be deleted because it is linked to other records.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BranchCourseExists(int id)
        {
            return _context.BranchCourses.Any(e => e.Id == id);
        }

        private async Task<string> GenerateClassCodeAsync(int branchId, int courseId, DateOnly startDate)
        {
            var branch = await _context.Branches.FindAsync(branchId);
            var course = await _context.Courses.FindAsync(courseId);

            var branchPart = "BR";

            if (branch != null && !string.IsNullOrWhiteSpace(branch.Name))
            {
                branchPart = new string(branch.Name
                    .Where(char.IsLetterOrDigit)
                    .Take(3)
                    .ToArray())
                    .ToUpper();

                if (branchPart.Length < 3)
                {
                    branchPart = branchPart.PadRight(3, 'X');
                }
            }

            var courseWords = (course?.Name ?? "COURSE")
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var coursePart = string.Concat(courseWords.Select(w => char.ToUpper(w[0])));

            if (string.IsNullOrWhiteSpace(coursePart))
            {
                coursePart = "CRS";
            }

            var yearPart = startDate.Year.ToString();
            var baseCode = $"{branchPart}-{coursePart}-{yearPart}";

            var existingCount = await _context.BranchCourses
                .CountAsync(bc => bc.ClassCode.StartsWith(baseCode));

            return $"{baseCode}-{(existingCount + 1):D3}";
        }
    }
}