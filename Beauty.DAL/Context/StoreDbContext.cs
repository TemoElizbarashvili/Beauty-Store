using Beauty.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Beauty.DAL.Context
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }
       
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Feedback> Feedbacks => Set<Feedback>();
        public DbSet<ShoppingCart> ShoppingCart => Set<ShoppingCart>();
        public DbSet<Order> Orders => Set<Order>();

    }
}
