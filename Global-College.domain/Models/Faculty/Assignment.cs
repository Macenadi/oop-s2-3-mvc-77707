using Global_College.domain.Models.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_College.domain.Models.Faculty
{
    public class Assignment
    {
        public int Id { get; set; }            // Primary key for the Assignment entity.
        public string Title { get; set; } = string.Empty;    // Title of the assignment.
        public int MaxScore { get; set; } 
        public DateOnly DueDate { get; set; }   // Due date for the assignment.
        public Course? Course { get; set; }     // Navigation property to Course. Allows access to the related Course entity.
        public int CourseId { get; set; }       // Foreign key to Course. Calls the ID from the Course class.


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
