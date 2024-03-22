 using Microsoft.EntityFrameworkCore;
using ShoppingWeb.Models;

namespace ShoppingWeb.Repository
{
	public class SeedData
	{
		public static void SeedingData(DataContext _context)
		{
			_context.Database.Migrate();
			if(!_context.Products.Any())
			{
				CategoryModel apple = new CategoryModel { Name= "apple", Slug="aople", Description="aplewathch", Status=1};
				CategoryModel samsung = new CategoryModel { Name= "galaxi", Slug="aople", Description="ss", Status=1};
				BrandModel dell = new BrandModel { Name = "apple", Slug = "aople", Description = "aplewathch", Status = 1 };
				BrandModel sony = new BrandModel { Name = "galaxi", Slug = "aople", Description = "ss", Status = 1 };
				_context.Products.AddRange(
					new ProductModel { Name = "microsoft", Slug = "microsoft", Description = "aplewathch", Image = "1.jpg", Category = apple, Brand = dell, Price = 1222, },
					new ProductModel { Name = "pc", Slug = "pc", Description = "ppc", Image = "1.jpg", Category = samsung, Brand = sony, Price = 1222, }
				
				);
				_context.SaveChanges();
			}
		}
	}
}
