using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DownloadableProduct.Services;
using DownloadableProduct.UI.Models.Purchase;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Areas.User.Controllers
{
    public class PurchaseController : UserBaseController
    {
        private readonly UserService _userService;
        public PurchaseController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(PurchaseSearchViewModel model)
        {
            var result = _userService
                .GetAllPurchaseForUser(model.PageNumber, model.PageSize, UserId);

            return View(result.Data);
        }
    }
}