using Beauty.DAL.UnitOfWork;
using Beauty.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Beauty.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private UserManager<IdentityUser> _userManager;

        public AdminController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        #region Products

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

        #endregion

        #region Orders
        [HttpGet]
        public async Task<IActionResult> Orders()
        {
            var user = await _userManager.GetUserAsync(User);
            var list = _unitOfWork.OrderRepository.List().Include(c => c.Lines).ThenInclude(l => l.Product).ToList();
            return View(list);
        }

        #endregion
    }
}
