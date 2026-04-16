using Global_College.domain.Models.Administrator;
using System;

namespace Global_College.domain.Models.Faculty
{
    public class AssignmentResult
    {
        public int Id { get; set; }           // Primary key for the AssignmentResult entity.
        public int Score { get; set; }         // Score obtained by the student for the assignment.
        public string Grade { get; set; } = string.Empty;    // Calculated percentage value.
        public string Feedback { get; set; } = string.Empty;   // Feedback provided by the instructor for the assignment.

        public StudentProfile? StudentProfile { get; set; } // Navigation property to StudentProfile.
        public int StudentProfileId { get; set; } // Foreign key to the StudentProfile entity.

        public Assignment? Assignment { get; set; } // Navigation property to Assignment.
        public int AssignmentId { get; set; }  // Foreign key to Assignment.

        // Ensures the score is valid
        public void Validate()
        {
            // Ensures the score is not negative
            if (Score < 0)
            {
                throw new ArgumentException("Score cannot be negative.");
            }

            // Ensures the score does not exceed the assignment's maximum score
            if (Assignment != null && Score > Assignment.MaxScore)
            {
                throw new ArgumentException($"Score ({Score}) cannot exceed max score ({Assignment.MaxScore}).");
            }
        }
    }
}