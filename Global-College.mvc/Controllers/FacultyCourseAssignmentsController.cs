using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Global_College.domain.Models.Administrator;
using Global_College.mvc.Data;

namespace Global_College.mvc.Controllers
{
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
            var applicationDbContext = _context.FacultyCourseAssignments.Include(f => f.Course).Include(f => f.FacultyProfile);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FacultyCourseAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyCourseAssignment = await _context.FacultyCourseAssignments
                .Include(f => f.Course)
                .Include(f => f.FacultyProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultyCourseAssignment == null)
            {
                return NotFound();
            }

            return View(facultyCourseAssignment);
        }

        // GET: FacultyCourseAssignments/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["FacultyProfileId"] = new SelectList(_context.FacultyProfiles, "Id", "Email");
            return View();
        }

        // POST: FacultyCourseAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,FacultyProfileId")] FacultyCourseAssignment facultyCourseAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facultyCourseAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", facultyCourseAssignment.CourseId);
            ViewData["FacultyProfileId"] = new SelectList(_context.FacultyProfiles, "Id", "Email", facultyCourseAssignment.FacultyProfileId);
            return View(facultyCourseAssignment);
        }

        // GET: FacultyCourseAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyCourseAssignment = await _context.FacultyCourseAssignments.FindAsync(id);
            if (facultyCourseAssignment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", facultyCourseAssignment.CourseId);
            ViewData["FacultyProfileId"] = new SelectList(_context.FacultyProfiles, "Id", "Email", facultyCourseAssignment.FacultyProfileId);
            return View(facultyCourseAssignment);
        }

        // POST: FacultyCourseAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,FacultyProfileId")] FacultyCourseAssignment facultyCourseAssignment)
        {
            if (id != facultyCourseAssignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facultyCourseAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyCourseAssignmentExists(facultyCourseAssignment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", facultyCourseAssignment.CourseId);
            ViewData["FacultyProfileId"] = new SelectList(_context.FacultyProfiles, "Id", "Email", facultyCourseAssignment.FacultyProfileId);
            return View(facultyCourseAssignment);
        }

        // GET: FacultyCourseAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facultyCourseAssignment = await _context.FacultyCourseAssignments
                .Include(f => f.Course)
                .Include(f => f.FacultyProfile)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facultyCourseAssignment == null)
            {
                return NotFound();
            }

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
