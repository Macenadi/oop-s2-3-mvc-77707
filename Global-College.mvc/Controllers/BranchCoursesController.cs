using Global_College.domain.Models.Administrator;
using Global_College.mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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
                .Include(bc => bc.Course);

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

            return View(branchCourse);
        }

        // GET: BranchCourses/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name");
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            return View();
        }

        // POST: BranchCourses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BranchId,CourseId")] BranchCourse branchCourse)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(branchCourse);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "This course is already assigned to this branch.");
                }
            }

            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name", branchCourse.BranchId);
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

            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name", branchCourse.BranchId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", branchCourse.CourseId);
            return View(branchCourse);
        }

        // POST: BranchCourses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BranchId,CourseId")] BranchCourse branchCourse)
        {
            if (id != branchCourse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(branchCourse);
                    await _context.SaveChangesAsync();
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
                    ModelState.AddModelError("", "This course is already assigned to this branch.");
                }
            }

            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "Name", branchCourse.BranchId);
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

            if (branchCourse != null)
            {
                _context.BranchCourses.Remove(branchCourse);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BranchCourseExists(int id)
        {
            return _context.BranchCourses.Any(e => e.Id == id);
        }
    }
}
