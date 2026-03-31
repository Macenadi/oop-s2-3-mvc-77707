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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // =========================
            // BRANCHES (3)
            // =========================
            builder.Entity<Branch>().HasData(
                new Branch { Id = 1, Name = "Dublin", Address = "City Centre" },
                new Branch { Id = 2, Name = "Cork", Address = "Main Street" },
                new Branch { Id = 3, Name = "Galway", Address = "West District" }
            );

            // =========================
            // COURSES (5)
            // =========================
            builder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "Computer Science", BranchId = 1, StartDate = new DateOnly(2025, 1, 1), EndDate = new DateOnly(2025, 12, 31) },
                new Course { Id = 2, Name = "Business", BranchId = 1, StartDate = new DateOnly(2025, 1, 1), EndDate = new DateOnly(2025, 12, 31) },
                new Course { Id = 3, Name = "Engineering", BranchId = 2, StartDate = new DateOnly(2025, 1, 1), EndDate = new DateOnly(2025, 12, 31) },
                new Course { Id = 4, Name = "Design", BranchId = 3, StartDate = new DateOnly(2025, 1, 1), EndDate = new DateOnly(2025, 12, 31) },
                new Course { Id = 5, Name = "Marketing", BranchId = 2, StartDate = new DateOnly(2025, 1, 1), EndDate = new DateOnly(2025, 12, 31) }
            );

            // =========================
            // STUDENTS (50)
            // =========================
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
                    StudentNumber = $"STU{i:D3}"
                });
            }
            builder.Entity<StudentProfile>().HasData(students);

            // =========================
            // FACULTY (10)
            // =========================
            var faculty = new List<FacultyProfile>();
            for (int i = 1; i <= 10; i++)
            {
                faculty.Add(new FacultyProfile
                {
                    Id = i,
                    IdentityUserId = $"faculty-user-{i}",
                    FullName = $"Faculty {i}",
                    Email = $"faculty{i}@college.com",
                    Phone = $"999999{i}"
                });
            }
            builder.Entity<FacultyProfile>().HasData(faculty);

            // =========================
            // ENROLMENTS (50)
            // =========================
            var enrolments = new List<CourseEnrolment>();
            for (int i = 1; i <= 50; i++)
            {
                enrolments.Add(new CourseEnrolment
                {
                    Id = i,
                    StudentProfileId = i,
                    CourseId = (i % 5) + 1,
                    EnrolDate = new DateOnly(2025, 1, 1),
                    Status = "Enrolled"
                });
            }
            builder.Entity<CourseEnrolment>().HasData(enrolments);

            // =========================
            // FACULTY COURSE ASSIGNMENT (10)
            // =========================
            var facultyAssignments = new List<FacultyCourseAssignment>();
            for (int i = 1; i <= 10; i++)
            {
                facultyAssignments.Add(new FacultyCourseAssignment
                {
                    Id = i,
                    FacultyProfileId = i,
                    CourseId = (i % 5) + 1
                });
            }
            builder.Entity<FacultyCourseAssignment>().HasData(facultyAssignments);

            // =========================
            // ASSIGNMENTS (5)
            // =========================
            builder.Entity<Assignment>().HasData(
                new Assignment { Id = 1, Title = "Assignment 1", CourseId = 1, MaxScore = 100, DueDate = new DateOnly(2025, 5, 1) },
                new Assignment { Id = 2, Title = "Assignment 2", CourseId = 2, MaxScore = 100, DueDate = new DateOnly(2025, 5, 2) },
                new Assignment { Id = 3, Title = "Assignment 3", CourseId = 3, MaxScore = 100, DueDate = new DateOnly(2025, 5, 3) },
                new Assignment { Id = 4, Title = "Assignment 4", CourseId = 4, MaxScore = 100, DueDate = new DateOnly(2025, 5, 4) },
                new Assignment { Id = 5, Title = "Assignment 5", CourseId = 5, MaxScore = 100, DueDate = new DateOnly(2025, 5, 5) }
            );

            // =========================
            // ASSIGNMENT RESULTS (50)
            // =========================
            var assignmentResults = new List<AssignmentResult>();
            for (int i = 1; i <= 50; i++)
            {
                assignmentResults.Add(new AssignmentResult
                {
                    Id = i,
                    AssignmentId = (i % 5) + 1,
                    StudentProfileId = i,
                    Score = 75,
                    Feedback = "Good work"
                });
            }
            builder.Entity<AssignmentResult>().HasData(assignmentResults);

            // =========================
            // ATTENDANCE (50)
            // =========================
            var attendanceRecords = new List<AttendanceRecord>();
            for (int i = 1; i <= 50; i++)
            {
                attendanceRecords.Add(new AttendanceRecord
                {
                    Id = i,
                    CourseEnrolmentId = i,
                    Date = new DateOnly(2025, 2, 1),
                    Present = true
                });
            }
            builder.Entity<AttendanceRecord>().HasData(attendanceRecords);

            // =========================
            // EXAMS (5)
            // =========================
            builder.Entity<Exam>().HasData(
                new Exam { Id = 1, Title = "Exam 1", CourseId = 1, Date = new DateOnly(2025, 6, 1), MaxScore = 100, ResultsReleased = true },
                new Exam { Id = 2, Title = "Exam 2", CourseId = 2, Date = new DateOnly(2025, 6, 2), MaxScore = 100, ResultsReleased = true },
                new Exam { Id = 3, Title = "Exam 3", CourseId = 3, Date = new DateOnly(2025, 6, 3), MaxScore = 100, ResultsReleased = true },
                new Exam { Id = 4, Title = "Exam 4", CourseId = 4, Date = new DateOnly(2025, 6, 4), MaxScore = 100, ResultsReleased = true },
                new Exam { Id = 5, Title = "Exam 5", CourseId = 5, Date = new DateOnly(2025, 6, 5), MaxScore = 100, ResultsReleased = true }
            );

            // =========================
            // EXAM RESULTS (50)
            // =========================
            var examResults = new List<ExamResult>();
            for (int i = 1; i <= 50; i++)
            {
                examResults.Add(new ExamResult
                {
                    Id = i,
                    ExamId = (i % 5) + 1,
                    StudentProfileId = i,
                    Score = 70,
                    Grade = "B"
                });
            }
            builder.Entity<ExamResult>().HasData(examResults);
        }
    }
}