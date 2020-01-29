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

        public IActionResult Create()
        {
            var wallet = _userService.GetWallet(UserId).Data;
            ViewBag.Wallet = wallet;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Create(CheckoutCreateViewModel model)
        {
            var result = _userService.CheckoutRequest(model.ToDto(UserId));

            if (result.Success)
                return RedirectToAction(nameof(Create));

            AddErrors(result);

            return View(model);
        }
    }
}