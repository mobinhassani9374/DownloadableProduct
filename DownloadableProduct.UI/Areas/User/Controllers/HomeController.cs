using DownloadableProduct.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Areas.User.Controllers
{
    public class HomeController : UserBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}