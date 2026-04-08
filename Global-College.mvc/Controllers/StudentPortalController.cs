using System.Linq;
using System.Threading.Tasks;
using Global_College.mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Global_College.mvc.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentPortalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentPortalController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyExamResults()
        {
            var userId = _userManager.GetUserId(User);

            var studentProfile = await _context.StudentProfiles
                .FirstOrDefaultAsync(s => s.IdentityUserId == userId);

            if (studentProfile == null)
            {
                return NotFound("Student profile not found.");
            }

            var results = await _context.ExamResults
                .Include(er => er.Exam)
                    .ThenInclude(e => e.Course)
                .Where(er => er.StudentProfileId == studentProfile.Id &&
                             er.Exam != null &&
                             er.Exam.ResultsReleased)
                .ToListAsync();

            return View(results);
        }
    }
}