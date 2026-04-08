using System.ComponentModel.DataAnnotations;

namespace Global_College.domain.Models.Administrator
{
    public class CourseEnrolment
    {
        public int Id { get; set; }

        public DateOnly EnrolDate { get; set; }

        [Required]
        public string Status { get; set; } = "Enrolled";

        [StringLength(500)]
        public string? Justification { get; set; }

        public DateOnly? StoppedDate { get; set; }

        public DateTime? LastChangedAt { get; set; }

        [StringLength(500)]
        public string? LastChangeJustification { get; set; }

        public int BranchCourseId { get; set; }
        public BranchCourse? BranchCourse { get; set; }

        public int StudentProfileId { get; set; }
        public StudentProfile? StudentProfile { get; set; }

        public ICollection<CourseEnrolmentChangeHistory> ChangeHistories { get; set; } = new List<CourseEnrolmentChangeHistory>();

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Status))
            {
                throw new ArgumentException("Status cannot be empty");
            }
        }
    }
}
