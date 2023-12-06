using Microsoft.EntityFrameworkCore;
using MVC_PustokPlus.Models;
using MVC_PustokPlus.Areas.Admin.ViewModels;

namespace MVC_PustokPlus.Contexts;

public class Pustoc02DbContext : DbContext
{

    public Pustoc02DbContext(DbContextOptions<Pustoc02DbContext> opt) : base(opt) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductImages> ProductImages { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Author> Authors { get; set; }
}
