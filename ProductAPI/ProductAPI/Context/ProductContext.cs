using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

namespace ProductAPI.Context
{
    public class ProductContext : IdentityDbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
        public DbSet<ProductAPI.Models.Product>? Product { get; set; }
    }
}
