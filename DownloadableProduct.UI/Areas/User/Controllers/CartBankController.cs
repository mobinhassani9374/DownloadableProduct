using DownloadableProduct.Services;
using DownloadableProduct.UI.Models.CartBank;
using Microsoft.AspNetCore.Mvc;
using DNTPersianUtils.Core;

namespace DownloadableProduct.UI.Areas.User.Controllers
{
    public class CartBankController : UserBaseController
    {
        private readonly UserService _userService;
        public CartBankController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Create(CartBankCreateViewModel model)
        {
            var cart = model.Part1 + model.Part2 + model.Part3 + model.Part4;

            if (!cart.IsValidIranShetabNumber())
                ModelState.AddModelError("", "کارت بانکی معتبر نمی باشد");

            return View(model);
        }
    }
}