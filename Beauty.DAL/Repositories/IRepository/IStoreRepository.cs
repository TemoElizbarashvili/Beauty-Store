using Beauty.Shared.Models;

namespace Beauty.DAL.Repositories.IRepository
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Feedback> Feedbacks { get; }
        IQueryable<ShoppingCart> ShoppingCart { get; }
        IQueryable<Order> Orders { get; }
    }
}
