using DownloadableProduct.Domain.Dto.Product;
using DownloadableProduct.Services;
using DownloadableProduct.UI.Helpers;
using DownloadableProduct.UI.Mapping;
using DownloadableProduct.UI.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        private readonly ProductService _productService;
        private readonly AdminService _adminService;
        private readonly FileHelper _fileHelper;
        public ProductController(ProductService productService,
            FileHelper fileHelper,
            AdminService adminService)
        {
            _productService = productService;
            _fileHelper = fileHelper;
            _adminService = adminService;
        }
        public IActionResult Wating()
        {
            var result = _productService.GetAllWating();
            return View(result.Data);
        }

        public IActionResult Check(int id)
        {
            var result = _productService.Get(id);
            if (result.Success)
                return View(result.Data);
            return NotFound();
        }
        public IActionResult SetSmallImage(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult SetSmallImage(SetImageViewModel model)
        {
            var uploadResult = _fileHelper.Upload(model.Image);

            if (uploadResult.Success)
            {
                var result = _productService
                    .SetSmallImage(new SetImageDto(model.Id, uploadResult.Data));

                if (result.Success)
                {
                    Swal(true, "عملیات با موفقیت انجام شد و هم اکنون عکس اصلی طرح را انتخاب کنید");
                    return RedirectToAction(nameof(SetImage), new { id = model.Id });
                }

                AddErrors(result);

                ViewBag.Id = model.Id;

                return View(model);
            }

            AddErrors(uploadResult);

            ViewBag.Id = model.Id;

            return View(model);
        }

        public IActionResult SetImage(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult SetImage(SetFileViewModel model)
        {
            var uploadResult = _fileHelper.Upload(model.File);

            if (uploadResult.Success)
            {
                var result = _productService
                    .SetImage(new SetImageDto(model.Id, uploadResult.Data));

                if (result.Success)
                {
                    Swal(true, "عملیات با موفقیت انجام شد");
                    return RedirectToAction(nameof(Wating));
                }

                AddErrors(result);

                ViewBag.Id = model.Id;

                return View(model);
            }

            AddErrors(uploadResult);

            ViewBag.Id = model.Id;

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var result = _adminService.GetProduct(id);
            return View(result.Data.ToViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Edit(ProductUpdateViewModel model)
        {
            var result = _adminService.UpdateProduct(model.ToDto());

            if (result.Success)
            {
                Swal(true, "عملیات با موفقیت انجام شد");
                return RedirectToAction(nameof(Check), new { id = model.Id });
            }

            AddErrors(result);
            return View(model);
        }

        public IActionResult Reject(int id)
        {
            var result = _adminService.GetProduct(id);
            return View(result.Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Reject(int id,string description)
        {
            var result = _adminService.Reject(id, description);
            Swal(true,"عملیات با موفقیت انجام شد");
            return RedirectToAction(nameof(Wating));
        }
    }
}