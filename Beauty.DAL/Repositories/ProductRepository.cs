using Beauty.DAL.Context;
using Beauty.DAL.Repositories.IRepository;
using Beauty.Shared.Models;


namespace Beauty.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private StoreDbContext _db;

        public ProductRepository(StoreDbContext db)
        {
            _db = db;
        }

        public Task CreateProduct(Product capy)
        {
            _db.Products.Add(capy);
            _db.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteProduct(int id)
        {
            var ObjToDelete = _db.Products.Where(p => p.ProductId == id).FirstOrDefault();
            _db.Products.Remove(ObjToDelete);   
            _db.SaveChanges();
            return Task.CompletedTask;
        }

        public Task EditProduct(Product prd)
        {
            var ObjToEdit = _db.Products.Where(p => p.ProductId == prd.ProductId).FirstOrDefault();

            ObjToEdit.Name = prd.Name;
            ObjToEdit.Description = prd.Description;
            ObjToEdit.Price = prd.Price;
            ObjToEdit.ImgUrl = prd.ImgUrl;
            _db.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await Task.FromResult(_db.Products.FirstOrDefault(p => p.ProductId == id));
        }

        public IQueryable<Product> List()
        {
            return _db.Products;
        }
    }
}
