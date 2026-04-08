using Global_College.domain.Models.Administrator;
using Global_College.mvc.Data;
using Global_College.mvc.Models.ViewModel;
using Global_College.mvc.Services;
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
        private readonly IEmailService _emailService;

        public StudentProfilesController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
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
                .Include(s => s.CourseEnrolments)
                    .ThenInclude(e => e.ChangeHistories)
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
                ModelState.AddModelError("BranchCourseId", "Invalid course.");
                LoadBranchCoursesDropDown(model.BranchCourseId);
                return View(model);
            }

            var today = DateOnly.FromDateTime(DateTime.Today);
            var start = selectedBranchCourse.Course.StartDate;
            var limit = start.AddDays(20);

            bool allowed = today <= limit;

            if (!allowed)
            {
                model.RequiresOverrideApproval = true;

                bool ok = true;

                if (string.IsNullOrWhiteSpace(model.AdminPassword))
                {
                    ModelState.AddModelError("AdminPassword", "Admin password required.");
                    ok = false;
                }
                else
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null || !await _userManager.CheckPasswordAsync(user, model.AdminPassword))
                    {
                        ModelState.AddModelError("AdminPassword", "Invalid password.");
                        ok = false;
                    }
                }

                if (string.IsNullOrWhiteSpace(model.Justification))
                {
                    ModelState.AddModelError("Justification", "Justification required.");
                    ok = false;
                }

                if (!ok)
                {
                    LoadBranchCoursesDropDown(model.BranchCourseId);
                    return View(model);
                }
            }

            if (!ModelState.IsValid)
            {
                LoadBranchCoursesDropDown(model.BranchCourseId);
                return View(model);
            }

            // 🔥 GERAR STUDENT NUMBER
            string studentNumber = await GenerateStudentNumber();

            // 🔥 GERAR EMAIL DO SISTEMA
            string systemEmail = $"{studentNumber}@college.com";

            // 🔥 GERAR SENHA
            string systemPassword = GeneratePassword();

            var student = new StudentProfile
            {
                FullName = model.FullName,
                Email = model.Email, // EMAIL PESSOAL
                Phone = model.Phone ?? "",
                Address = model.Address,
                StudentNumber = studentNumber,
                IdentityUserId = "student" + studentNumber,

                SystemEmail = systemEmail,
                SystemPassword = systemPassword
            };

            _context.StudentProfiles.Add(student);
            await _context.SaveChangesAsync();

            var enrol = new CourseEnrolment
            {
                StudentProfileId = student.Id,
                BranchCourseId = model.BranchCourseId,
                EnrolDate = DateOnly.FromDateTime(DateTime.Now),
                Status = "Enrolled",
                Justification = model.Justification
            };

            _context.CourseEnrolments.Add(enrol);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] =
                $"Student created! Login: {systemEmail} | Password: {systemPassword}";

            return RedirectToAction(nameof(Index));
        }

        // GET: StudentProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentProfile = await _context.StudentProfiles
                .Include(s => s.CourseEnrolments)
                    .ThenInclude(e => e.BranchCourse)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (studentProfile == null)
            {
                return NotFound();
            }

            var currentEnrolment = studentProfile.CourseEnrolments
                .OrderByDescending(e => e.EnrolDate)
                .FirstOrDefault();

            var model = new StudentProfileEditViewModel
            {
                Id = studentProfile.Id,
                IdentityUserId = studentProfile.IdentityUserId,
                FullName = studentProfile.FullName,
                Email = studentProfile.Email,
                Phone = studentProfile.Phone,
                Address = studentProfile.Address,
                StudentNumber = studentProfile.StudentNumber,
                CurrentCourseEnrolmentId = currentEnrolment?.Id,
                BranchCourseId = currentEnrolment?.BranchCourseId,
                Status = currentEnrolment?.Status ?? "Enrolled"
            };

            await LoadEditDropDowns(model);
            return View(model);
        }

        // POST: StudentProfiles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentProfileEditViewModel model)
        {
            if (id != model.Id)
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
                .FirstOrDefaultAsync(s => s.Id == id);

            if (studentProfile == null)
            {
                return NotFound();
            }

            var currentEnrolment = studentProfile.CourseEnrolments
                .OrderByDescending(e => e.EnrolDate)
                .FirstOrDefault();

            if (currentEnrolment == null)
            {
                ModelState.AddModelError("", "This student does not have a course enrolment to edit.");
                await LoadEditDropDowns(model);
                return View(model);
            }

            bool branchCourseChanged = model.BranchCourseId.HasValue && model.BranchCourseId.Value != currentEnrolment.BranchCourseId;
            bool statusChanged = !string.Equals(model.Status, currentEnrolment.Status, StringComparison.OrdinalIgnoreCase);

            if ((branchCourseChanged || statusChanged) && string.IsNullOrWhiteSpace(model.ChangeJustification))
            {
                ModelState.AddModelError("ChangeJustification", "Justification is required when changing Course / Branch or Status.");
            }

            if (!ModelState.IsValid)
            {
                await LoadEditDropDowns(model);
                return View(model);
            }

            studentProfile.FullName = model.FullName;
            studentProfile.Email = model.Email;
            studentProfile.Phone = model.Phone ?? string.Empty;
            studentProfile.Address = model.Address;

            if (branchCourseChanged)
            {
                var oldBranchCourseText = $"{currentEnrolment.BranchCourse?.Branch?.Name} - {currentEnrolment.BranchCourse?.Course?.Name}";

                var newBranchCourse = await _context.BranchCourses
                    .Include(bc => bc.Branch)
                    .Include(bc => bc.Course)
                    .FirstOrDefaultAsync(bc => bc.Id == model.BranchCourseId!.Value);

                currentEnrolment.BranchCourseId = model.BranchCourseId!.Value;
                currentEnrolment.LastChangedAt = DateTime.Now;
                currentEnrolment.LastChangeJustification = model.ChangeJustification;

                var newBranchCourseText = $"{newBranchCourse?.Branch?.Name} - {newBranchCourse?.Course?.Name}";

                _context.CourseEnrolmentChangeHistories.Add(new CourseEnrolmentChangeHistory
                {
                    CourseEnrolmentId = currentEnrolment.Id,
                    ChangeType = "BranchCourseChanged",
                    OldValue = oldBranchCourseText,
                    NewValue = newBranchCourseText,
                    Justification = model.ChangeJustification,
                    ChangedAt = DateTime.Now
                });
            }

            if (statusChanged)
            {
                string oldStatus = currentEnrolment.Status;
                currentEnrolment.Status = model.Status;
                currentEnrolment.LastChangedAt = DateTime.Now;
                currentEnrolment.LastChangeJustification = model.ChangeJustification;

                if (string.Equals(model.Status, "Enrolled", StringComparison.OrdinalIgnoreCase))
                {
                    currentEnrolment.StoppedDate = null;
                }
                else
                {
                    currentEnrolment.StoppedDate ??= DateOnly.FromDateTime(DateTime.Today);
                }

                _context.CourseEnrolmentChangeHistories.Add(new CourseEnrolmentChangeHistory
                {
                    CourseEnrolmentId = currentEnrolment.Id,
                    ChangeType = "StatusChanged",
                    OldValue = oldStatus,
                    NewValue = model.Status,
                    Justification = model.ChangeJustification,
                    ChangedAt = DateTime.Now
                });
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Student profile updated successfully.";

            return RedirectToAction(nameof(Details), new { id = studentProfile.Id });
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

        private string GenerateTemporaryPassword()
        {
            return "Stu@" + Random.Shared.Next(100000, 999999);
        }

        private void LoadBranchCoursesDropDown(int? selectedBranchCourseId = null)
        {
            var branchCourses = _context.BranchCourses
                .Include(bc => bc.Branch)
                .Include(bc => bc.Course)
                .OrderBy(bc => bc.Branch.Name)
                .ThenBy(bc => bc.Course.Name)
                .ThenBy(bc => bc.Course.StartDate)
                .Select(bc => new
                {
                    bc.Id,
                    DisplayName = bc.Branch.Name + " - " + bc.Course.Name + " - Start: " + bc.Course.StartDate
                })
                .ToList();

            ViewData["BranchCourseId"] = new SelectList(branchCourses, "Id", "DisplayName", selectedBranchCourseId);
        }

        private async Task LoadEditDropDowns(StudentProfileEditViewModel model)
        {
            model.BranchCourseOptions = await _context.BranchCourses
                .Include(bc => bc.Branch)
                .Include(bc => bc.Course)
                .OrderBy(bc => bc.Branch.Name)
                .ThenBy(bc => bc.Course.Name)
                .ThenBy(bc => bc.Course.StartDate)
                .Select(bc => new SelectListItem
                {
                    Value = bc.Id.ToString(),
                    Text = bc.Branch.Name + " - " + bc.Course.Name + " - Start: " + bc.Course.StartDate,
                    Selected = model.BranchCourseId == bc.Id
                })
                .ToListAsync();

            model.StatusOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "Enrolled", Text = "Enrolled", Selected = model.Status == "Enrolled" },
                new SelectListItem { Value = "Finished", Text = "Finished", Selected = model.Status == "Finished" },
                new SelectListItem { Value = "Trancou a matricula", Text = "Trancou a matricula", Selected = model.Status == "Trancou a matricula" }
            };
        }

            private string GeneratePassword()
            {
            var rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
            }

            private async Task<string> GenerateStudentNumber()
        {
            var rnd = new Random();
            string number;

            do
            {
                number = rnd.Next(0, 100000).ToString("D5");
            }
            while (await _context.StudentProfiles.AnyAsync(s => s.StudentNumber == number));

            return number;
        }
    }
}





//me de o conteolelr atualizado sem a parte do email profissional, pois ainda nao vou fazer ეს ახლა. So o resto atualizado