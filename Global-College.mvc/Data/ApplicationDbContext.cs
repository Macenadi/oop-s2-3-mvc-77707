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
            : base(options)
        {
        }

        // =========================
        // DbSets
        // =========================

        // Administrator
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<BranchCourse> BranchCourses { get; set; }
        public DbSet<CourseEnrolment> CourseEnrolments { get; set; }
        public DbSet<CourseEnrolmentChangeHistory> CourseEnrolmentChangeHistories { get; set; } // NOVO
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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // =========================
            // RELATIONSHIPS
            // =========================

            // BranchCourse -> Branch
            builder.Entity<BranchCourse>()
                .HasOne(bc => bc.Branch)
                .WithMany(b => b.BranchCourses)
                .HasForeignKey(bc => bc.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // BranchCourse -> Course
            builder.Entity<BranchCourse>()
                .HasOne(bc => bc.Course)
                .WithMany(c => c.BranchCourses)
                .HasForeignKey(bc => bc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique Branch + Course
            builder.Entity<BranchCourse>()
                .HasIndex(bc => new { bc.BranchId, bc.CourseId })
                .IsUnique();

            // CourseEnrolment -> BranchCourse
            builder.Entity<CourseEnrolment>()
                .HasOne(ce => ce.BranchCourse)
                .WithMany(bc => bc.CourseEnrolments)
                .HasForeignKey(ce => ce.BranchCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // CourseEnrolment -> Student
            builder.Entity<CourseEnrolment>()
                .HasOne(ce => ce.StudentProfile)
                .WithMany(sp => sp.CourseEnrolments)
                .HasForeignKey(ce => ce.StudentProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevent duplicate enrolments
            builder.Entity<CourseEnrolment>()
                .HasIndex(ce => new { ce.StudentProfileId, ce.BranchCourseId })
                .IsUnique();

            // NOVO: CourseEnrolmentChangeHistory -> CourseEnrolment
            builder.Entity<CourseEnrolmentChangeHistory>()
                .HasOne(h => h.CourseEnrolment)
                .WithMany(e => e.ChangeHistories)
                .HasForeignKey(h => h.CourseEnrolmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // FacultyCourseAssignment -> FacultyProfile
            builder.Entity<FacultyCourseAssignment>()
                .HasOne(fca => fca.FacultyProfile)
                .WithMany(fp => fp.FacultyCourseAssignments)
                .HasForeignKey(fca => fca.FacultyProfileId)
                .OnDelete(DeleteBehavior.Restrict);

            // FacultyCourseAssignment -> BranchCourse
            builder.Entity<FacultyCourseAssignment>()
                .HasOne(fca => fca.BranchCourse)
                .WithMany()
                .HasForeignKey(fca => fca.BranchCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevent duplicates
            builder.Entity<FacultyCourseAssignment>()
                .HasIndex(fca => new { fca.FacultyProfileId, fca.BranchCourseId })
                .IsUnique();

            // =========================
            // SEED DATA
            // =========================

            // Branches
            builder.Entity<Branch>().HasData(
                new Branch { Id = 1, Name = "Dublin", Address = "City Centre" },
                new Branch { Id = 2, Name = "Cork", Address = "Main Street" },
                new Branch { Id = 3, Name = "Galway", Address = "West District" }
            );

            // Courses
            builder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Computer Science", StartDate = new DateOnly(2025, 1, 1), EndDate = new DateOnly(2025, 12, 31) },
                new Course { Id = 2, Name = "Business", StartDate = new DateOnly(2025, 1, 1), EndDate = new DateOnly(2025, 12, 31) },
                new Course { Id = 3, Name = "Engineering", StartDate = new DateOnly(2025, 1, 1), EndDate = new DateOnly(2025, 12, 31) },
                new Course { Id = 4, Name = "Design", StartDate = new DateOnly(2025, 1, 1), EndDate = new DateOnly(2025, 12, 31) },
                new Course { Id = 5, Name = "Marketing", StartDate = new DateOnly(2025, 1, 1), EndDate = new DateOnly(2025, 12, 31) }
            );

            // BranchCourse (N:N)
            builder.Entity<BranchCourse>().HasData(
                new BranchCourse { Id = 1, BranchId = 1, CourseId = 1 },
                new BranchCourse { Id = 2, BranchId = 1, CourseId = 2 },
                new BranchCourse { Id = 3, BranchId = 2, CourseId = 1 },
                new BranchCourse { Id = 4, BranchId = 2, CourseId = 3 },
                new BranchCourse { Id = 5, BranchId = 3, CourseId = 4 },
                new BranchCourse { Id = 6, BranchId = 3, CourseId = 5 }
            );

            // Students
            var students = new List<StudentProfile>();
            for (int i = 1; i <= 50; i++)
            {
                students.Add(new StudentProfile
                {
                    Id = i,
                    IdentityUserId = $"student-user-{i}",
                    FullName = $"Student {i}",
                    Email = $"student{i}@college.com",
                    Phone = $"000000{i}",
                    Address = $"Address {i}",
                    StudentNumber = $"S{i:D4}"
                });
            }
            builder.Entity<StudentProfile>().HasData(students);

            // Faculty
            var faculty = new List<FacultyProfile>();
            for (int i = 1; i <= 10; i++)
            {
                string facultyNumber = i.ToString("D5");

                faculty.Add(new FacultyProfile
                {
                    Id = i,
                    IdentityUserId = $"faculty{facultyNumber}",
                    FullName = $"Faculty {i}",
                    Email = $"faculty{i}@college.com",
                    Phone = $"999999{i}",
                    FacultyNumber = facultyNumber,
                    SystemEmail = $"{facultyNumber}@college.com",
                    SystemPassword = $"{facultyNumber}@college.com"
                });
            }
            builder.Entity<FacultyProfile>().HasData(faculty);

            // Enrolments (NOW USING BranchCourseId)
            var enrolments = new List<CourseEnrolment>();
            for (int i = 1; i <= 50; i++)
            {
                enrolments.Add(new CourseEnrolment
                {
                    Id = i,
                    StudentProfileId = i,
                    BranchCourseId = ((i - 1) % 6) + 1,
                    EnrolDate = new DateOnly(2025, 1, 1),
                    Status = "Enrolled"
                });
            }
            builder.Entity<CourseEnrolment>().HasData(enrolments);
        }
    }
}