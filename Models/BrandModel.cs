using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb.Models
{
    public class BrandModel
    {
        [Key]
        public int Id { get; set; }
		[Required(ErrorMessage = "Enter Brand Name")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Enter Brand Description")]
		public string Description { get; set; }
        public string Slug { get; set; }
        public int Status { get; set; }

		public static implicit operator BrandModel(CategoryModel v)
		{
			throw new NotImplementedException();
		}
	}
}
