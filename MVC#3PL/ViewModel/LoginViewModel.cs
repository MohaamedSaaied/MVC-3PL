using System.ComponentModel.DataAnnotations;

namespace MVC_3PL.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-mailame is Required !")]
        [EmailAddress(ErrorMessage = "Must be an E-mail")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is Required !")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "Password Must Be Atleat 5 Characters")]
        public string Password { get; set; }
        public bool RememberMe {  get; set; }

    }
}
