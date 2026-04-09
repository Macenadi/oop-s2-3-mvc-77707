using System.ComponentModel.DataAnnotations;

namespace Global_College.mvc.Models.ViewModel
{
    public class FacultyProfileCreateViewModel
    {
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
    }
}