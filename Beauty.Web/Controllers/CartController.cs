﻿using Beauty.DAL.UnitOfWork;
using Beauty.Shared.Models;
using Beauty.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        #region +, - , Delete

        [HttpPost]
        public async Task<IActionResult> Plus(long productId)
        {
            var userId = _userManager.GetUserId(User);
            var cartList = await _uow.ShoppingCartRepository.List().Where(c => c.UserId == userId &&
                    c.Product.ProductId == productId).ToListAsync();
            var cartFromDb = cartList.FirstOrDefault();
            await _uow.ShoppingCartRepository.IncrementCount(cartFromDb,1);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Minus(long productId)
        {
            var userId = _userManager.GetUserId(User);
            var cartList = await _uow.ShoppingCartRepository.List().Where(c => c.UserId == userId &&
                    c.Product.ProductId == productId).ToListAsync();
            var cartFromDb = cartList.FirstOrDefault();
            if (cartFromDb != null)
            {
                await _uow.ShoppingCartRepository.DecrementCount(cartFromDb, 1);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Remove(long productId)
        {
            var userId = _userManager.GetUserId(User);
            var cartList = await _uow.ShoppingCartRepository.List().Where(c => c.UserId == userId &&
                    c.Product.ProductId == productId).ToListAsync();
            var cartFromDb = cartList.FirstOrDefault();
            if (cartFromDb != null)
            {
                await _uow.ShoppingCartRepository.DeleteShoppingCart(cartFromDb.ShoppingCartId);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Ordering

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            var user = await _userManager.GetUserAsync(User);
            var lines = _uow.ShoppingCartRepository.List().Include(u => u.Product).Where(l => l.UserId == user.Id).ToList();
            double total = 0;
            foreach(var item in lines)
            {
                total += (double)(item.Count * item.Product.Price);
            }
            Order order = new Order
            {
                Lines = lines,
                UserId = user.Id,
                OrderTotal = total
            };
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Summary(Order order)
        {
            var user = await _userManager.GetUserAsync(User);
            var lines = _uow.ShoppingCartRepository.List().Include(u => u.Product).Where(l => l.UserId == user.Id);
            double total = 0;
            foreach (var item in lines)
            {
                total += (double)(item.Count * item.Product.Price);
            }
            order.Lines = lines.ToList();
            order.UserId = user.Id;
            order.OrderTotal = total;
            order.Status = StatusData.StatusInProcess;
            await _uow.OrderRepository.CreateOrder(order);
            return RedirectToAction("Confirmation");
        }

        public async Task<IActionResult> Confirmation()
        {
            var user = await _userManager.GetUserAsync(User);
            var query = _uow.ShoppingCartRepository.List().Where(c => c.UserId == user.Id);
            var list = query.ToList();
            foreach (var item in list)
            {
                await _uow.ShoppingCartRepository.ChangeUser(item.ShoppingCartId);
            }
            var order = _uow.OrderRepository.List().Where(o => o.UserId  == user.Id).FirstOrDefault();
            return View(order);
        }
        #endregion
    }
}
