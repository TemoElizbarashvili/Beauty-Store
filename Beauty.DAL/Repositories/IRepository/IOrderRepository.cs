using Beauty.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.DAL.Repositories.IRepository
{
    public interface IOrderRepository
    {
        public IQueryable<Order> List();
        public Task<Order> GetByIdAsync(int id);
        public Task CreateOrder(Order order);
        public Task DeleteOrder(int id);
        public Task<IEnumerable<Order>> GetAllWithShoppingCart();
        public Task ChangeStatus(long id, string status);

    }
}
