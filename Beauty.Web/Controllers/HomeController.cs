using Beauty.Buisiness.Services.IServices;
using Beauty.Shared.Models;
using Beauty.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Beauty.Web.Controllers
{
    public class HomeController : Controller
    {
       
        private IProductService _produtService;
        private IFeedbackService _feedbackService;
        private readonly UserManager<IdentityUser> _userManager;
        public int PageSize = 3;

        public HomeController(IProductService produtService, UserManager<IdentityUser> userManager,
            IFeedbackService feedbackService)
        {

            _produtService = produtService;
            _userManager = userManager;
            _feedbackService = feedbackService;
        }

        public IActionResult Index(int productPage = 1)
        {
            var viewModel = new StoreViewModel
            {
                Products = _produtService.List().OrderBy(p => p.ProductId).Skip((productPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = _produtService.List().Count()
                },
                Feedbacks = _feedbackService.List()
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
            fb.User = user;
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
    }
}