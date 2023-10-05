using BOL.Models;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore;
using WebApplication2.BOL.Models;
using WebApplication2.Utilities;
namespace WebApplication2.DAL.data
{
    public class appDbContext:DbContext
    {
        public appDbContext(DbContextOptions<appDbContext> options) : base(options) { }
        public DbSet<Category> categories { set; get; }
        public DbSet<Product> products { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
			
			modelBuilder.Entity<Category>().HasData(
                new Category{ Id = "ACT".toId(),Name = "action", abb = "ACT", Description = "" },
                new Category{ Id="ADV".toId(),Name = "adventure", abb = "ADV", Description = "" }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product{Id=1,Name="witcher",Description="a witcher search for his daughter",Price=30,Author="zlofzki"},
                new Product { Id = 2, Name = "harry potter", Description = "test", Price = 30, Author = "ukien" }

                );
            
        }
    }
}
