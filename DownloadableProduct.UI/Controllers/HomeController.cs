using DownloadableProduct.Domain.Dto.User;
using DownloadableProduct.Services;
using DownloadableProduct.UI.Models.Product;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DownloadableProduct.UI.Controllers
{
    public class HomeController : BaseController
    {
        private readonly UserService _userService;
        private readonly ProductService _productService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public HomeController(ProductService productService,
            UserService userService,
            IHostingEnvironment hostingEnvironment)
        {
            _productService = productService;
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(ProductSearchViewModel model)
        {
            var result = _userService.GetAllConfirmed(model.PageNumber, model.PageSize);
            return View(result.Data);
        }
        public IActionResult Detail(int id)
        {
            var result = _userService.GetProduct(id);
            return View(result.Data);
        }
        public IActionResult PurchaseRequest(int id)
        {
            var product = _userService.GetProduct(id);

            if (!product.Success)
            {
                Swal(false, "شناسه محصول مورد نظر معتبر نمی باشد");
                return RedirectToAction(nameof(Index));
            }

            if (!User.Identity.IsAuthenticated)
                return RedirectPermanent("/auth");

            if (UserId == product.Data.UserId)
            {
                Swal(false, "طرح متعلق به شما می باشد و امکان خرید توسط خود شما نمی باشد");
                return RedirectToAction(nameof(Detail), new { id = id });
            }

            // بررسی میشود که آیا کاربر قبلا طرح را خریداری کرده است یا نه؟
            var purchaseSuc = _userService.GetAllSuccessPurchase(UserId);
            
            if(purchaseSuc.Data.Any(c => c.ProductId == product.Data.Id))
            {
                Swal(false, "شما قبلا این طرح را خریداری کرده اید و در طرح های خریداری شده توسط من در ناحیه کاربری نیز قابل مشاهده می باشد");
                return RedirectToAction(nameof(Detail), new { id = id });
            }

            var result = _userService.PurchaseRequest(new ParchaseRequestDto
            {
                ProductId = id,
                UserId = this.UserId,
                Price = product.Data.Price
            });

            if (result.Success)
            {
                return RedirectPermanent($"/payment/index/{result.Data}");
            }

            AddErrors(result);
            return RedirectToAction(nameof(Detail), new { id = id });
        }
        [Route("auth")]
        public IActionResult Auth()
        {
            return View();
        }
        public IActionResult Download(int id)
        {
            var producut = _userService.GetProduct(id);

            if (producut.Data == null)
                return RedirectPermanent("/");

            if (string.IsNullOrEmpty(producut.Data.File))
                return RedirectPermanent("/");

            if (producut.Data.Price > 0)
                return RedirectPermanent("/");

            var filePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "Files", producut.Data.File);

            return PhysicalFile(filePath, "dekfoejf/fefwfw", $"{producut.Data.Title}{System.IO.Path.GetExtension(producut.Data.File)}");
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Policy()
        {
            return View();
        }
    }
}