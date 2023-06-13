using Beauty.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.Buisiness.Services.IServices
{
    public interface IProductService
    {
        public IQueryable<Product> List();
        public Task<Product> GetByIdAsync(int id);
        public Task CreateProduct(Product prd);
        public Task DeleteProduct(int id);
        public Task EditProduct(Product prd);

    }
}
