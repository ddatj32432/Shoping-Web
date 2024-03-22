using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingWeb.Models;
using ShoppingWeb.Repository;

namespace ShoppingWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize]
	public class ProductController : Controller
	{
		private readonly DataContext _dataContext;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ProductController(DataContext context, IWebHostEnvironment webHostEnvironment)
		{
			_dataContext = context;
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<IActionResult> Index()
		{
			return View(await _dataContext.Products.OrderByDescending(p => p.Id).Include(p => p.Category).Include(p=>p.Brand).ToListAsync());
		}
		[HttpGet]
		public IActionResult Create()
        {
			ViewBag.Categories = new SelectList(_dataContext.Categories,"Id","Name");
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name");
            return View();
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductModel product)
		{
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name",product.CategoryId);
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name",product.BrandId);

			if(ModelState.IsValid)
			{
				product.Slug = product.Name.Replace(" ","-");
				var slug = await _dataContext.Products.FirstOrDefaultAsync(p=>p.Slug == product.Slug);

				if(slug != null)
				{
					ModelState.AddModelError("", "Product already exits in database");
					return View(product);
				}
				
				if(product.ImageUpLoad != null)
					{
						string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath,"media/products");
						string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpLoad.FileName;
						string filePath = Path.Combine(uploadsDir,imageName);

						FileStream fs = new FileStream(filePath, FileMode.Create);
						await product.ImageUpLoad.CopyToAsync(fs);
						fs.Close();
						product.Image = imageName;
						
				}
				
				_dataContext.Add(product);
				await _dataContext.SaveChangesAsync();
                TempData["success"] = "Add product success";
                return RedirectToAction("Index");
            }
			else
			{
				TempData["error"] = "Model Error";
				List<string> errors = new List<string>();
				foreach (var value in ModelState.Values)
				{
					foreach(var error in value.Errors)
					{
						errors.Add(error.ErrorMessage);
					}
				}
                string errorMessage = string.Join("\n", errors);
				return BadRequest(errorMessage);
            }
            return View(product);
        }
        public async Task<IActionResult> Edit(int Id)
        {
            ProductModel product = await _dataContext.Products.FindAsync(Id);
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);


            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id,ProductModel product)
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "Name", product.CategoryId);
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "Name", product.BrandId);

            if (ModelState.IsValid)
            {
                product.Slug = product.Name.Replace(" ", "-");
                var slug = await _dataContext.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);

                if (slug != null)
                {
                    ModelState.AddModelError("", "Product already exits in database");
                    return View(product);
                }

                if (product.ImageUpLoad != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                    string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpLoad.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpLoad.CopyToAsync(fs);
                    fs.Close();
                    product.Image = imageName;

                }

                _dataContext.Update(product);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Update product success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Model Error";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            ProductModel product = await _dataContext.Products.FindAsync(Id);
            if (!string.Equals(product.Image, "noname.jpg"))
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                string ofilePathImage = Path.Combine(uploadsDir, product.Image);
                if(System.IO.File.Exists(ofilePathImage))
                {
                    System.IO.File.Delete(ofilePathImage);
                }
            }
            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
            TempData["error"] = "Delete product success";
            return RedirectToAction("Index");
        }


    }
}
