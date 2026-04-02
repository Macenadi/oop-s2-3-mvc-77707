using System.ComponentModel.DataAnnotations;

namespace Global_College.domain.Models.Administrator
{
    public class CourseEnrolment
    {
        public int Id { get; set; }

        public DateOnly EnrolDate { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        public int BranchCourseId { get; set; }
        public BranchCourse? BranchCourse { get; set; }

        public int StudentProfileId { get; set; }
        public StudentProfile? StudentProfile { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Status))
            {
                throw new ArgumentException("Status cannot be empty");
            }
        }
    }
}
