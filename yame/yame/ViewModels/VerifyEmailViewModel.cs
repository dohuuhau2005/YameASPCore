using System.ComponentModel.DataAnnotations;

namespace yame.ViewModels
{
    public class VerifyEmailViewModel
    {
        //match Email in fill on web with Database
        [Required(ErrorMessage = "Email is required !!")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
