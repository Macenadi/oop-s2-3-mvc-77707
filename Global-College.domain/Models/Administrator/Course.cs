using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Global_College.domain.Models.Faculty;
using Global_College.domain.Models.Student;

namespace Global_College.domain.Models.Administrator
{
    public class Course : IValidatableObject
    {
        // Represents a course and its associated branch.
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation between entities. Where course is connected with CourseEnrolments, FacultyCourseAssignment, assignments, and exams.
        // These collections allow access to the related entities associated with this Course.
        public ICollection<FacultyCourseAssignment> FacultyCourseAssignments { get; set; } = new List<FacultyCourseAssignment>();
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<BranchCourse> BranchCourses { get; set; } = new List<BranchCourse>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                yield return new ValidationResult(
                    "Course name is required.",
                    new[] { nameof(Name) });
            }
        }
    }
}