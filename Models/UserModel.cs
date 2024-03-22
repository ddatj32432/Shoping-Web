using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="Enter User Name")]
		public string Username { get; set; }
		[Required(ErrorMessage = "Enter User Email"), EmailAddress]
		public string Email { get; set; }
		[DataType(DataType.Password),Required(ErrorMessage ="Enter Password")]
		public string Password { get; set; }

	}
}
