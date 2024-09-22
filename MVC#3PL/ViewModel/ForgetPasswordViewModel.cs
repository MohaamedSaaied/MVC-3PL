using System.ComponentModel.DataAnnotations;

namespace MVC_3PL.ViewModel
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "E-mailame is Required !")]
        [EmailAddress(ErrorMessage = "Must be an E-mail")]
        public string Email { get; set; }
    }
}
