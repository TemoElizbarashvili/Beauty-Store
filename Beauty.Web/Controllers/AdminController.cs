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
        public async Task<IActionResult> Orders(string status = null)
        {
            if (status != null)
            {
                var list = _unitOfWork.OrderRepository.List().Where(o => o.Status.Equals(status)).Include(o => o.Lines).ThenInclude(c => c.Product);
                var res = await list.ToListAsync();
                return View(new OrderViewModel
                {
                    Orders = list,
                    Status = status
                });
            }
            else
            {
                var list = _unitOfWork.OrderRepository.List().Include(o => o.Lines).ThenInclude(c => c.Product);
                var res = await list.ToListAsync();
                return View(new OrderViewModel
                {
                    Orders = res,
                    Status = "All"
                });
            }
        }

        public IActionResult Filter(string status)
        {
            if (status.Equals("cancelled"))
            {
                return RedirectToAction("Orders", new { status = StatusData.StatusCancelled });
            }
            else
            {
                if (status.Equals("completed"))
                {

                    return RedirectToAction("Orders", new { status = StatusData.StatusCompleted });
                }
                else
                {
                    if (status.Equals("ready"))
                    {
                        return RedirectToAction("Orders", new { status = StatusData.StatusReady });
                    }
                    else
                    {
                        return RedirectToAction("Orders", new { status = StatusData.StatusInProcess });
                    }
                }
            }
        }

        public async Task<IActionResult> ReadyToShip(long id)
        {
            await _unitOfWork.OrderRepository.ChangeStatus(id, StatusData.StatusReady);
            return RedirectToAction("Orders");
        }

        public async Task<IActionResult> Completed(long id)
        {
            await _unitOfWork.OrderRepository.ChangeStatus(id, StatusData.StatusCompleted);
            return RedirectToAction("Orders");
        }

        public async Task<IActionResult> OrderCancel(long id)
        {
            await _unitOfWork.OrderRepository.ChangeStatus(id, StatusData.StatusCancelled);
            return RedirectToAction("Orders");
        }

        #endregion
    }
}
