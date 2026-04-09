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

        public async Task<IActionResult> Index()
        {
            var facultyProfiles = await _context.FacultyProfiles
                .Include(f => f.FacultyCourseAssignments)
                    .ThenInclude(fca => fca.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .ToListAsync();

            return View(facultyProfiles);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var facultyProfile = await _context.FacultyProfiles
    .Include(f => f.FacultyCourseAssignments)
        .ThenInclude(fca => fca.BranchCourse)
            .ThenInclude(bc => bc.Course)
    .Include(f => f.FacultyCourseAssignments)
        .ThenInclude(fca => fca.BranchCourse)
            .ThenInclude(bc => bc.Branch)
    .Include(f => f.ChangeHistories)
    .FirstOrDefaultAsync(m => m.Id == id);

            if (facultyProfile == null)
                return NotFound();

            return View(facultyProfile);
        }

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

        [HttpGet]
        public async Task<JsonResult> GetCoursesByBranch(int branchId)
        {
            var courses = await _context.BranchCourses
                .Include(bc => bc.Course)
                .Where(bc => bc.BranchId == branchId)
                .Select(bc => new
                {
                    id = bc.Id,
                    name = bc.Course.Name + " - Start: " + bc.Course.StartDate.ToString("yyyy-MM-dd")
                })
                .ToListAsync();

            return Json(courses);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FacultyProfileCreateViewModel model)
        {
            await LoadCreateViewData(model.BranchId);
            await ValidateSelectedCoursesAsync(model.BranchId, model.SelectedBranchCourseIds);

            if (!ModelState.IsValid)
                return View(model);

            string facultyNumber = await GenerateUniqueFacultyNumberAsync();

            var faculty = new FacultyProfile
            {
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone ?? "",
                CreatedAt = DateTime.Now,
                FacultyNumber = facultyNumber,
                IdentityUserId = "faculty" + facultyNumber,
                SystemEmail = $"{facultyNumber}@college.com",
                SystemPassword = $"{facultyNumber}@college.com"
            };

            _context.FacultyProfiles.Add(faculty);
            await _context.SaveChangesAsync();

            foreach (var branchCourseId in model.SelectedBranchCourseIds.Distinct())
            {
                _context.FacultyCourseAssignments.Add(new FacultyCourseAssignment
                {
                    FacultyProfileId = faculty.Id,
                    BranchCourseId = branchCourseId
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

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

            var selectedAssignments = faculty.FacultyCourseAssignments.ToList();
            if (selectedAssignments.Any())
            {
                var firstBranchCourseId = selectedAssignments.First().BranchCourseId;

                var firstBranchCourse = await _context.BranchCourses
                    .FirstOrDefaultAsync(bc => bc.Id == firstBranchCourseId);

                if (firstBranchCourse != null)
                    selectedBranchId = firstBranchCourse.BranchId;
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
                SelectedBranchCourseIds = selectedAssignments
                    .Select(a => a.BranchCourseId)
                    .ToList(),
                ChangeJustification = ""
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
         Text = bc.Course.Name + " - Start: " + bc.Course.StartDate.ToString("yyyy-MM-dd")
     })
     .ToListAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FacultyProfileEditViewModel model)
        {
            if (id != model.Id)
                return NotFound();

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
                    Text = bc.Course.Name + " - Start: " + bc.Course.StartDate.ToString("yyyy-MM-dd")
                })
                .ToListAsync();

            await ValidateSelectedCoursesAsync(model.BranchId, model.SelectedBranchCourseIds);

            var faculty = await _context.FacultyProfiles
                .Include(f => f.FacultyCourseAssignments)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (faculty == null)
                return NotFound();

            var existingCourseIds = faculty.FacultyCourseAssignments
                .Select(a => a.BranchCourseId)
                .OrderBy(x => x)
                .ToList();

            var newCourseIds = model.SelectedBranchCourseIds
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            bool coursesChanged = !existingCourseIds.SequenceEqual(newCourseIds);

            if (coursesChanged && string.IsNullOrWhiteSpace(model.ChangeJustification))
            {
                ModelState.AddModelError("ChangeJustification", "Justification is required when adding or removing courses.");
            }

            if (!ModelState.IsValid)
                return View(model);

            faculty.FullName = model.FullName;
            faculty.Email = model.Email;
            faculty.Phone = model.Phone ?? "";

            var removedCourseIds = existingCourseIds.Except(newCourseIds).ToList();
            var addedCourseIds = newCourseIds.Except(existingCourseIds).ToList();

            foreach (var removedId in removedCourseIds)
            {
                var removedBranchCourse = await _context.BranchCourses
                    .Include(bc => bc.Course)
                    .Include(bc => bc.Branch)
                    .FirstOrDefaultAsync(bc => bc.Id == removedId);

                if (removedBranchCourse != null)
                {
                    _context.FacultyCourseAssignmentChangeHistories.Add(new FacultyCourseAssignmentChangeHistory
                    {
                        FacultyProfileId = faculty.Id,
                        ActionType = "Removed",
                        CourseName = removedBranchCourse.Course?.Name ?? "",
                        BranchName = removedBranchCourse.Branch?.Name ?? "",
                        CourseStartDate = removedBranchCourse.Course?.StartDate,
                        Justification = model.ChangeJustification ?? "",
                        ChangedAt = DateTime.Now
                    });
                }
            }

            foreach (var addedId in addedCourseIds)
            {
                var addedBranchCourse = await _context.BranchCourses
                    .Include(bc => bc.Course)
                    .Include(bc => bc.Branch)
                    .FirstOrDefaultAsync(bc => bc.Id == addedId);

                if (addedBranchCourse != null)
                {
                    _context.FacultyCourseAssignmentChangeHistories.Add(new FacultyCourseAssignmentChangeHistory
                    {
                        FacultyProfileId = faculty.Id,
                        ActionType = "Added",
                        CourseName = addedBranchCourse.Course?.Name ?? "",
                        BranchName = addedBranchCourse.Branch?.Name ?? "",
                        CourseStartDate = addedBranchCourse.Course?.StartDate,
                        Justification = model.ChangeJustification ?? "",
                        ChangedAt = DateTime.Now
                    });
                }
            }

            _context.FacultyCourseAssignments.RemoveRange(faculty.FacultyCourseAssignments);

            foreach (var branchCourseId in model.SelectedBranchCourseIds.Distinct())
            {
                _context.FacultyCourseAssignments.Add(new FacultyCourseAssignment
                {
                    FacultyProfileId = faculty.Id,
                    BranchCourseId = branchCourseId
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

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

        private async Task<string> GenerateUniqueFacultyNumberAsync()
        {
            var random = new Random();
            string facultyNumber;
            bool exists;

            do
            {
                facultyNumber = random.Next(0, 100000).ToString("D5");
                exists = await _context.FacultyProfiles.AnyAsync(f => f.FacultyNumber == facultyNumber);
            }
            while (exists);

            return facultyNumber;
        }

        private async Task LoadCreateViewData(int branchId)
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
                .Where(bc => bc.BranchId == branchId)
                .Select(bc => new SelectListItem
                {
                    Value = bc.Id.ToString(),
                    Text = bc.Course.Name + " - Start: " + bc.Course.StartDate.ToString("yyyy-MM-dd")
                })
                .ToListAsync();
        }

        private async Task ValidateSelectedCoursesAsync(int branchId, List<int> selectedBranchCourseIds)
        {
            if (selectedBranchCourseIds == null || !selectedBranchCourseIds.Any())
            {
                ModelState.AddModelError("SelectedBranchCourseIds", "Select at least one course.");
                return;
            }

            var selectedIds = selectedBranchCourseIds.Distinct().ToList();

            var selectedBranchCourses = await _context.BranchCourses
                .Where(bc => selectedIds.Contains(bc.Id))
                .ToListAsync();

            if (selectedBranchCourses.Count != selectedIds.Count)
            {
                ModelState.AddModelError("SelectedBranchCourseIds", "One or more selected courses are invalid.");
                return;
            }

            if (selectedBranchCourses.Any(bc => bc.BranchId != branchId))
            {
                ModelState.AddModelError("SelectedBranchCourseIds", "All selected courses must belong to the same city/branch.");
            }
        }
    }
}