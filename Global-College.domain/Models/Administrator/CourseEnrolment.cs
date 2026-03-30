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
        public int StudentProfileId { get; set; }
        public DateOnly EnrolDate { get; set; }
        public string status { get; set; } = ""; // Status of the enrolment (e.g., "Enrolled", "Completed", "Dropped").
        public Course? Course { get; set; }     // Navigation property to Course. Allows access to the related Course entity.
        public int CourseId { get; set; }       // Foreign key to Course. Calls the ID from the Course class
        public StudentProfile? Student { get; set; }   // Navigation property to Student. Allows access to the related Student entity.
    }
}
