using Microsoft.EntityFrameworkCore;
using WebApplication02.Models;
namespace WebApplication02.Contexts;

public class PustokDbContext : DbContext
{
    public DbSet<Slider> Sliders { get; set; }

    public PustokDbContext(DbContextOptions opt) : base(opt) { }
    
}
