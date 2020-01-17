using DownloadableProduct.Domain.Dto.User;
using DownloadableProduct.Services;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly UserService _userService;
        private readonly ProductService _productService;
        public HomeController(ProductService productService,
            UserService userService)
        {
            _productService = productService;
            _userService = userService;
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
        public IActionResult PurchaseRequest(int id)
        {
            var result = _userService.PurchaseRequest(new ParchaseRequestDto
            {
                ProductId = id,
                UserId = this.UserId
            });

            if (result.Success)
            {
                return RedirectPermanent($"/payment/index/{result.Data}");
            }

            AddErrors(result);
            return RedirectToAction(nameof(Detail), new { id = id });
        }
    }
}