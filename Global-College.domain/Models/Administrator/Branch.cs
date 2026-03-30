using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_College.domain.Models.Administrator
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public ICollection<Course> Courses { get; set; } = new List<Course>(); // Navigation property to related courses. Allows access to the collection of Course entities associated with this Branch.
    }
}
