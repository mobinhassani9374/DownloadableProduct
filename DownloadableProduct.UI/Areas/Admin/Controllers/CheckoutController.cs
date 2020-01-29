using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DownloadableProduct.Services;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Areas.Admin.Controllers
{
    public class CheckoutController : AdminBaseController
    {
        private readonly AdminService _adminService;
        public CheckoutController(AdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Wating()
        {
            var result = _adminService.GetAllWatingCheckout();
            return View(result.Data);
        }
    }
}