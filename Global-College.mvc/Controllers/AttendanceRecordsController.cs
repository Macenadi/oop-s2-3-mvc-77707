using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Global_College.domain.Models.Faculty;
using Global_College.mvc.Data;

namespace Global_College.mvc.Controllers
{
    public class AttendanceRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AttendanceRecords
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AttendanceRecords
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.StudentProfile)
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.BranchCourse)
                        .ThenInclude(bc => bc.Course);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AttendanceRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceRecord = await _context.AttendanceRecords
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.StudentProfile)
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (attendanceRecord == null)
            {
                return NotFound();
            }

            return View(attendanceRecord);
        }

        // GET: AttendanceRecords/Create
        public IActionResult Create()
        {
            LoadDropDowns();
            return View();
        }

        // POST: AttendanceRecords/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Present,CourseEnrolmentId")] AttendanceRecord attendanceRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendanceRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            LoadDropDowns(attendanceRecord.CourseEnrolmentId);
            return View(attendanceRecord);
        }

        // GET: AttendanceRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceRecord = await _context.AttendanceRecords.FindAsync(id);
            if (attendanceRecord == null)
            {
                return NotFound();
            }

            LoadDropDowns(attendanceRecord.CourseEnrolmentId);
            return View(attendanceRecord);
        }

        // POST: AttendanceRecords/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Present,CourseEnrolmentId")] AttendanceRecord attendanceRecord)
        {
            if (id != attendanceRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendanceRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceRecordExists(attendanceRecord.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            LoadDropDowns(attendanceRecord.CourseEnrolmentId);
            return View(attendanceRecord);
        }

        // GET: AttendanceRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendanceRecord = await _context.AttendanceRecords
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.StudentProfile)
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.BranchCourse)
                        .ThenInclude(bc => bc.Branch)
                .Include(a => a.CourseEnrolment)
                    .ThenInclude(ce => ce.BranchCourse)
                        .ThenInclude(bc => bc.Course)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (attendanceRecord == null)
            {
                return NotFound();
            }

            return View(attendanceRecord);
        }

        // POST: AttendanceRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendanceRecord = await _context.AttendanceRecords.FindAsync(id);

            if (attendanceRecord != null)
            {
                _context.AttendanceRecords.Remove(attendanceRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceRecordExists(int id)
        {
            return _context.AttendanceRecords.Any(e => e.Id == id);
        }

        private void LoadDropDowns(int? selectedCourseEnrolmentId = null)
        {
            var enrolments = _context.CourseEnrolments
                .Include(ce => ce.StudentProfile)
                .Include(ce => ce.BranchCourse)
                    .ThenInclude(bc => bc.Branch)
                .Include(ce => ce.BranchCourse)
                    .ThenInclude(bc => bc.Course)
                .Select(ce => new
                {
                    ce.Id,
                    Display = ce.StudentProfile.FullName + " - " +
                              ce.BranchCourse.Branch.Name + " - " +
                              ce.BranchCourse.Course.Name
                })
                .ToList();

            ViewData["CourseEnrolmentId"] = new SelectList(enrolments, "Id", "Display", selectedCourseEnrolmentId);
        }
    }
}
