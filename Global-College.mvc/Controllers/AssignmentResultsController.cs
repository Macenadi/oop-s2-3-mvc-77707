using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Global_College.domain.Models.Faculty;
using Global_College.mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Global_College.mvc.Controllers
{
    [Authorize(Roles = "Administrator,Faculty")]
    public class AssignmentResultsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AssignmentResultsController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AssignmentResults
        public async Task<IActionResult> Index(int? assignmentId = null)
        {
            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var query = _context.AssignmentResults
                .Include(a => a.Assignment)
                    .ThenInclude(a => a.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(a => a.Assignment)
                    .ThenInclude(a => a.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(a => a.StudentProfile)
                .Where(ar => allowedBranchCourseIds.Contains(ar.Assignment.BranchCourseId))
                .AsQueryable();

            if (assignmentId.HasValue)
            {
                var selectedAssignment = await _context.Assignments
                    .Include(a => a.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                    .FirstOrDefaultAsync(a =>
                        a.Id == assignmentId.Value &&
                        allowedBranchCourseIds.Contains(a.BranchCourseId));

                if (selectedAssignment == null)
                {
                    return Forbid();
                }

                query = query.Where(ar => ar.AssignmentId == assignmentId.Value);

                ViewBag.SelectedAssignmentTitle = selectedAssignment.Title;
                ViewBag.SelectedClassCode = selectedAssignment.BranchCourse?.ClassCode;
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

            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var assignmentResult = await _context.AssignmentResults
                .Include(a => a.Assignment)
                    .ThenInclude(a => a.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(a => a.Assignment)
                    .ThenInclude(a => a.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(a => a.StudentProfile)
                .FirstOrDefaultAsync(m =>
                    m.Id == id &&
                    allowedBranchCourseIds.Contains(m.Assignment.BranchCourseId));

            if (assignmentResult == null)
            {
                return NotFound();
            }

            return View(assignmentResult);
        }

        // GET: AssignmentResults/Create
        public async Task<IActionResult> Create(int? assignmentId = null)
        {
            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();
            var model = new AssignmentResult();

            if (assignmentId.HasValue)
            {
                var assignmentAllowed = await _context.Assignments
                    .AnyAsync(a =>
                        a.Id == assignmentId.Value &&
                        allowedBranchCourseIds.Contains(a.BranchCourseId));

                if (!assignmentAllowed)
                {
                    return Forbid();
                }

                model.AssignmentId = assignmentId.Value;
            }

            await LoadDropDowns(model.AssignmentId, model.StudentProfileId);
            return View(model);
        }

        // POST: AssignmentResults/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Score,Feedback,StudentProfileId,AssignmentId")] AssignmentResult assignmentResult)
        {
            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var assignment = await _context.Assignments
                .Include(a => a.BranchCourse)
                .FirstOrDefaultAsync(a => a.Id == assignmentResult.AssignmentId);

            if (assignment == null)
            {
                ModelState.AddModelError(nameof(assignmentResult.AssignmentId), "Invalid assignment.");
            }
            else if (!allowedBranchCourseIds.Contains(assignment.BranchCourseId))
            {
                return Forbid();
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

            await LoadDropDowns(assignmentResult.AssignmentId, assignmentResult.StudentProfileId);
            return View(assignmentResult);
        }

        // GET: AssignmentResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var assignmentResult = await _context.AssignmentResults
                .Include(ar => ar.Assignment)
                .FirstOrDefaultAsync(ar =>
                    ar.Id == id &&
                    allowedBranchCourseIds.Contains(ar.Assignment.BranchCourseId));

            if (assignmentResult == null)
            {
                return NotFound();
            }

            await LoadDropDowns(assignmentResult.AssignmentId, assignmentResult.StudentProfileId);
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

            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var existingResult = await _context.AssignmentResults
                .Include(ar => ar.Assignment)
                .FirstOrDefaultAsync(ar =>
                    ar.Id == id &&
                    allowedBranchCourseIds.Contains(ar.Assignment.BranchCourseId));

            if (existingResult == null)
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
            else if (!allowedBranchCourseIds.Contains(assignment.BranchCourseId))
            {
                return Forbid();
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
                    existingResult.Score = assignmentResult.Score;
                    existingResult.Feedback = assignmentResult.Feedback;
                    existingResult.StudentProfileId = assignmentResult.StudentProfileId;
                    existingResult.AssignmentId = assignmentResult.AssignmentId;
                    existingResult.Grade = assignmentResult.Grade;

                    _context.Update(existingResult);
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

            await LoadDropDowns(assignmentResult.AssignmentId, assignmentResult.StudentProfileId);
            return View(assignmentResult);
        }

        // GET: AssignmentResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var assignmentResult = await _context.AssignmentResults
                .Include(a => a.Assignment)
                    .ThenInclude(a => a.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .Include(a => a.Assignment)
                    .ThenInclude(a => a.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(a => a.StudentProfile)
                .FirstOrDefaultAsync(m =>
                    m.Id == id &&
                    allowedBranchCourseIds.Contains(m.Assignment.BranchCourseId));

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
            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var assignmentResult = await _context.AssignmentResults
                .Include(ar => ar.Assignment)
                .FirstOrDefaultAsync(ar =>
                    ar.Id == id &&
                    allowedBranchCourseIds.Contains(ar.Assignment.BranchCourseId));

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

        private async Task LoadDropDowns(int? selectedAssignmentId = null, int? selectedStudentProfileId = null)
        {
            var allowedBranchCourseIds = await GetAllowedBranchCourseIdsAsync();

            var assignments = await _context.Assignments
                .Include(a => a.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Include(a => a.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .Where(a => allowedBranchCourseIds.Contains(a.BranchCourseId))
                .Select(a => new
                {
                    a.Id,
                    Display = a.Title + " - " + a.BranchCourse.ClassCode + " - " + a.BranchCourse.Course.Name + " (" + a.BranchCourse.Branch.Name + ")"
                })
                .ToListAsync();

            var studentsQuery = _context.StudentProfiles.AsQueryable();

            if (selectedAssignmentId.HasValue)
            {
                var assignment = await _context.Assignments
                    .Include(a => a.BranchCourse)
                    .FirstOrDefaultAsync(a =>
                        a.Id == selectedAssignmentId.Value &&
                        allowedBranchCourseIds.Contains(a.BranchCourseId));

                if (assignment != null)
                {
                    studentsQuery = _context.CourseEnrolments
                        .Where(ce => ce.BranchCourseId == assignment.BranchCourseId)
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

            ViewData["AssignmentId"] = new SelectList(assignments, "Id", "Display", selectedAssignmentId);
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