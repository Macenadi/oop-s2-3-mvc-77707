using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Global_College.domain.Models.Administrator
{
    public class Branch
    {
        //This one is related with the information about the place and is connected with Courses.
        public int Id { get; set; }

        [Required]              // The Name property is required and has a maximum length of 100 characters. It represents the name of the branch.
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Address { get; set; } = string.Empty;

        // Navigation property to related courses. Allows access to the collection of Course entities associated with this Branch.
        public ICollection<BranchCourse> BranchCourses { get; set; } = new List<BranchCourse>();
    }
}
