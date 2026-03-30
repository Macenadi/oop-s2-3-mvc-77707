using Global_College.domain.Models.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_College.domain.Models.Student
{
    public class ExamResult
    {
        public int Id { get; set; }
        public int ExamId { get; set; }         // Foreign key to Exam. Calls the ID from the Exam class.
        public int StudentProfileId { get; set; }      // Foreign key to Student. Calls the ID from the Student class.
        public int Score { get; set; }
        public string Grade { get; set; } = string.Empty; // Grade can be represented as a string (e.g., A, B, C) or as a letter grade.
        public Exam? Exam { get; set; }         // Navigation property to Exam. Allows access to the related Exam entity.
        public StudentProfile? StudentProfile { get; set; }   // Navigation property to Student. Allows access to the related Student entity.
    }
}
