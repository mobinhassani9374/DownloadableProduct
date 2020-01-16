using DownloadableProduct.Services;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ProductService _productService;
        public HomeController(ProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            var result = _productService.GetAllAvailable();
            return View(result.Data);
        }
        public IActionResult Detail(int id)
        {
            var result = _productService.Get(id);
            return View(result.Data);
        }
    }
}