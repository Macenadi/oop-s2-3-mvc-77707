using Global_College.domain.Models.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_College.domain.Models.Faculty
{
    public class AssignmentResult
    {
        public int Id { get; set; }           // Primary key for the AssignmentResult entity.
        public int Score { get; set; }         // Score obtained by the student for the assignment.
        public string Feedback { get; set; } = string.Empty;   // Feedback provided by the instructor for the assignment.
        public StudentProfile? StudentProfile { get; set; } // Navigation property to StudentProfile. Allows access to the related StudentProfile entity.
        public int StudentProfileId { get; set; } // Foreign key to the StudentProfile entity. Calls the ID from the StudentProfile class.
        public Assignment? Assignment { get; set; } // Navigation property to Assignment. Allows access to the related Assignment entity.
        public int AssignmentId { get; set; }  // Foreign key to Assignment. Calls the ID from the Assignment class.

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
