using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShoppingWeb.Repository.Validation;

namespace ShoppingWeb.Models
{
    public class ProductModel
    {
        [Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Enter Product Name")]
		public string Name { get; set; }

        public string Slug { get; set; }
		[Required, MinLength(4, ErrorMessage = "Enter Product Description")]

		public string Description { get; set; }
		[Required(ErrorMessage = "Enter Product Price")]
        [Range(0.01,double.MaxValue)]
        [Column(TypeName ="decimal(8, 2)")]
		public decimal Price { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Choose Product Brand")]

        public int BrandId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Choose Product Category")]

        public int CategoryId { get; set; }


        public CategoryModel Category { get; set; }

        public BrandModel Brand { get; set; }

        public String Image { get; set; } = "noimage.jpg";

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpLoad {  get; set; }
    }
}
