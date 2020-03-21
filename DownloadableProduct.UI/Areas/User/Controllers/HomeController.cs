using DownloadableProduct.Services;
using DownloadableProduct.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Areas.User.Controllers
{
    public class HomeController : UserBaseController
    {
        private readonly UserService _userService;
        public HomeController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            var wallet = _userService.GetWallet(UserId).Data;
            var wallertStr = wallet.ToString("#,##");
            if (string.IsNullOrEmpty(wallertStr))
                wallertStr = "صفر";
            ViewBag.Wallet = wallertStr;

            ViewBag.CountProduct = _userService.CountProduct(UserId).Data.ToString();

            ViewBag.CountProductConfirm = _userService.CountProductConfirm(UserId).Data.ToString();

            ViewBag.Income = _userService.GetIncome(UserId).Data.ToString("#,##");

            ViewBag.CountBuy = _userService.CountBuy(UserId).Data.ToString();

            return View();
        }
    }
}