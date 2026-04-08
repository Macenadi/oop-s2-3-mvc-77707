using System.ComponentModel.DataAnnotations;

namespace Global_College.mvc.Models.ViewModel
{
    public class BranchEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Branch Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        public bool RequiresAdminPasswordConfirmation { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Admin Password")]
        public string? AdminPassword { get; set; }
    }
}
