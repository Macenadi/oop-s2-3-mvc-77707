using System.ComponentModel.DataAnnotations;

namespace Global_College.domain.Models.Administrator
{
    public class CourseEnrolment
    {
        public int Id { get; set; }

        public DateOnly EnrolDate { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Justification { get; set; }

        public int BranchCourseId { get; set; }
        public BranchCourse? BranchCourse { get; set; } = null;

        public int StudentProfileId { get; set; }
        public StudentProfile? StudentProfile { get; set; } = null;

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Status))
            {
                throw new ArgumentException("Status cannot be empty");
            }
        }
    }
}
