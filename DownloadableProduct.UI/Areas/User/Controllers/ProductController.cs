using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DownloadableProduct.DataAccess.Repositories;
using DownloadableProduct.Domain.Dto.Product;
using DownloadableProduct.Services;
using DownloadableProduct.UI.Helpers;
using DownloadableProduct.UI.Mapping;
using DownloadableProduct.UI.Models.Product;
using DownloadableProduct.Utillity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Areas.User.Controllers
{
    public class ProductController : UserBaseController
    {
        private readonly ProductService _productService;
        private readonly FileHelper _fileHelper;
        private readonly UserService _userService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly PurchaseRepository _purchaseRepository;
        public ProductController(ProductService productService,
            FileHelper fileHelper,
            UserService userService,
            IHostingEnvironment hostingEnvironment,
            PurchaseRepository purchaseRepository)
        {
            _productService = productService;
            _fileHelper = fileHelper;
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
            _purchaseRepository = purchaseRepository;
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
                Swal(true, "عملیات با موفقیت انجام شد");
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
            if (string.IsNullOrEmpty(validation.Data.UserUpoadImage))
            {
                Swal(false, "ابتدا باید عکس طرح را انتخاب کنید");
                return RedirectToAction(nameof(SetImage), new { id = id });
            }
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult SetFile(SetFileViewModel model)
        {
            var uploadResult = _fileHelper.Upload(model.File, (1024 * 1024 * 100));

            if (uploadResult.Success)
            {
                var result = _userService
                    .SetFile(new SetFileDto(model.Id, uploadResult.Data, model.File.Length));

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
        public IActionResult Detail(int id)
        {
            var producut = _userService.GetProduct(id);

            if (producut.Data == null)
                return RedirectPermanent("/");

            if (producut.Data.UserId != UserId)
                return RedirectPermanent("/");

            return View(producut.Data);
        }

        public IActionResult Download(int id)
        {
            var producut = _userService.GetProduct(id);

            if (producut.Data == null)
                return RedirectPermanent("/");

            if (producut.Data.UserId != UserId)
                return RedirectPermanent("/");

            if (string.IsNullOrEmpty(producut.Data.File))
                return RedirectPermanent("/");

            var filePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "Files", producut.Data.File);

            return PhysicalFile(filePath, "dekfoejf/fefwfw", $"{producut.Data.Title}{System.IO.Path.GetExtension(producut.Data.File)}");
        }
        public IActionResult Buy()
        {
            var data = _userService.Buy(UserId);
            return View(data.Data);
        }
        public IActionResult Sell()
        {
            var result = _userService.GetAllSell(UserId);
            return View(result.Data);
        }
        public IActionResult DownloadBuy(int id)
        {
            var purchaseSuc = _purchaseRepository.GetAllSuccess(UserId);
            if (!purchaseSuc.Any(c => c.ProductId == id))
                return RedirectToAction(nameof(Buy));
            var producut = _userService.GetProduct(id);

            var filePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "Files", producut.Data.File);

            return PhysicalFile(filePath, "dekfoejf/fefwfw", $"{producut.Data.Title}{System.IO.Path.GetExtension(producut.Data.File)}");

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