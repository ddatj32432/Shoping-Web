using Microsoft.AspNetCore.Identity;

namespace ShoppingWeb.Models
{
	public class AppUserModel : IdentityUser
	{
		public string Occupation {  get; set; }
	}
}
