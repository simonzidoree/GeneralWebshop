using Microsoft.EntityFrameworkCore;
using Webshop.Core.Entities;

namespace Webshop.Infrastructure.Data
{
    public class WebshopContext : DbContext
    {
        public WebshopContext(DbContextOptions<WebshopContext> opt) : base(opt)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}