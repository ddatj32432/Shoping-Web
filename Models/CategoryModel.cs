using System.ComponentModel.DataAnnotations;

namespace ShoppingWeb.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Enter Category Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Category Description")]
        public string Description { get; set; }

		public string Slug { get; set; }
        public int Status { get; set; }

    }
}
