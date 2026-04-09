using System.ComponentModel.DataAnnotations;

namespace Global_College.mvc.Models.ViewModel
{
    public class CourseEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public bool RequiresAdminPasswordConfirmation { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Admin Password")]
        public string? AdminPassword { get; set; }
    }
}