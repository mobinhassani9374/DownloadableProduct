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
        public IActionResult Index()
        {
            var result = _userService.GetAllCartBank(UserId);
            return View(result.Data);
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
            else
            {
                var result = _userService.CreateCartBank(new Domain.Dto.CartBank.CartBankCreateDto
                {
                    CartNumber = cart,
                    UserId = UserId
                });

                if (result.Success)
                {
                    Swal(true, "کارت بانکی با موفقیت ثبت شد");
                    return RedirectToAction(nameof(Index));
                }
                AddErrors(result);
            }

            return View(model);
        }
    }
}