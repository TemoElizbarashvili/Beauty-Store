using Beauty.Buisiness.Services.IServices;
using Beauty.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Beauty.Web.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private IProductService _produtService;
        public int PageSize = 3;

        public HomeController(IProductService produtService)
        {
           
            _produtService = produtService;
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
                }
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}