using Microsoft.EntityFrameworkCore;
using MVC_OneToMany.Models;
using MVC_PustokPlusClass.Areas.Admin.Controllers;
using MVC_PustokPlusClass.Models;

namespace MVC_OneToMany.Contexts
{
    public class PustokDbContext : DbContext
    {

        public PustokDbContext(DbContextOptions<PustokDbContext> opt) : base(opt) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
