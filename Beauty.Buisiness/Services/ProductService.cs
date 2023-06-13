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
    public class ProductService : IProductService
    {
        private IUnitOfWork _uow;

        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;   
        }

        public Task CreateProduct(Product prd)
        {
            _uow.ProductRepository.CreateProduct(prd);
            return Task.CompletedTask;
        }

        public Task DeleteProduct(int id)
        {
            _uow.ProductRepository.DeleteProduct(id);
            return Task.CompletedTask;
        }

        public Task EditProduct(Product prd)
        {
            _uow.ProductRepository.EditProduct(prd);
            return Task.CompletedTask;
        }

        public Task<Product> GetByIdAsync(int id)
        {
            return _uow.ProductRepository.GetByIdAsync(id);
        }

        public IQueryable<Product> List()
        {
            return _uow.ProductRepository.List();
        }
    }
}
