using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Global_College.domain.Models.Faculty;
using Global_College.mvc.Data;
using Microsoft.AspNetCore.Authorization;

namespace Global_College.mvc.Controllers
{
    [Authorize(Roles = "Administrator,Faculty")]
    public class AssignmentResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignmentResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AssignmentResults
        public async Task<IActionResult> Index(int? assignmentId = null)
        {
            var query = _context.AssignmentResults
                .Include(a => a.Assignment)
                    .ThenInclude(a => a.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(a => a.Assignment)
                    .ThenInclude(a => a.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(a => a.StudentProfile)
                .AsQueryable();

            if (assignmentId.HasValue)
            {
                query = query.Where(ar => ar.AssignmentId == assignmentId.Value);

                var selectedAssignment = await _context.Assignments
                    .Include(a => a.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                    .FirstOrDefaultAsync(a => a.Id == assignmentId.Value);

                ViewBag.SelectedAssignmentTitle = selectedAssignment?.Title;
                ViewBag.SelectedClassCode = selectedAssignment?.BranchCourse?.ClassCode;
            }

            return View(await query.ToListAsync());
        }

        // GET: AssignmentResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignmentResult = await _context.AssignmentResults
                .Include(a => a.Assignment)
                    .ThenInclude(a => a.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(a => a.Assignment)
                    .ThenInclude(a => a.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(a => a.StudentProfile)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (assignmentResult == null)
            {
                return NotFound();
            }

            return View(assignmentResult);
        }

        // GET: AssignmentResults/Create
        public IActionResult Create(int? assignmentId = null)
        {
            var model = new AssignmentResult();

            if (assignmentId.HasValue)
            {
                model.AssignmentId = assignmentId.Value;
            }

            LoadDropDowns(model.AssignmentId, model.StudentProfileId);
            return View(model);
        }

        // POST: AssignmentResults/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Score,Feedback,StudentProfileId,AssignmentId")] AssignmentResult assignmentResult)
        {
            var assignment = await _context.Assignments
                .Include(a => a.BranchCourse)
                .FirstOrDefaultAsync(a => a.Id == assignmentResult.AssignmentId);

            if (assignment == null)
            {
                ModelState.AddModelError(nameof(assignmentResult.AssignmentId), "Invalid assignment.");
            }
            else
            {
                assignmentResult.Assignment = assignment;

                try
                {
                    assignmentResult.Validate();
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(nameof(assignmentResult.Score), ex.Message);
                }

                if (assignmentResult.Score > assignment.MaxScore)
                {
                    ModelState.AddModelError(nameof(assignmentResult.Score), $"Score cannot be greater than the assignment max score ({assignment.MaxScore}).");
                }

                var studentIsInClass = await _context.CourseEnrolments
                    .AnyAsync(ce => ce.BranchCourseId == assignment.BranchCourseId &&
                                    ce.StudentProfileId == assignmentResult.StudentProfileId);

                if (!studentIsInClass)
                {
                    ModelState.AddModelError(nameof(assignmentResult.StudentProfileId), "This student is not enrolled in the selected class.");
                }

                var duplicate = await _context.AssignmentResults.AnyAsync(ar =>
                    ar.AssignmentId == assignmentResult.AssignmentId &&
                    ar.StudentProfileId == assignmentResult.StudentProfileId &&
                    ar.Id != assignmentResult.Id);

                if (duplicate)
                {
                    ModelState.AddModelError("", "This student already has a result for this assignment.");
                }

                assignmentResult.Grade = CalculateGradeValue(assignmentResult.Score, assignment.MaxScore);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(assignmentResult);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { assignmentId = assignmentResult.AssignmentId });
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "This student already has a result for this assignment.");
                }
            }

            LoadDropDowns(assignmentResult.AssignmentId, assignmentResult.StudentProfileId);
            return View(assignmentResult);
        }

        // GET: AssignmentResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignmentResult = await _context.AssignmentResults.FindAsync(id);
            if (assignmentResult == null)
            {
                return NotFound();
            }

            LoadDropDowns(assignmentResult.AssignmentId, assignmentResult.StudentProfileId);
            return View(assignmentResult);
        }

        // POST: AssignmentResults/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Score,Feedback,StudentProfileId,AssignmentId")] AssignmentResult assignmentResult)
        {
            if (id != assignmentResult.Id)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.BranchCourse)
                .FirstOrDefaultAsync(a => a.Id == assignmentResult.AssignmentId);

            if (assignment == null)
            {
                ModelState.AddModelError(nameof(assignmentResult.AssignmentId), "Invalid assignment.");
            }
            else
            {
                assignmentResult.Assignment = assignment;

                try
                {
                    assignmentResult.Validate();
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError(nameof(assignmentResult.Score), ex.Message);
                }

                if (assignmentResult.Score > assignment.MaxScore)
                {
                    ModelState.AddModelError(nameof(assignmentResult.Score), $"Score cannot be greater than the assignment max score ({assignment.MaxScore}).");
                }

                var studentIsInClass = await _context.CourseEnrolments
                    .AnyAsync(ce => ce.BranchCourseId == assignment.BranchCourseId &&
                                    ce.StudentProfileId == assignmentResult.StudentProfileId);

                if (!studentIsInClass)
                {
                    ModelState.AddModelError(nameof(assignmentResult.StudentProfileId), "This student is not enrolled in the selected class.");
                }

                var duplicate = await _context.AssignmentResults.AnyAsync(ar =>
                    ar.AssignmentId == assignmentResult.AssignmentId &&
                    ar.StudentProfileId == assignmentResult.StudentProfileId &&
                    ar.Id != assignmentResult.Id);

                if (duplicate)
                {
                    ModelState.AddModelError("", "This student already has a result for this assignment.");
                }

                assignmentResult.Grade = CalculateGradeValue(assignmentResult.Score, assignment.MaxScore);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignmentResult);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { assignmentId = assignmentResult.AssignmentId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentResultExists(assignmentResult.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "This student already has a result for this assignment.");
                }
            }

            LoadDropDowns(assignmentResult.AssignmentId, assignmentResult.StudentProfileId);
            return View(assignmentResult);
        }

        // GET: AssignmentResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignmentResult = await _context.AssignmentResults
                .Include(a => a.Assignment)
                    .ThenInclude(a => a.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(a => a.Assignment)
                    .ThenInclude(a => a.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(a => a.StudentProfile)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (assignmentResult == null)
            {
                return NotFound();
            }

            return View(assignmentResult);
        }

        // POST: AssignmentResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignmentResult = await _context.AssignmentResults.FindAsync(id);

            if (assignmentResult != null)
            {
                _context.AssignmentResults.Remove(assignmentResult);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentResultExists(int id)
        {
            return _context.AssignmentResults.Any(e => e.Id == id);
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

        private void LoadDropDowns(int? selectedAssignmentId = null, int? selectedStudentProfileId = null)
        {
            var assignments = _context.Assignments
                .Include(a => a.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(a => a.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .Select(a => new
                {
                    a.Id,
                    Display = a.Title + " - " + a.BranchCourse.ClassCode + " - " + a.BranchCourse.Course.Name + " (" + a.BranchCourse.Branch.Name + ")"
                })
                .ToList();

            var studentsQuery = _context.StudentProfiles.AsQueryable();

            if (selectedAssignmentId.HasValue)
            {
                var assignment = _context.Assignments
                    .Include(a => a.BranchCourse)
                    .FirstOrDefault(a => a.Id == selectedAssignmentId.Value);

                if (assignment != null)
                {
                    studentsQuery = _context.CourseEnrolments
                        .Where(ce => ce.BranchCourseId == assignment.BranchCourseId)
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

            ViewData["AssignmentId"] = new SelectList(assignments, "Id", "Display", selectedAssignmentId);
            ViewData["StudentProfileId"] = new SelectList(students, "Id", "FullName", selectedStudentProfileId);
        }
    }
}