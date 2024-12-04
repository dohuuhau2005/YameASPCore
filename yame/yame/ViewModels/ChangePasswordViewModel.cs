using System.ComponentModel.DataAnnotations;

namespace yame.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email is required !!")]
        [EmailAddress]
        public string Email { get; set; }
        //      Password
        [Required(ErrorMessage = "New Password is required")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "The {0} have min = {2} and max = {1}")]
        [DataType(DataType.Password)]
        [Compare("ConfirmNewPassword", ErrorMessage = "Confirm Password don't match")]
        public string NewPassword { get; set; }


        //     ConfirmPassword   
        [Required(ErrorMessage = "Confirm New Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        public string ConfirmNewPassword { get; set; }
    }
}
