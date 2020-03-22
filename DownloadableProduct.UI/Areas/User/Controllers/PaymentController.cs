using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DownloadableProduct.Services;
using DownloadableProduct.UI.Models.Payment;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Areas.User.Controllers
{
    public class PaymentController : UserBaseController
    {
        private readonly UserService _userService;
        public PaymentController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(PaymentSearchViewModel model)
        {
            var result = _userService
                .GetAllPaymentForUser(model.PageNumber, model.PageSize, UserId);

            return View(result.Data);
        }
    }
}