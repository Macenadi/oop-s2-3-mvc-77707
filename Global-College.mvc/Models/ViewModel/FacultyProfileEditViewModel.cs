using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Global_College.mvc.Models.ViewModel
{
    public class FacultyProfileEditViewModel
    {
        public int Id { get; set; }

        public string IdentityUserId { get; set; } = string.Empty;

        public string SystemEmail { get; set; } = string.Empty;
        public string SystemPassword { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty; // personal email

        [Phone]
        public string? Phone { get; set; }

        [Required]
        [Display(Name = "City / Branch")]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "Select at least one course.")]
        public List<int> SelectedBranchCourseIds { get; set; } = new();

        public List<SelectListItem> BranchOptions { get; set; } = new();
        public List<SelectListItem> BranchCourseOptions { get; set; } = new();
    }
}