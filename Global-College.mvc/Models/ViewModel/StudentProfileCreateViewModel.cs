using System.ComponentModel.DataAnnotations;

namespace Global_College.mvc.Models.ViewModel
{
    public class StudentProfileCreateViewModel
    {
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
        [Display(Name = "Course / Branch")]
        public int BranchCourseId { get; set; }

        public bool RequiresOverrideApproval { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Admin Password")]
        public string? AdminPassword { get; set; }

        [Display(Name = "Justification")]
        [StringLength(500)]
        public string? Justification { get; set; }

        public string? SuggestedBranchCourseMessage { get; set; }
    }
}