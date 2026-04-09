using Global_College.domain.Models.Administrator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Global_College.domain.Models.Faculty
{
    public class AttendanceRecord
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public bool Present { get; set; }
        public CourseEnrolment? CourseEnrolment { get; set; }
        public int CourseEnrolmentId { get; set; }

        [StringLength(500)]
        public string? ChangeJustification { get; set; }

        public void Validate()
        {
            if (Date > DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("Attendance date cannot be in the future.");
            }
        }
    }
}