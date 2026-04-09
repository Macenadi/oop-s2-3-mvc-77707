using Global_College.domain.Models.Administrator;
using Global_College.mvc.Data;
using Global_College.mvc.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Global_College.mvc.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class FacultyProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacultyProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // INDEX
        // =========================
        public async Task<IActionResult> Index()
        {
            var facultyProfiles = await _context.FacultyProfiles
                .Include(f => f.FacultyCourseAssignments)
                    .ThenInclude(fca => fca.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .ToListAsync();

            return View(facultyProfiles);
        }

        // =========================
        // DETAILS
        // =========================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var facultyProfile = await _context.FacultyProfiles
                .Include(f => f.FacultyCourseAssignments)
                    .ThenInclude(fca => fca.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (facultyProfile == null)
                return NotFound();

            return View(facultyProfile);
        }

        // =========================
        // CREATE - GET
        // =========================
        public async Task<IActionResult> Create()
        {
            var model = new FacultyProfileCreateViewModel();

            ViewBag.BranchOptions = await _context.Branches
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                })
                .ToListAsync();

            ViewBag.BranchCourseOptions = new List<SelectListItem>();

            return View(model);
        }

        // =========================
        // CREATE - AJAX
        // =========================
        [HttpGet]
        public async Task<JsonResult> GetCoursesByBranch(int branchId)
        {
            var courses = await _context.BranchCourses
                .Include(bc => bc.Course)
                .Where(bc => bc.BranchId == branchId)
                .Select(bc => new
                {
                    id = bc.Id,
                    name = bc.Course.Name
                })
                .ToListAsync();

            return Json(courses);
        }

        // =========================
        // CREATE - POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FacultyProfileCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BranchOptions = await _context.Branches
                    .Select(b => new SelectListItem
                    {
                        Value = b.Id.ToString(),
                        Text = b.Name
                    })
                    .ToListAsync();

                ViewBag.BranchCourseOptions = await _context.BranchCourses
                    .Include(bc => bc.Course)
                    .Where(bc => bc.BranchId == model.BranchId)
                    .Select(bc => new SelectListItem
                    {
                        Value = bc.Id.ToString(),
                        Text = bc.Course.Name
                    })
                    .ToListAsync();

                return View(model);
            }

            string facultyNumber = new Random().Next(10000, 99999).ToString();

            var faculty = new FacultyProfile
            {
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone ?? "",
                IdentityUserId = "faculty" + facultyNumber,
                SystemEmail = $"{facultyNumber}@college.com",
                SystemPassword = "TempPassword123!"
            };

            _context.FacultyProfiles.Add(faculty);
            await _context.SaveChangesAsync();

            if (model.SelectedBranchCourseIds != null && model.SelectedBranchCourseIds.Any())
            {
                foreach (var branchCourseId in model.SelectedBranchCourseIds.Distinct())
                {
                    _context.FacultyCourseAssignments.Add(new FacultyCourseAssignment
                    {
                        FacultyProfileId = faculty.Id,
                        BranchCourseId = branchCourseId
                    });
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // EDIT - GET
        // =========================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var faculty = await _context.FacultyProfiles
                .Include(f => f.FacultyCourseAssignments)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (faculty == null)
                return NotFound();

            int selectedBranchId = 0;

            var firstAssignment = faculty.FacultyCourseAssignments.FirstOrDefault();
            if (firstAssignment != null)
            {
                var branchCourse = await _context.BranchCourses
                    .FirstOrDefaultAsync(bc => bc.Id == firstAssignment.BranchCourseId);

                if (branchCourse != null)
                    selectedBranchId = branchCourse.BranchId;
            }

            var model = new FacultyProfileEditViewModel
            {
                Id = faculty.Id,
                IdentityUserId = faculty.IdentityUserId,
                SystemEmail = faculty.SystemEmail,
                SystemPassword = faculty.SystemPassword,
                FullName = faculty.FullName,
                Email = faculty.Email,
                Phone = faculty.Phone,
                BranchId = selectedBranchId,
                SelectedBranchCourseIds = faculty.FacultyCourseAssignments
                    .Select(a => a.BranchCourseId)
                    .ToList()
            };

            model.BranchOptions = await _context.Branches
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                })
                .ToListAsync();

            model.BranchCourseOptions = await _context.BranchCourses
                .Include(bc => bc.Course)
                .Where(bc => bc.BranchId == selectedBranchId)
                .Select(bc => new SelectListItem
                {
                    Value = bc.Id.ToString(),
                    Text = bc.Course.Name
                })
                .ToListAsync();

            return View(model);
        }

        // =========================
        // EDIT - POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FacultyProfileEditViewModel model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                model.BranchOptions = await _context.Branches
                    .Select(b => new SelectListItem
                    {
                        Value = b.Id.ToString(),
                        Text = b.Name
                    })
                    .ToListAsync();

                model.BranchCourseOptions = await _context.BranchCourses
                    .Include(bc => bc.Course)
                    .Where(bc => bc.BranchId == model.BranchId)
                    .Select(bc => new SelectListItem
                    {
                        Value = bc.Id.ToString(),
                        Text = bc.Course.Name
                    })
                    .ToListAsync();

                return View(model);
            }

            var faculty = await _context.FacultyProfiles
                .Include(f => f.FacultyCourseAssignments)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (faculty == null)
                return NotFound();

            faculty.FullName = model.FullName;
            faculty.Email = model.Email;
            faculty.Phone = model.Phone ?? "";

            var existingAssignments = faculty.FacultyCourseAssignments.ToList();
            _context.FacultyCourseAssignments.RemoveRange(existingAssignments);

            if (model.SelectedBranchCourseIds != null && model.SelectedBranchCourseIds.Any())
            {
                foreach (var branchCourseId in model.SelectedBranchCourseIds.Distinct())
                {
                    _context.FacultyCourseAssignments.Add(new FacultyCourseAssignment
                    {
                        FacultyProfileId = faculty.Id,
                        BranchCourseId = branchCourseId
                    });
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // =========================
        // DELETE - GET
        // =========================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var facultyProfile = await _context.FacultyProfiles
                .Include(f => f.FacultyCourseAssignments)
                    .ThenInclude(fca => fca.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (facultyProfile == null)
                return NotFound();

            return View(facultyProfile);
        }

        // =========================
        // DELETE - POST
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facultyProfile = await _context.FacultyProfiles
                .Include(f => f.FacultyCourseAssignments)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (facultyProfile != null)
            {
                _context.FacultyCourseAssignments.RemoveRange(facultyProfile.FacultyCourseAssignments);
                _context.FacultyProfiles.Remove(facultyProfile);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}