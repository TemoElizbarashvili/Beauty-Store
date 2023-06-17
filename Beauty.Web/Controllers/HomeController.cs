using Beauty.Buisiness.Services.IServices;
using Beauty.Shared.Models;
using Beauty.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace Beauty.Web.Controllers
{
    public class HomeController : Controller
    {
       
        private IProductService _produtService;
        private IFeedbackService _feedbackService;
        private IShoppingCartService _shoppingCartService;
        private readonly UserManager<IdentityUser> _userManager;
        public int PageSize = 3;

        public HomeController(IProductService produtService, UserManager<IdentityUser> userManager,
            IFeedbackService feedbackService, IShoppingCartService shoppingCartService)
        {

            _produtService = produtService;
            _userManager = userManager;
            _feedbackService = feedbackService;
            _shoppingCartService = 
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index(int productPage = 1)
        {
            var list = await _produtService.List().OrderBy(p => p.ProductId).Skip((productPage - 1) * PageSize).ToListAsync();
            var viewModel = new StoreViewModel
            {
                Products = list.Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = _produtService.List().Count()
                },
                Feedbacks = _feedbackService.List().ToList()
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SearchResult(string searchText = "")
        {
            var resultList = _produtService.List().Where(p => p.Name.Contains(searchText)).ToList();
            return View(resultList);
        }

        #region Feedback Creation
        [Authorize]
        [HttpGet]
        public IActionResult Feedback()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Feedback(Feedback fb)
        {
            var user = await _userManager.GetUserAsync(User);
            fb.UserId = user.Id;
            if (ModelState.IsValid)
            {
                await _feedbackService.CreateFeedback(fb);
            }
            else
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        #endregion
        #region Testionals Page
        [HttpGet]
        public IActionResult Testionals()
        {
            return View(_feedbackService.List().ToList());
        }
        [Authorize]
        public async Task<IActionResult> TestionalsOnPost()
        {
            var user = await _userManager.GetUserAsync(User);
            var list = _feedbackService.List().Where(fb => fb.UserId == user.Id).ToList();
            return View("Testionals", list);
        }
        #endregion

        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }

        #region Details & add in Shopping Cart

        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(long productId)
        {
            var user = await _userManager.GetUserAsync(User);
            ShoppingCart = new()
            {
                UserId = user.Id,
                Product = _produtService.List().Where(p => p.ProductId == productId).FirstOrDefault()
            };
            return View(ShoppingCart);
        }
        [HttpPost]
        public async Task<IActionResult> DetailsOnPost(long productId)
        {
            var k = await _produtService.List().Where(p => p.ProductId == productId).ToListAsync();
            var product = k.FirstOrDefault();
            ShoppingCart.Product = product;
            if (ModelState.IsValid)
            {
                var list = await _shoppingCartService.List().Where(c => c.UserId == ShoppingCart.UserId &&
                        c.Product.ProductId == ShoppingCart.Product.ProductId).ToListAsync();
                var cartFromDb = list.FirstOrDefault();
                if(cartFromDb == null)
                {
                    await _shoppingCartService.CreateShoppingCart(ShoppingCart);
                }
                else
                {
                    await _shoppingCartService.IncrementCount(cartFromDb, ShoppingCart.Count);
                }
                return RedirectToAction("Shop");
            }
            return View();
        }
        #endregion

        #region Shop
        public async Task<IActionResult> Shop()
        {
            var list = await _produtService.List().OrderBy(p => p.ProductId).Take(6).ToListAsync();
            return View(new ShopVIewModel
            {
                Products = list,
                IsAllShown = false
            });
        }
        public IActionResult ShopShowMore()
        {
            var list = _produtService.List().OrderBy(p => p.ProductId).ToList();
            return View("Shop", new ShopVIewModel
            {
                Products = list,
                IsAllShown = true
            });
        }


        #endregion

    }
}