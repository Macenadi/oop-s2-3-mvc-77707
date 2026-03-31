using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_College.domain.Models.Administrator
{
    public class Course
    {
        // Represents a course and its associated branch.
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Branch? Branch { get; set; }     // Navigation property to Branch. Allows access to the related Branch entity.
        public int BranchId { get; set; }       // Foreign key to Branch. Calls the ID from the Branch class.

        // Validates that EndDate is not earlier than StartDate
        public void Validate() 
        {
            if (EndDate < StartDate)
            {
                throw new ArgumentException("End date cannot be earlier than start date.");
            }
        }
    }
}
