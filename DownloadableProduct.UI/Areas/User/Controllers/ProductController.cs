using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DownloadableProduct.Domain.Dto.Product;
using DownloadableProduct.Services;
using DownloadableProduct.UI.Helpers;
using DownloadableProduct.UI.Mapping;
using DownloadableProduct.UI.Models.Product;
using DownloadableProduct.Utillity;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Areas.User.Controllers
{
    public class ProductController : UserBaseController
    {
        private readonly ProductService _productService;
        private readonly FileHelper _fileHelper;
        private readonly UserService _userService;
        public ProductController(ProductService productService,
            FileHelper fileHelper,
            UserService userService)
        {
            _productService = productService;
            _fileHelper = fileHelper;
            _userService = userService;
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
            var result = _userService.CreateProdut(model.ToDto(this.UserId));

            if (result.Success)
            {
                return RedirectToAction(nameof(SetImage), new { id = result.Data });
            }

            AddErrors(result);

            return View(model);
        }

        public IActionResult SetImage(int id)
        {
            var validation = Validation(id);
            if (!validation.Success)
            {
                Swal(false, validation.Errors.First().Code, true);
                return RedirectPermanent("/");
            }

            ViewBag.Id = id;
            ViewBag.UserUpoadImage = validation.Data.UserUpoadImage;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult SetImage(SetImageViewModel model)
        {
            var uploadResult = _fileHelper.Upload(model.Image);

            if (uploadResult.Success)
            {
                var result = _userService
                    .SetUserUpoadImage(new SetImageDto(model.Id, uploadResult.Data));

                if (result.Success)
                {
                    Swal(true, "عکس طرح با موفقیت آپلود شد");
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
            var validation = Validation(id);
            if (!validation.Success)
            {
                Swal(false, validation.Errors.First().Code, true);
                return RedirectPermanent("/");
            }

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
                var result = _userService
                    .SetFile(new SetFileDto(model.Id, uploadResult.Data));

                if (result.Success)
                {
                    Swal(true, "فایل طرح با موفقیت آپلود شد");
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

        public IActionResult EditBasic(int id)
        {
            var validation = Validation(id);
            if (!validation.Success)
            {
                Swal(false, validation.Errors.First().Code, true);
                return RedirectPermanent("/");
            }
            return View(validation.Data.ToViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult EditBasic(ProductUpdateViewModel model)
        {
            var result = _userService.UpdateProduct(model.ToDto(), this.UserId);

            if (result.Success)
            {
                Swal(true, "عملیات با موفقیت انجام شد");
                return RedirectToAction(nameof(SetImage), new { id = model.Id });
            }

            AddErrors(result);
            return View(model);
        }
        private ServiceResult<ProductDto> Validation(int id)
        {
            var result = new ServiceResult<ProductDto>(true);
            var producut = _userService.GetProduct(id);
            if (producut.Data == null)
                result.AddError("ProductNotFound");
            else
            {
                if (producut.Data.Status == Domain.Enums.ProductStatus.Production)
                {
                    if (producut.Data.UserId != UserId)
                        result.AddError("ProductNotForUser");
                }
                else
                {
                    result.AddError("ProductNotProduction");
                }
            }
            result.Data = producut.Data;
            return result;
        }
    }
}