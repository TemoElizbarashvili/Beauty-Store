﻿using Beauty.DAL.Context;
using Beauty.DAL.Repositories.IRepository;
using Beauty.Shared.Models;

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
        public IQueryable<ShoppingCart> ShoppingCart => _db.ShoppingCart;
        public IQueryable<Order> Orders => _db.Orders;
    }
}
