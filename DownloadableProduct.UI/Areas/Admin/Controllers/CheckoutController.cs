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
        public IActionResult Confirm(int id)
        {
            var result = _adminService.CheckoutCinfirm(id);

            if (result.Success)
                Swal(true, "عملیات با موفقیت انجام شد");
            else Swal(false, result.Errors.Select(c => c.Code).FirstOrDefault(), true);

            return RedirectToAction(nameof(Wating));
        }
        public IActionResult Reject(int id)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Reject(int id, string description)
        {
            var result = _adminService.Reject(id, description);
            Swal(true, "عملیات با موفقیت انجام شد");
            return RedirectToAction(nameof(Wating));
        }
    }
}