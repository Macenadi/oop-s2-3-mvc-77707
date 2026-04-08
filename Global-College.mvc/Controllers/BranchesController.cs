using Global_College.domain.Models.Administrator;
using Global_College.mvc.Data;
using Global_College.mvc.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Global_College.mvc.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class BranchesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BranchesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Branches
        public async Task<IActionResult> Index()
        {
            return View(await _context.Branches.ToListAsync());
        }

        // GET: Branches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches
                .Include(b => b.BranchCourses)
                    .ThenInclude(bc => bc.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // GET: Branches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(branch);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Branch created successfully.";
                return RedirectToAction(nameof(Index));
            }

            return View(branch);
        }

        // GET: Branches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches
                .Include(b => b.BranchCourses)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (branch == null)
            {
                return NotFound();
            }

            var viewModel = new BranchEditViewModel
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address,
                RequiresAdminPasswordConfirmation = branch.BranchCourses.Any()
            };

            return View(viewModel);
        }

        // POST: Branches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BranchEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            var branch = await _context.Branches
                .Include(b => b.BranchCourses)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (branch == null)
            {
                return NotFound();
            }

            var requiresPasswordConfirmation = branch.BranchCourses.Any();
            model.RequiresAdminPasswordConfirmation = requiresPasswordConfirmation;

            if (requiresPasswordConfirmation)
            {
                if (string.IsNullOrWhiteSpace(model.AdminPassword))
                {
                    ModelState.AddModelError("AdminPassword", "Admin password is required because this branch is already linked to one or more branch courses.");
                }
                else
                {
                    var currentUser = await _userManager.GetUserAsync(User);

                    if (currentUser == null || !await _userManager.CheckPasswordAsync(currentUser, model.AdminPassword))
                    {
                        ModelState.AddModelError("AdminPassword", "Invalid admin password.");
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            branch.Name = model.Name;
            branch.Address = model.Address;

            try
            {
                _context.Update(branch);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Branch updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(model.Id))
                {
                    return NotFound();
                }

                throw;
            }
        }

        // GET: Branches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches
                .Include(b => b.BranchCourses)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (branch == null)
            {
                return NotFound();
            }

            ViewBag.CanDelete = !branch.BranchCourses.Any();

            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branch = await _context.Branches
                .Include(b => b.BranchCourses)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (branch == null)
            {
                return NotFound();
            }

            if (branch.BranchCourses.Any())
            {
                TempData["ErrorMessage"] = "This branch cannot be deleted because it is linked to one or more branch courses.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Branches.Remove(branch);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Branch deleted successfully.";
            }
            catch (DbUpdateException)
            {
                TempData["ErrorMessage"] = "This branch could not be deleted because it is linked to other records.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BranchExists(int id)
        {
            return _context.Branches.Any(e => e.Id == id);
        }
    }
}