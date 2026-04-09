using Global_College.domain.Models.Administrator;
using Global_College.mvc.Data;
using Global_College.mvc.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Global_College.mvc.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CoursesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Courses.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.BranchCourses)
                    .ThenInclude(bc => bc.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            var branchStudentCounts = await _context.CourseEnrolments
                .Where(e => e.BranchCourse != null && e.BranchCourse.CourseId == course.Id)
                .GroupBy(e => e.BranchCourseId)
                .Select(g => new
                {
                    BranchCourseId = g.Key,
                    StudentCount = g.Select(e => e.StudentProfileId).Distinct().Count()
                })
                .ToDictionaryAsync(x => x.BranchCourseId, x => x.StudentCount);

            ViewBag.BranchStudentCounts = branchStudentCounts;

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.CreatedAt = DateTime.UtcNow;

                _context.Add(course);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Course created successfully.";
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.BranchCourses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            var viewModel = new CourseEditViewModel
            {
                Id = course.Id,
                Name = course.Name,
                RequiresAdminPasswordConfirmation = course.BranchCourses.Any()
            };

            return View(viewModel);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.BranchCourses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            var requiresPasswordConfirmation = course.BranchCourses.Any();
            model.RequiresAdminPasswordConfirmation = requiresPasswordConfirmation;

            if (requiresPasswordConfirmation)
            {
                if (string.IsNullOrWhiteSpace(model.AdminPassword))
                {
                    ModelState.AddModelError("AdminPassword", "Admin password is required because this course is already in progress.");
                }
                else
                {
                    var currentUser = await _userManager.GetUserAsync(User);

                    if (currentUser == null || !await _userManager.CheckPasswordAsync(currentUser, model.AdminPassword))
                    {
                        ModelState.AddModelError("AdminPassword", "Invalid admin password.");
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            course.Name = model.Name;

            try
            {
                _context.Update(course);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Course updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(model.Id))
                {
                    return NotFound();
                }

                throw;
            }
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.BranchCourses)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            if (course.BranchCourses.Any())
            {
                TempData["ErrorMessage"] = "This course cannot be deleted because it is assigned to a branch.";
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses
                .Include(c => c.BranchCourses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            if (course.BranchCourses.Any())
            {
                TempData["ErrorMessage"] = "This course cannot be deleted because it is assigned to a branch.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Course deleted successfully.";
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "This course could not be deleted because it is linked to other records.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}