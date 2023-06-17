using Beauty.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.DAL.Repositories.IRepository
{
    public interface IShoppingCartRepository
    {
        public IQueryable<ShoppingCart> List();
        public Task<ShoppingCart> GetByIdAsync(long crt);
        public Task<ShoppingCart> GetByUserIdAsync(string userId);
        public Task CreateShoppingCart(ShoppingCart crt);
        public Task DeleteShoppingCart(long crt);
        public Task<int> IncrementCount(ShoppingCart shoppingCart, int count);
        public Task<int> DecrementCount(ShoppingCart shoppingCart, int count);
        public Task ChangeUser(long id);
    }
}
