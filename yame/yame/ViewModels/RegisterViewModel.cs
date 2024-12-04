using System.ComponentModel.DataAnnotations;

namespace yame.ViewModels
{
    public class RegisterViewModel
    {
        //    FirstName
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }


        //       LastName
        [Required(ErrorMessage = "LastName is required")]

        public string LastName { get; set; }

        //          Email
        [Required(ErrorMessage = "Email is required")]

        public string Email { get; set; }
        //        BirthDay
        [Required(ErrorMessage = "BirthDay is required")]

        public DateTime Birthday { get; set; }

        //      Password
        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "The {0} have min = {2} and max = {1}")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        //     ConfirmPassword   
        [Required(ErrorMessage = "ConfirmPassword is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password does not match the Password")]

        [Display(Name = "Confirm Password")]

        public string ConfirmPassword { get; set; }

    }
}
