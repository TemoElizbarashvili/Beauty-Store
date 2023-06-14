using Beauty.DAL.Context;
using Beauty.DAL.Repositories.IRepository;
using Beauty.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.DAL.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private StoreDbContext _db;
        public StoreRepository(StoreDbContext db)
        {
            _db = db;
        }

        public IQueryable<Product> Products => _db.Products;
        public IQueryable<Feedback> Feedbacks => _db.Feedbacks;
    }
}
