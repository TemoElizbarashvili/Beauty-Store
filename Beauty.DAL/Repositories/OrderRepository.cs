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
    public class OrderRepository : IOrderRepository
    {
        private StoreDbContext _db;

        public OrderRepository(StoreDbContext db)
        {
            _db = db;
        }

        public async Task ChangeStatus(long id, string status)
        {
            var listFromDb = await _db.Orders.Where(o => o.OrderId == id).ToListAsync();
            var orderFromDb = listFromDb.FirstOrDefault();
            if (orderFromDb != null)
            {
                orderFromDb.Status = status;
            }
            await _db.SaveChangesAsync();
        }

        public async Task CreateOrder(Order order)
        {
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
        }

        public Task DeleteOrder(int id)
        {
            var objToDelete = _db.Orders.Where(o => o.OrderId == id).FirstOrDefault();
            _db.Remove(objToDelete);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Order>> GetAllWithShoppingCart()
        {
            var query = _db.Orders.Include(o => o.Lines).ThenInclude(c => c.Product);
            var list = await query.ToListAsync();
            return list;
        }

        public Task<Order> GetByIdAsync(int id)
        {
            return Task.FromResult(_db.Orders.Where(o => o.OrderId == id).FirstOrDefault());
        }

        public IQueryable<Order> List()
        {
            return _db.Orders;
        }
    }
}
