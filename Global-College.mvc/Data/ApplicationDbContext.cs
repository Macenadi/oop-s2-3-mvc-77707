using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Global_College.domain.Models.Administrator;
using Global_College.domain.Models.Faculty;
using Global_College.domain.Models.Student;

namespace Global_College.mvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)     // This line comes with the project
        {
        }

        // Each DbSet represents one table on the database
        // Administrator
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEnrolment> CourseEnrolments { get; set; }
        public DbSet<FacultyCourseAssignment> FacultyCourseAssignments { get; set; }
        public DbSet<FacultyProfile> FacultyProfiles { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }

        // Faculty
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentResult> AssignmentResults { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }

        // Student
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
    }
}