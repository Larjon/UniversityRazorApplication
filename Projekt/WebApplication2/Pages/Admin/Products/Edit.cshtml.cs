using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Pages.Admin.Products
{
    public class EditModel : PageModel
    {

        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public ProductDto ProductDto { get; set; } = new ProductDto();

        public Product Product { get; set; } = new Product();

        public string errorMessage = "";
        public string successMessage = "";

        public EditModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }


        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            var product = context.Products.Find(id);
            if (product == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;

            }
            ProductDto.Name = product.Name;
            ProductDto.Description = product.Description;
            ProductDto.Price = product.Price;
            ProductDto.Category = product.Category;
            ProductDto.Brand = product.Brand;

            Product = product;
        }

        public void OnPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return;

            }

            var product = context.Products.Find(id);
            if (product == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;

            }

            // Update image file if we have a new image
            string newFileName = product.ImageFileName;
            if (ProductDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(ProductDto.ImageFile.FileName);

                string imageFullPath = environment.WebRootPath + "/products/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    ProductDto.ImageFile.CopyTo(stream);
                }

                // delete old image
                string oldImageFullPath = environment.WebRootPath + "/products/" + product.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }

            // Update the product in the database

            product.Name = ProductDto.Name;
            product.Brand = ProductDto.Brand;
            product.Category = ProductDto.Category;
            product.Price = ProductDto.Price;
            product.Description = ProductDto.Description ?? "";
            product.ImageFileName = newFileName;

            context.SaveChanges();

            Product = product;

            successMessage = "Product update successfully";

            Response.Redirect("/Admin/Products/Index");
        }
    }
}
