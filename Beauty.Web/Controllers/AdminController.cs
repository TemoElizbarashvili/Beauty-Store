using Beauty.DAL.UnitOfWork;
using Beauty.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Beauty.Web.Controllers
{
    public class AdminController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var list = _unitOfWork.ProductRepository.List().ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Upsert(int productId)
        {
            var product = _unitOfWork.ProductRepository.List().Where(p => p.ProductId == productId).FirstOrDefault();
            return View(product);
        }

        [HttpPost]
        public IActionResult Upsert(int productId, string name, string description, decimal price, string imgUrl)
        {
            var product = _unitOfWork.ProductRepository.List().Where(p => p.ProductId == productId).FirstOrDefault();
            if (product == null)
            {
                //if (!ModelState.IsValid)
                //{
                //    return View();
                //}
                product = new Product();
                product.Name = name;
                product.Description = description;
                product.Price = price;
                product.ImgUrl = imgUrl;
                _unitOfWork.ProductRepository.CreateProduct(product);
                return RedirectToAction("Index");
            }
            else
            {
                product.Name = name;
                product.Description = description;
                product.Price = price;
                product.ImgUrl = imgUrl;
                _unitOfWork.ProductRepository.EditProduct(product);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Delete(int productId)
        {
            await _unitOfWork.ProductRepository.DeleteProduct(productId);
            return RedirectToAction("Index");
        }
    }
}
