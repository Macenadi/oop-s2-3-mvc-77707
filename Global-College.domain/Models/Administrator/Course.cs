using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_College.domain.Models.Administrator
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BranchId { get; set; }       // Foreign key to Branch. Calls the ID from the Branch class.
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Branch? Branch { get; set; }     // Navigation property to Branch. Allows access to the related Branch entity.
    }
}
