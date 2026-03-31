using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global_College.domain.Models.Administrator
{
    public class StudentProfile
    {
        public int Id { get; set; }
        public string IdentityUserId { get; set; } = string.Empty; 
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string StudentNumber { get; set; } = string.Empty; // Unique student number used to identify the student

    }
}
