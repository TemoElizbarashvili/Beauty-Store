using Beauty.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.DAL.Context
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }
       
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Feedback> Feedbacks => Set<Feedback>();

    }
}
