using Beauty.DAL.Context;
using Beauty.DAL.Repositories.IRepository;
using Beauty.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.DAL.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private StoreDbContext _db;

        public ShoppingCartRepository(StoreDbContext db)
        {
            _db = db;
        }

        public async Task ChangeUser(long id)
        {
            var listFromDb = await _db.ShoppingCart.Where(o => o.ShoppingCartId == id).ToListAsync();
            var objFromDb = listFromDb.FirstOrDefault();
            objFromDb.UserId = "Ordered";
            _db.SaveChanges();
        }

        public Task CreateShoppingCart(ShoppingCart crt)
        {
            _db.ShoppingCart.Add(crt);
            _db.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public async Task<int> DecrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
            if(shoppingCart.Count == 0)
            {
                _db.ShoppingCart.Remove(shoppingCart);
            }
            await _db.SaveChangesAsync();
            return shoppingCart.Count;
        }
        public async Task DeleteShoppingCart(long crt)
        {
            var query = _db.ShoppingCart.Where(c => c.ShoppingCartId == crt);
            var list = await query.ToListAsync();
            var obj = list.FirstOrDefault();
            _db.Remove(obj);
            await _db.SaveChangesAsync();
        }

        public Task<ShoppingCart> GetByIdAsync(long crt)
        {
            return Task.FromResult(_db.ShoppingCart.Where(c => c.ShoppingCartId == crt).FirstOrDefault());
        }

        public Task<ShoppingCart> GetByUserIdAsync(string userId)
        {
            return Task.FromResult(_db.ShoppingCart.Where(c => c.UserId == userId).FirstOrDefault());
        }

        public async Task<int> IncrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            await _db.SaveChangesAsync();
            return shoppingCart.Count;
        }

        public IQueryable<ShoppingCart> List()
        {
            return _db.ShoppingCart;
        }
    }
}
