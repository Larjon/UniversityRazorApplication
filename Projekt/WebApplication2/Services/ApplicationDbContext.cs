using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Services
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var client = new IdentityRole("client");
            client.NormalizedName = "client";

            var seller = new IdentityRole("seller");
            seller.NormalizedName = "seller";

            builder.Entity<IdentityRole>().HasData(admin, client, seller);
        }
       
         public DbSet<Product> Products { get; set; }
        /*
               public class ApplicationDbContext : DbContext
           {
               public ApplicationDbContext(DbContextOptions options) : base(options)
               {

               }

               public DbSet<Product> Products { get; set; }
           }

           }

           public class ApplicationDbContext : DbContext
           {
               public ApplicationDbContext(DbContextOptions options) : base(options)
               {

               }

               public DbSet<Product> Products { get; set; }
           }
               */
    }

}
