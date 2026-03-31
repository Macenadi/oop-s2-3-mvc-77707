using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_College.domain.Models.Administrator
{
    public class CourseEnrolment
    {
        public int Id { get; set; }
        public DateOnly EnrolDate { get; set; }
        public string Status { get; set; } = String.Empty; // Status of the enrolment (e.g., "Enrolled", "Completed", "Dropped").
        public Course? Course { get; set; }     // Navigation property to Course. Allows access to the related Course entity.
        public int CourseId { get; set; }       // Foreign key to Course. Calls the ID from the Course class
        public StudentProfile? StudentProfile { get; set; }   // Navigation property to StudentProfile. Allows access to the related Student entity.
        public int StudentProfileId { get; set; }   // Foreign key to StudentProfile.Calls the ID from the StudentProfile


        // Validates that Status is not empty
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Status))
            {
                throw new ArgumentException("Status cannot be empty");
            }
        }
    }
}
