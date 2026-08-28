using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class ShopContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Brand> Brands { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }
    

}