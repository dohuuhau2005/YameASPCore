using System.ComponentModel.DataAnnotations;

namespace yame.ViewModels
{
    public class LoginViewModel
    {
        //cần nhập email ko dc trống
        [Required(ErrorMessage = "Email is required !!")]
        [EmailAddress]
        public string Email { get; set; }

        //cần nhập password ko dc trống

        [Required(ErrorMessage = "Password is required !!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
