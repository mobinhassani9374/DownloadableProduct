using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DownloadableProduct.Services;
using DownloadableProduct.UI.Mapping;
using DownloadableProduct.UI.Models.Checkout;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Areas.User.Controllers
{
    public class CheckoutController : UserBaseController
    {
        private readonly UserService _userService;
        public CheckoutController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(CheckoutSearchViewModel model)
        {
            var result = _userService.GetAllChecoutForUser(model.PageNumber, model.PageSize, UserId);
            return View(result.Data);
        }

        public IActionResult Create()
        {
            var wallet = _userService.GetWallet(UserId).Data;
            ViewBag.Wallet = wallet.ToString("#,##");

            var cartBanks = _userService.GetAllCartBankSucess(UserId);
            ViewBag.CartBanks = cartBanks.Data;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Create(CheckoutCreateViewModel model)
        {
            var result = _userService.CheckoutRequest(model.ToDto(UserId));

            if (result.Success)
            {
                Swal(true, "درخواست تسویه حساب با موفقیت ثبت شد");
                return RedirectToAction(nameof(Index));
            }

            var wallet = _userService.GetWallet(UserId).Data;
            ViewBag.Wallet = wallet.ToString("#,##");

            var cartBanks = _userService.GetAllCartBankSucess(UserId);
            ViewBag.CartBanks = cartBanks.Data;

            AddErrors(result);

            return View(model);
        }
    }
}