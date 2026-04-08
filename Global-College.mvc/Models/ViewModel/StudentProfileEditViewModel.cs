using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Global_College.mvc.Models.ViewModel
{
    public class StudentProfileEditViewModel
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
        public string? Phone { get; set; }

        [Required]
        [StringLength(150)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string StudentNumber { get; set; } = string.Empty;

        public int? CurrentCourseEnrolmentId { get; set; }

        [Display(Name = "Course / Branch")]
        public int? BranchCourseId { get; set; }

        [Required]
        public string Status { get; set; } = "Enrolled";

        [StringLength(500)]
        public string? ChangeJustification { get; set; }

        public List<SelectListItem> BranchCourseOptions { get; set; } = new();
        public List<SelectListItem> StatusOptions { get; set; } = new();
    }
}