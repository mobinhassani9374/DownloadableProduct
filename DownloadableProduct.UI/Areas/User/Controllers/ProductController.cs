using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DownloadableProduct.Domain.Dto.Product;
using DownloadableProduct.Services;
using DownloadableProduct.UI.Helpers;
using DownloadableProduct.UI.Mapping;
using DownloadableProduct.UI.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Areas.User.Controllers
{
    public class ProductController : UserBaseController
    {
        private readonly ProductService _productService;
        private readonly FileHelper _fileHelper;
        public ProductController(ProductService productService,
            FileHelper fileHelper)
        {
            _productService = productService;
            _fileHelper = fileHelper;
        }

        public IActionResult Index()
        {
            var result = _productService.GetAll(this.UserId);
            return View(result.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult Create(ProductCreateViewModel model)
        {
            var result = _productService.Create(model.ToDto(this.UserId));

            if (result.Success)
            {
                return RedirectToAction(nameof(SetImage), new { id = result.Data });
            }

            AddErrors(result);

            return View(model);
        }

        public IActionResult SetImage(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult SetImage(SetImageViewModel model)
        {
            var uploadResult = _fileHelper.Upload(model.Image);

            if (uploadResult.Success)
            {
                var result = _productService
                    .SetUserUpoadImage(new SetImageDto(model.Id, uploadResult.Data));

                if (result.Success)
                {
                    return RedirectToAction(nameof(SetFile), new { id = model.Id });
                }

                AddErrors(result);

                ViewBag.Id = model.Id;

                return View(model);
            }

            AddErrors(uploadResult);

            ViewBag.Id = model.Id;

            return View(model);
        }

        public IActionResult SetFile(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult SetFile(SetFileViewModel model)
        {
            var uploadResult = _fileHelper.Upload(model.File);

            if (uploadResult.Success)
            {
                var result = _productService
                    .SetFile(new SetFileDto(model.Id, uploadResult.Data));

                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
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