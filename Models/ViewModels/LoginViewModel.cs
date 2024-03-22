using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb.Models.ViewModels
{
	public class LoginViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Enter User Name")]
		public string Username { get; set; }
		
		[DataType(DataType.Password), Required(ErrorMessage = "Enter Password")]
		public string Password { get; set; }
		public string ReturnUrl { get; set; }
	}
}
