using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Global_College.domain.Models.Faculty;
using Global_College.domain.Models.Student;

namespace Global_College.domain.Models.Administrator
{
    public class Course
    {
        // Represents a course and its associated branch.
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        // Navigation between entities. Where course is connected with CourseEnrolments, FacultyCourseAssignment, assignments, and exams.
        // These collections allow access to the related entities associated with this Course.
        public ICollection<FacultyCourseAssignment> FacultyCourseAssignments { get; set; } = new List<FacultyCourseAssignment>();
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<BranchCourse> BranchCourses { get; set; } = new List<BranchCourse>();


        // Validates that EndDate is not earlier than StartDate
        public void Validate() 
        {
            if (EndDate < StartDate)
            {
                throw new ArgumentException("End date cannot be earlier than start date.");
            }
        }
    }
}
