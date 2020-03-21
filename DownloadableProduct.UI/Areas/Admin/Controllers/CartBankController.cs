using DownloadableProduct.Services;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Areas.Admin.Controllers
{
    public class CartBankController : AdminBaseController
    {
        private readonly AdminService _adminService;
        public CartBankController(AdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Wating()
        {
            var result = _adminService.GetAllWatingCartBank();
            return View(result.Data);
        }
        public IActionResult Confirm(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Confirm(int id, string bankName)
        {
            var result = _adminService.CartBankConfirm(id, bankName);
            Swal(true, "عملیات با موفقیت صورت گرفت");
            return RedirectToAction(nameof(Wating));
        }
        public IActionResult Reject(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Reject(int id, string description)
        {
            var result = _adminService.CartBankReject(id, description);
            Swal(true, "عملیات با موفقیت صورت گرفت");
            return RedirectToAction(nameof(Wating));
        }
    }
}