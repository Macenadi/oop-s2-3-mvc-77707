using Global_College.domain.Models.Administrator;
using Global_College.mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Global_College.mvc.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CourseEnrolmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseEnrolmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseEnrolments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CourseEnrolments
                .Include(c => c.StudentProfile)
                .Include(c => c.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .Include(c => c.BranchCourse)
                    .ThenInclude(bc => bc.Course);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CourseEnrolments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEnrolment = await _context.CourseEnrolments
                .Include(c => c.StudentProfile)
                .Include(c => c.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .Include(c => c.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (courseEnrolment == null)
            {
                return NotFound();
            }

            return View(courseEnrolment);
        }

        // GET: CourseEnrolments/Create
        public IActionResult Create()
        {
            LoadDropDowns();
            return View();
        }

        // POST: CourseEnrolments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EnrolDate,Status,Justification,StoppedDate,BranchCourseId,StudentProfileId")] CourseEnrolment courseEnrolment)
        {
            if (courseEnrolment.Status == "Active")
            {
                courseEnrolment.StoppedDate = null;
            }
            else if (!courseEnrolment.StoppedDate.HasValue)
            {
                courseEnrolment.StoppedDate = DateOnly.FromDateTime(DateTime.Today);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(courseEnrolment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "This student is already enrolled in this branch course.");
                }
            }

            LoadDropDowns(courseEnrolment.BranchCourseId, courseEnrolment.StudentProfileId);
            return View(courseEnrolment);
        }

        // GET: CourseEnrolments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEnrolment = await _context.CourseEnrolments.FindAsync(id);
            if (courseEnrolment == null)
            {
                return NotFound();
            }

            LoadDropDowns(courseEnrolment.BranchCourseId, courseEnrolment.StudentProfileId);
            return View(courseEnrolment);
        }

        // POST: CourseEnrolments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EnrolDate,Status,Justification,StoppedDate,BranchCourseId,StudentProfileId")] CourseEnrolment courseEnrolment)
        {
            if (id != courseEnrolment.Id)
            {
                return NotFound();
            }

            if (courseEnrolment.Status == "Active")
            {
                courseEnrolment.StoppedDate = null;
            }
            else if (!courseEnrolment.StoppedDate.HasValue)
            {
                courseEnrolment.StoppedDate = DateOnly.FromDateTime(DateTime.Today);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseEnrolment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseEnrolmentExists(courseEnrolment.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "This student is already enrolled in this branch course.");
                }
            }

            LoadDropDowns(courseEnrolment.BranchCourseId, courseEnrolment.StudentProfileId);
            return View(courseEnrolment);
        }

        // GET: CourseEnrolments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEnrolment = await _context.CourseEnrolments
                .Include(c => c.StudentProfile)
                .Include(c => c.BranchCourse)
                    .ThenInclude(bc => bc.Branch!)
                .Include(c => c.BranchCourse)
                    .ThenInclude(bc => bc.Course!)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (courseEnrolment == null)
            {
                return NotFound();
            }

            return View(courseEnrolment);
        }

        // POST: CourseEnrolments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseEnrolment = await _context.CourseEnrolments.FindAsync(id);

            if (courseEnrolment != null)
            {
                _context.CourseEnrolments.Remove(courseEnrolment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CourseEnrolmentExists(int id)
        {
            return _context.CourseEnrolments.Any(e => e.Id == id);
        }

        private void LoadDropDowns(int? selectedBranchCourseId = null, int? selectedStudentProfileId = null)
        {
            var branchCourses = _context.BranchCourses
                .Include(bc => bc.Branch)
                .Include(bc => bc.Course)
                .Select(bc => new
                {
                    bc.Id,
                    DisplayName = bc.Branch.Name + " - " + bc.Course.Name
                })
                .ToList();

            var students = _context.StudentProfiles
                .Select(sp => new
                {
                    sp.Id,
                    sp.FullName
                })
                .ToList();

            ViewData["BranchCourseId"] = new SelectList(branchCourses, "Id", "DisplayName", selectedBranchCourseId);
            ViewData["StudentProfileId"] = new SelectList(students, "Id", "FullName", selectedStudentProfileId);
        }
    }
}
