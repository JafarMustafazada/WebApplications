using Microsoft.EntityFrameworkCore;
using MVC_PustokPlus.Models;
using MVC_PustokPlus.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MVC_PustokPlus.Contexts;

public class Pustoc02DbContext : IdentityDbContext
{
    public Pustoc02DbContext(DbContextOptions<Pustoc02DbContext> opt) : base(opt) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<BlogTag> BlogTags { get; set; }

	public DbSet<AppUser> AppUsers { get; set; }
}
