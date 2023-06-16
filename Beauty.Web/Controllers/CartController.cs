using Beauty.DAL.UnitOfWork;
using Beauty.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Beauty.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(IUnitOfWork uow, UserManager<IdentityUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            CartViewModel cartViewModel = new CartViewModel
            {
                ShoppingCartList = _uow.ShoppingCartRepository.List().Where(u => u.UserId == user.Id).Include(p => p.Product),
                CartTotal = 0
            };
            foreach(var item in cartViewModel.ShoppingCartList)
            {
                cartViewModel.CartTotal += (double)(item.Product.Price * item.Count);
            }
            return View(cartViewModel);
        }
    }
}
