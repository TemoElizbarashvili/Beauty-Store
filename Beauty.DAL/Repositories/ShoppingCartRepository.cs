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

        public Task CreateShoppingCart(ShoppingCart prd)
        {
            throw new NotImplementedException();
        }

        public int DecrementCount(ShoppingCart shoppingCart, int count)
        {
            //shoppingCart.Count -=count;
            return shoppingCart.Count;
        }

        public Task DeleteShoppingCart(int crt)
        {
            throw new NotImplementedException();
        }

        public Task EditShoppingCart(ShoppingCart crt)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingCart> GetByIdAsync(int crt)
        {
            throw new NotImplementedException();
        }

        public Task<ShoppingCart> GetByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public int IncrementCount(ShoppingCart shoppingCart, int count)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ShoppingCart> List()
        {
            throw new NotImplementedException();
        }
    }
}
