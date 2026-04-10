using Global_College.domain.Models.Administrator;
using System.Collections.Generic;

namespace Global_College.mvc.Models.ViewModel
{
    public class BranchCourseDetailsViewModel
    {
        public BranchCourse BranchCourse { get; set; } = null!;
        public List<BranchCourse> RelatedBranchCourses { get; set; } = new();
    }
}