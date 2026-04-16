using Global_College.domain.Models.Administrator;
using System;

namespace Global_College.domain.Models.Faculty
{
    public class Assignment
    {
        public int Id { get; set; }            // Primary key for the Assignment entity.
        public string Title { get; set; } = string.Empty;    // Title of the assignment.
        public int MaxScore { get; set; }
        public DateOnly DueDate { get; set; }   // Due date for the assignment.

        public BranchCourse? BranchCourse { get; set; }     // Navigation property to BranchCourse.
        public int BranchCourseId { get; set; }             // Foreign key to BranchCourse.

        // Validation for the MaxScore
        public void Validate()
        {
            if (MaxScore > 100)
            {
                throw new ArgumentException($"The MaxScore cannot be greater than 100. Current value: {MaxScore}");
            }
            if (MaxScore <= 0)
            {
                throw new ArgumentException("MaxScore must be greater than zero.");
            }
        }
    }
}