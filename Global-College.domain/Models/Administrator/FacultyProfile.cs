using System.ComponentModel.DataAnnotations;

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
        public string Email { get; set; } = string.Empty; // personal email

        [Phone]
        public string? Phone { get; set; } = string.Empty;

        public string FacultyNumber { get; set; } = string.Empty;

        public string SystemEmail { get; set; } = string.Empty;
        public string SystemPassword { get; set; } = string.Empty;

        public ICollection<FacultyCourseAssignment> FacultyCourseAssignments { get; set; } = new List<FacultyCourseAssignment>();
    }
}
