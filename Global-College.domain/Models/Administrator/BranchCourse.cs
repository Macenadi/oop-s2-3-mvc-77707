using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_College.domain.Models.Administrator
{
    public class BranchCourse : IValidatableObject
    {
        public int Id { get; set; }

        public int BranchId { get; set; }
        public Branch? Branch { get; set; } = null;

        public int CourseId { get; set; }
        public Course? Course { get; set; } = null;

        [Required]
        [StringLength(50)]
        public string ClassCode { get; set; } = string.Empty;

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        public ICollection<CourseEnrolment> CourseEnrolments { get; set; } = new List<CourseEnrolment>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate < StartDate)
            {
                yield return new ValidationResult(
                    "End date cannot be earlier than start date.",
                    new[] { nameof(EndDate) });
            }
        }
    }
}