using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_College.domain.Models.Administrator
{
    public class Branch
    {
        //This one is related with the information about the place and is connected with Courses.
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        // Navigation property to related courses. Allows access to the collection of Course entities associated with this Branch.
        public ICollection<Course> Courses { get; set; } = new List<Course>(); 
    }
}
