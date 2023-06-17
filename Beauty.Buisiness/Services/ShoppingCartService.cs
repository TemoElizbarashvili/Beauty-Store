using Beauty.Buisiness.Services.IServices;
using Beauty.DAL.UnitOfWork;
using Beauty.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.Buisiness.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private IUnitOfWork _uow;

        public ShoppingCartService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Task CreateShoppingCart(ShoppingCart crt)
        {
            return _uow.ShoppingCartRepository.CreateShoppingCart(crt);
        }

        public Task<int> DecrementCount(ShoppingCart shoppingCart, int count)
        {
            return _uow.ShoppingCartRepository.DecrementCount(shoppingCart, count);
        }

        public Task DeleteShoppingCart(int crt)
        {
            return _uow.ShoppingCartRepository.DeleteShoppingCart(crt);
        }

        public Task<ShoppingCart> GetByIdAsync(int crt)
        {
            return _uow.ShoppingCartRepository.GetByIdAsync(crt);
        }

        public Task<ShoppingCart> GetByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementCount(ShoppingCart shoppingCart, int count)
        {
            return _uow.ShoppingCartRepository.IncrementCount(shoppingCart, count);
        }

        public IQueryable<ShoppingCart> List()
        {
            return _uow.ShoppingCartRepository.List();
        }
    }
}
