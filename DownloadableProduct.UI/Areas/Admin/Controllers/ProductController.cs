using DownloadableProduct.Domain.Dto.Product;
using DownloadableProduct.Services;
using DownloadableProduct.UI.Helpers;
using DownloadableProduct.UI.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {
        private readonly ProductService _productService;
        private readonly FileHelper _fileHelper;
        public ProductController(ProductService productService,
            FileHelper fileHelper)
        {
            _productService = productService;
            _fileHelper = fileHelper;
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
    }
}