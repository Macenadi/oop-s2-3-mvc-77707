using Global_College.domain.Models.Administrator;
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
    public class FacultyCourseAssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacultyCourseAssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FacultyCourseAssignments
        public async Task<IActionResult> Index()
        {
            // Atualizado para incluir BranchCourse e depois Course
            var applicationDbContext = _context.FacultyCourseAssignments
                .Include(f => f.FacultyProfile)
                .Include(f => f.BranchCourse)
                    .ThenInclude(bc => bc.Course);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FacultyCourseAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var facultyCourseAssignment = await _context.FacultyCourseAssignments
                .Include(f => f.FacultyProfile)
                .Include(f => f.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (facultyCourseAssignment == null) return NotFound();

            return View(facultyCourseAssignment);
        }

        // GET: FacultyCourseAssignments/Create
        public IActionResult Create()
        {
            // Carrega BranchCourses para o SelectList
            ViewData["BranchCourseId"] = new SelectList(_context.BranchCourses.Include(bc => bc.Course), "Id", "Course.Name");
            ViewData["FacultyProfileId"] = new SelectList(_context.FacultyProfiles, "Id", "Email");
            return View();
        }

        // POST: FacultyCourseAssignments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BranchCourseId,FacultyProfileId")] FacultyCourseAssignment facultyCourseAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facultyCourseAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchCourseId"] = new SelectList(_context.BranchCourses.Include(bc => bc.Course), "Id", "Course.Name", facultyCourseAssignment.BranchCourseId);
            ViewData["FacultyProfileId"] = new SelectList(_context.FacultyProfiles, "Id", "Email", facultyCourseAssignment.FacultyProfileId);
            return View(facultyCourseAssignment);
        }

        // GET: FacultyCourseAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var facultyCourseAssignment = await _context.FacultyCourseAssignments.FindAsync(id);
            if (facultyCourseAssignment == null) return NotFound();

            ViewData["BranchCourseId"] = new SelectList(_context.BranchCourses.Include(bc => bc.Course), "Id", "Course.Name", facultyCourseAssignment.BranchCourseId);
            ViewData["FacultyProfileId"] = new SelectList(_context.FacultyProfiles, "Id", "Email", facultyCourseAssignment.FacultyProfileId);
            return View(facultyCourseAssignment);
        }

        // POST: FacultyCourseAssignments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BranchCourseId,FacultyProfileId")] FacultyCourseAssignment facultyCourseAssignment)
        {
            if (id != facultyCourseAssignment.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facultyCourseAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyCourseAssignmentExists(facultyCourseAssignment.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchCourseId"] = new SelectList(_context.BranchCourses.Include(bc => bc.Course), "Id", "Course.Name", facultyCourseAssignment.BranchCourseId);
            ViewData["FacultyProfileId"] = new SelectList(_context.FacultyProfiles, "Id", "Email", facultyCourseAssignment.FacultyProfileId);
            return View(facultyCourseAssignment);
        }

        // GET: FacultyCourseAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var facultyCourseAssignment = await _context.FacultyCourseAssignments
                .Include(f => f.FacultyProfile)
                .Include(f => f.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (facultyCourseAssignment == null) return NotFound();

            return View(facultyCourseAssignment);
        }

        // POST: FacultyCourseAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facultyCourseAssignment = await _context.FacultyCourseAssignments.FindAsync(id);
            if (facultyCourseAssignment != null)
            {
                _context.FacultyCourseAssignments.Remove(facultyCourseAssignment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacultyCourseAssignmentExists(int id)
        {
            return _context.FacultyCourseAssignments.Any(e => e.Id == id);
        }
    }
}