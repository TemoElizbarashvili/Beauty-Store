using Beauty.DAL.Context;
using Beauty.DAL.Repositories;
using Beauty.DAL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _db;

        public UnitOfWork(StoreDbContext db)
        {
            _db = db;
            ProductRepository = new ProductRepository(_db);
        }

        public IProductRepository ProductRepository { get; }
    }
}
