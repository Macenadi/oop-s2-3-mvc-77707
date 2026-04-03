using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_College.domain.Models.Administrator
{
    public class BranchCourse
    {
        public int Id { get; set; }

        public int BranchId { get; set; }
        public Branch? Branch { get; set; } = null;

        public int CourseId { get; set; }
        public Course? Course { get; set; } = null;

        public ICollection<CourseEnrolment> CourseEnrolments { get; set; } = new List<CourseEnrolment>();
    }
}