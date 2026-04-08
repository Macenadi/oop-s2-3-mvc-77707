using Global_College.domain.Models.Faculty;
using Global_College.domain.Models.Student;
using System.ComponentModel.DataAnnotations;

namespace Global_College.domain.Models.Administrator
{
    public class StudentProfile
    {
        public int Id { get; set; }

        [Required]
        public string IdentityUserId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? Phone { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(5)]
        public string StudentNumber { get; set; } = string.Empty;

        public ICollection<CourseEnrolment> CourseEnrolments { get; set; } = new List<CourseEnrolment>();
        public ICollection<AssignmentResult> AssignmentResults { get; set; } = new List<AssignmentResult>();
        public ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
    }
}
