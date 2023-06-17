using Beauty.Shared.Models;


namespace Beauty.Buisiness.Services.IServices
{
    public interface IShoppingCartService
    {
        public IQueryable<ShoppingCart> List();
        public Task<ShoppingCart> GetByIdAsync(int crt);
        public Task<ShoppingCart> GetByUserIdAsync(string userId);
        public Task CreateShoppingCart(ShoppingCart crt);
        public Task DeleteShoppingCart(int crt);
        public Task<int> IncrementCount(ShoppingCart shoppingCart, int count);
        public Task<int> DecrementCount(ShoppingCart shoppingCart, int count);
    }
}
