using System.ComponentModel.DataAnnotations;

namespace Global_College.domain.Models.Administrator
{
    public class CourseEnrolmentChangeHistory
    {
        public int Id { get; set; }

        public int CourseEnrolmentId { get; set; }
        public CourseEnrolment? CourseEnrolment { get; set; }

        [Required]
        [StringLength(100)]
        public string ChangeType { get; set; } = string.Empty;

        [StringLength(200)]
        public string? OldValue { get; set; }

        [StringLength(200)]
        public string? NewValue { get; set; }

        [StringLength(500)]
        public string? Justification { get; set; }

        public DateTime ChangedAt { get; set; }
    }
}