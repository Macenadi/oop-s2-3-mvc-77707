using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Global_College.domain.Models.Faculty;
using Global_College.domain.Models.Student;

namespace Global_College.domain.Models.Administrator
{
    public class FacultyProfile
    {
        public int Id { get; set; }

        [Required]
        public string IdentityUserId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;

        // Navigation property for faculty-course assignments (one faculty to many courses)
        public ICollection<FacultyCourseAssignment> FacultyCourseAssignments { get; set; } = new List<FacultyCourseAssignment>();
    }
}
