using Global_College.domain.Models.Faculty;
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
    public class AssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Assignments
                .Include(a => a.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(a => a.BranchCourse)
                    .ThenInclude(bc => bc.Branch);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Assignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(a => a.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (assignment == null)
            {
                return NotFound();
            }

            var totalResults = await _context.AssignmentResults
                .CountAsync(ar => ar.AssignmentId == assignment.Id);

            var totalStudentsInClass = await _context.CourseEnrolments
                .CountAsync(ce => ce.BranchCourseId == assignment.BranchCourseId);

            ViewBag.TotalResults = totalResults;
            ViewBag.TotalStudentsInClass = totalStudentsInClass;
            ViewBag.PendingResults = totalStudentsInClass - totalResults;

            return View(assignment);
        }

        // GET: Assignments/Create
        public IActionResult Create()
        {
            LoadDropDowns();
            return View(new Assignment());
        }

        // POST: Assignments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,MaxScore,DueDate,BranchCourseId")] Assignment assignment)
        {
            try
            {
                assignment.Validate();
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(nameof(assignment.MaxScore), ex.Message);
            }

            var branchCourse = await _context.BranchCourses
                .FirstOrDefaultAsync(bc => bc.Id == assignment.BranchCourseId);

            if (branchCourse == null)
            {
                ModelState.AddModelError(nameof(assignment.BranchCourseId), "Invalid class selected.");
            }
            else
            {
                if (assignment.DueDate < DateOnly.FromDateTime(DateTime.Today))
                {
                    ModelState.AddModelError(nameof(assignment.DueDate), "Due date cannot be earlier than today.");
                }

                if (assignment.DueDate < branchCourse.StartDate || assignment.DueDate > branchCourse.EndDate)
                {
                    ModelState.AddModelError(nameof(assignment.DueDate), "Due date must be within the class start and end dates.");
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadDropDowns(assignment.BranchCourseId);
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            LoadDropDowns(assignment.BranchCourseId);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,MaxScore,DueDate,BranchCourseId")] Assignment assignment)
        {
            if (id != assignment.Id)
            {
                return NotFound();
            }

            try
            {
                assignment.Validate();
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(nameof(assignment.MaxScore), ex.Message);
            }

            var branchCourse = await _context.BranchCourses
                .FirstOrDefaultAsync(bc => bc.Id == assignment.BranchCourseId);

            if (branchCourse == null)
            {
                ModelState.AddModelError(nameof(assignment.BranchCourseId), "Invalid class selected.");
            }
            else
            {
                if (assignment.DueDate < DateOnly.FromDateTime(DateTime.Today))
                {
                    ModelState.AddModelError(nameof(assignment.DueDate), "Due date cannot be earlier than today.");
                }

                if (assignment.DueDate < branchCourse.StartDate || assignment.DueDate > branchCourse.EndDate)
                {
                    ModelState.AddModelError(nameof(assignment.DueDate), "Due date must be within the class start and end dates.");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(assignment.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
            }

            LoadDropDowns(assignment.BranchCourseId);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(a => a.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);

            if (assignment != null)
            {
                try
                {
                    _context.Assignments.Remove(assignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    TempData["ErrorMessage"] = "This assignment cannot be deleted because it is linked to other records.";
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(e => e.Id == id);
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