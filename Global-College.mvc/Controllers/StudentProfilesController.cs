using Global_College.domain.Models.Administrator;
using Global_College.mvc.Data;
using Global_College.mvc.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Global_College.mvc.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StudentProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentProfilesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: StudentProfiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentProfiles.ToListAsync());
        }

        // GET: StudentProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentProfile = await _context.StudentProfiles
                .Include(s => s.CourseEnrolments)
                    .ThenInclude(e => e.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(s => s.CourseEnrolments)
                    .ThenInclude(e => e.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (studentProfile == null)
            {
                return NotFound();
            }

            return View(studentProfile);
        }

        // GET: StudentProfiles/Create
        public IActionResult Create()
        {
            LoadBranchCoursesDropDown();
            return View(new StudentProfileCreateViewModel());
        }

        // POST: StudentProfiles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentProfileCreateViewModel model)
        {
            var selectedBranchCourse = await _context.BranchCourses
                .Include(bc => bc.Branch)
                .Include(bc => bc.Course)
                .FirstOrDefaultAsync(bc => bc.Id == model.BranchCourseId);

            if (selectedBranchCourse == null)
            {
                ModelState.AddModelError("BranchCourseId", "Please select a valid branch course.");
                LoadBranchCoursesDropDown(model.BranchCourseId);
                return View(model);
            }

            var today = DateOnly.FromDateTime(DateTime.Today);
            var courseStartDate = selectedBranchCourse.Course.StartDate;
            var allowedWindowEnd = courseStartDate.AddDays(20);

            bool isAllowedByDateRule =
                today >= courseStartDate &&
                today <= allowedWindowEnd;

            if (!isAllowedByDateRule)
            {
                model.RequiresOverrideApproval = true;

                var suggestedBranchCourse = await _context.BranchCourses
                    .Include(bc => bc.Branch)
                    .Include(bc => bc.Course)
                    .Where(bc =>
                        bc.Course.Name == selectedBranchCourse.Course.Name &&
                        bc.Course.StartDate > today &&
                        bc.Id != selectedBranchCourse.Id)
                    .OrderBy(bc => bc.Course.StartDate)
                    .FirstOrDefaultAsync();

                if (suggestedBranchCourse != null)
                {
                    model.SuggestedBranchCourseMessage =
                        $"Suggested alternative: {suggestedBranchCourse.Branch.Name} - {suggestedBranchCourse.Course.Name} (Start Date: {suggestedBranchCourse.Course.StartDate}).";
                }
                else
                {
                    model.SuggestedBranchCourseMessage =
                        "No future start date was found for the same course.";
                }

                var overrideApproved = true;

                if (string.IsNullOrWhiteSpace(model.AdminPassword))
                {
                    ModelState.AddModelError("AdminPassword",
                        "Admin password is required because the selected course is outside the allowed enrolment period.");
                    overrideApproved = false;
                }
                else
                {
                    var currentUser = await _userManager.GetUserAsync(User);

                    if (currentUser == null || !await _userManager.CheckPasswordAsync(currentUser, model.AdminPassword))
                    {
                        ModelState.AddModelError("AdminPassword", "Invalid admin password.");
                        overrideApproved = false;
                    }
                }

                if (string.IsNullOrWhiteSpace(model.Justification))
                {
                    ModelState.AddModelError("Justification",
                        "Justification is required when enrolling outside the allowed period.");
                    overrideApproved = false;
                }

                if (!overrideApproved)
                {
                    ModelState.AddModelError("",
                        "Students can only enroll from the course start date up to 20 days after. Admin override is required.");
                }
            }

            if (!ModelState.IsValid)
            {
                LoadBranchCoursesDropDown(model.BranchCourseId);
                return View(model);
            }

            string studentNumber = await GenerateUniqueStudentNumberAsync();

            var studentProfile = new StudentProfile
            {
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone ?? string.Empty,
                Address = model.Address,
                StudentNumber = studentNumber,
                IdentityUserId = "student" + studentNumber
            };

            _context.StudentProfiles.Add(studentProfile);
            await _context.SaveChangesAsync();

            var courseEnrolment = new CourseEnrolment
            {
                StudentProfileId = studentProfile.Id,
                BranchCourseId = model.BranchCourseId,
                EnrolDate = DateOnly.FromDateTime(DateTime.Now),
                Status = "Active",
                Justification = model.RequiresOverrideApproval ? model.Justification : null
            };

            _context.CourseEnrolments.Add(courseEnrolment);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] =
                $"Student profile created successfully and enrolled. Student Number: {studentProfile.StudentNumber}";

            return RedirectToAction(nameof(Index));
        }

        // GET: StudentProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentProfile = await _context.StudentProfiles.FindAsync(id);
            if (studentProfile == null)
            {
                return NotFound();
            }

            return View(studentProfile);
        }

        // POST: StudentProfiles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdentityUserId,FullName,Email,Phone,Address,StudentNumber")] StudentProfile studentProfile)
        {
            if (id != studentProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentProfile);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Student profile updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentProfileExists(studentProfile.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
            }

            return View(studentProfile);
        }

        // GET: StudentProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentProfile = await _context.StudentProfiles
                .Include(s => s.CourseEnrolments)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (studentProfile == null)
            {
                return NotFound();
            }

            ViewBag.CanDelete = !studentProfile.CourseEnrolments.Any();

            return View(studentProfile);
        }

        // POST: StudentProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentProfile = await _context.StudentProfiles
                .Include(s => s.CourseEnrolments)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (studentProfile == null)
            {
                return NotFound();
            }

            if (studentProfile.CourseEnrolments.Any())
            {
                TempData["ErrorMessage"] =
                    "This student profile cannot be deleted because it is linked to one or more course enrolments.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.StudentProfiles.Remove(studentProfile);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Student profile deleted successfully.";
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] =
                    "This student profile could not be deleted because it is linked to other records.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool StudentProfileExists(int id)
        {
            return _context.StudentProfiles.Any(e => e.Id == id);
        }

        private async Task<string> GenerateUniqueStudentNumberAsync()
        {
            var random = new Random();
            string studentNumber;

            do
            {
                studentNumber = random.Next(0, 100000).ToString("D5");
            }
            while (await _context.StudentProfiles.AnyAsync(s => s.StudentNumber == studentNumber));

            return studentNumber;
        }

        private void LoadBranchCoursesDropDown(int? selectedBranchCourseId = null)
        {
            var branchCourses = _context.BranchCourses
                .Include(bc => bc.Branch)
                .Include(bc => bc.Course)
                .Select(bc => new
                {
                    bc.Id,
                    DisplayName = bc.Branch.Name + " - " + bc.Course.Name + " - Start: " + bc.Course.StartDate
                })
                .ToList();

            ViewData["BranchCourseId"] = new SelectList(branchCourses, "Id", "DisplayName", selectedBranchCourseId);
        }
    }
}