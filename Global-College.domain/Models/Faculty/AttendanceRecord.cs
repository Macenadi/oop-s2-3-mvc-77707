using Global_College.domain.Models.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_College.domain.Models.Faculty
{
    public class AttendanceRecord
    {
        public int Id { get; set; } // Primary key for the AttendanceRecord entity.
        public int CourseEnrolmentId { get; set; }
        public DateOnly Date { get; set; } // Date of the attendance record.
        public bool IsPresent { get; set; } // Indicates whether the student was present or absent.
        public CourseEnrolment? CourseEnrolment { get; set; } // Navigation property to CourseEnrolment. Allows access to the related CourseEnrolment entity.

    }
}
