using System.ComponentModel.DataAnnotations;

namespace MVC_3PL.ViewModel
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is Required !")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Password Must Be Atleat 5 Characters")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm The Password !")]
        [Compare(nameof(Password), ErrorMessage = "Password Doesn't Match")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
