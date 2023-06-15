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
        public Task<ShoppingCart> GetByIdAsync(int crt);
        public Task<ShoppingCart> GetByUserIdAsync(int userId);
        public Task CreateShoppingCart(ShoppingCart prd);
        public Task DeleteShoppingCart(int crt);
        public Task EditShoppingCart(ShoppingCart crt);
        public int IncrementCount(ShoppingCart shoppingCart, int count);
        public int DecrementCount(ShoppingCart shoppingCart, int count);
    }
}
