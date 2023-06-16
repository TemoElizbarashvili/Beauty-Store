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
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private StoreDbContext _db;

        public ShoppingCartRepository(StoreDbContext db)
        {
            _db = db;
        }

        public Task CreateShoppingCart(ShoppingCart crt)
        {
            _db.ShoppingCart.Add(crt);
            _db.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public int DecrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
            if(shoppingCart.Count == 0)
            {
                _db.ShoppingCart.Remove(shoppingCart);
            }
            _db.SaveChangesAsync();
            return shoppingCart.Count;
        }

        public Task DeleteShoppingCart(long crt)
        {
            var objToDelete = _db.ShoppingCart.Where(c => c.ShoppingCartId == crt).FirstOrDefault();
            _db.Remove(objToDelete);
            _db.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public Task<ShoppingCart> GetByIdAsync(long crt)
        {
            return Task.FromResult(_db.ShoppingCart.Where(c => c.ShoppingCartId == crt).FirstOrDefault());
        }

        public Task<ShoppingCart> GetByUserIdAsync(string userId)
        {
            return Task.FromResult(_db.ShoppingCart.Where(c => c.UserId == userId).FirstOrDefault());
        }

        public int IncrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            _db.SaveChangesAsync();
            return shoppingCart.Count;
        }

        public IQueryable<ShoppingCart> List()
        {
            return _db.ShoppingCart;
        }
    }
}
