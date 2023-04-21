using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

namespace ProductAPI.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
        public DbSet<ProductAPI.Models.Product>? Product { get; set; }
    }
}
