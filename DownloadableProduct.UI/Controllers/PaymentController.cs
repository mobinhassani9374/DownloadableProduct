using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DownloadableProduct.Services;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly UserService _userService;
        public PaymentController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public IActionResult Success(int id)
        {
            _userService.SuccessPayment(id);
            return RedirectPermanent("/");
        }
        public IActionResult Error(int id)
        {
            _userService.ErrorPayment(id);
            return RedirectPermanent("/");
        }
    }
}