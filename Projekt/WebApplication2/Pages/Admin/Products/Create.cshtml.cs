using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ProductDto ProductDto { get; set; } = new ProductDto();

        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        public CreateModel(IWebHostEnvironment environment, ApplicationDbContext context) 
        {
            this.environment = environment;
            this.context = context;

        }

        public void OnGet()
        {
        }

        public string errorMessage = "";
        public string successMessage = "";

        public void OnPost()
        {
            if (ProductDto.ImageFile == null)
            {
                ModelState.AddModelError("ProductDto.ImageFile", "The image file is required");
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return;
            }

            // save the image file
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(ProductDto.ImageFile!.FileName);

            string imageFillPath = environment.WebRootPath + "/products" + newFileName;
            using (var stream = System.IO.File.Create(imageFillPath))
            {
                ProductDto.ImageFile.CopyTo(stream);
            }

            // save the new product in the database

            Product product = new Product()
            {
                Name = ProductDto.Name,
                Brand = ProductDto.Brand,
                Category = ProductDto.Category,
                Price = ProductDto.Price,
                Description = ProductDto.Description ?? "",
                ImageFileName = newFileName,
                CreatedAt = DateTime.Now,
            };

            context.Products.Add(product);
            context.SaveChanges();

            // clear the form

            ProductDto.Name = "";
            ProductDto.Brand = "";
            ProductDto.Category = "";
            ProductDto.Price = "";
            ProductDto.Description = "";
            ProductDto.ImageFile = null;

            ModelState.Clear();

            successMessage = "Product created successfully";

            Response.Redirect("/Admin/Products/Index");
        }
    }
}
