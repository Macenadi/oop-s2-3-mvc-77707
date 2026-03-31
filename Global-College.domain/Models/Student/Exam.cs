using Global_College.domain.Models.Administrator;
using Global_College.domain.Models.Faculty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_College.domain.Models.Student
{
    public class Exam
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;     // Foreign key to Assignment. Calls the ID from the Assignment class.
        public DateOnly Date { get; set; }
        public int MaxScore { get; set; }
        public bool ResultsReleased { get; set; }
        public Course? Course { get; set; }     // Navigation property to Course. Allows access to the related Course entity.
        public int CourseId { get; set; }       // Foreign key to Course. Calls the ID from the Course class.
    }
}
