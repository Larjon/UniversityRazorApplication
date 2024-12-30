using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Pages.Admin.Products
{
    public class DeleteModel : PageModel
    {

        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;


        public DeleteModel(IWebHostEnvironment environment, ApplicationDbContext context)
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
                Response.Redirect("/Admin/Products/index");
                return;
            }

            string imageFullPath = environment.WebRootPath + "/products/" + product.ImageFileName;
            System.IO.File.Delete(imageFullPath);

            context.Products.Remove(product);
            context.SaveChanges();

            Response.Redirect("/Admin/Products/Index");        
        }
    }
}
