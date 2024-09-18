using System.ComponentModel.DataAnnotations;

namespace MVC_3PL.ViewModel
{
	public class SignupViewModel
	{
		[Required(ErrorMessage ="Username is Required !")]
        public string UserName { get; set; }


		[Required(ErrorMessage = "Firstname is Required !")]
		public string FirstName { get; set; }


		[Required(ErrorMessage = "Lastname is Required !")]
		public string LastName { get; set; }


		[Required(ErrorMessage = "E-mailame is Required !")]
		[EmailAddress(ErrorMessage ="Must be an E-mail")]
		public string Email { get; set; }


		[Required(ErrorMessage = "Password is Required !")]
		[DataType(DataType.Password)]
		[MinLength(5,ErrorMessage ="Password Must Be Atleat 5 Characters")]
		public string Password { get; set; }


		[Required(ErrorMessage = "Confirm The Password !")]
		[Compare(nameof(Password), ErrorMessage="Password Doesn't Match")]
		[DataType(DataType.Password)]
		public string ConfirmedPassword { get; set; }


		[Required(ErrorMessage = "Please Agree !")]
		public bool IsAgree { get; set; }
	}
}
