using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_College.domain.Models.Administrator
{
    public class FacultyCourseAssignment
    {
        public int Id { get; set; }            // Primary key for the FacultyCourseAssignment entity.

        // Faculty ⟷ Course(N:N)
        public Course? Course { get; set; }     // Navigation property to Course. Allows access to the related Course entity.
        public int CourseId { get; set; }       // Foreign key to Course. Calls the ID from the Course class.
        public FacultyProfile? FacultyProfile { get; set; }   // Navigation property to FacultyProfile. Allows access to the related FacultyProfile entity.
        public int FacultyProfileId { get; set; }       // Foreign key to FacultyProfile. Calls the ID from the FacultyProfile class.
    }
}

