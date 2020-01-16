using System.Threading.Tasks;
using DownloadableProduct.Identity;
using DownloadableProduct.Identity.DataModel;
using DownloadableProduct.UI.Mapping;
using DownloadableProduct.UI.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DownloadableProduct.UI.Controllers
{
    [AllowAnonymous()]
    public class AccountController : BaseController
    {
        private readonly UserManagerService _userManagerService;
        private readonly SignInManagerService _signInManagerService;
        public AccountController(UserManagerService userManagerService,
            SignInManagerService signInManagerService)
        {
            _userManagerService = userManagerService;
            _signInManagerService = signInManagerService;
        }
        [Route("/register")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectPermanent("/");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Route("/register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await _userManagerService
                  .UserRegistration(model.ToUser(), model.Password, model.ConfirmPassword);

            if (result.Success)
            {
                await _signInManagerService.SignIn(model.PhoneNumber, model.Password);
                return RedirectPermanent("/");
            }

            AddErrors(result);

            return View(model);
        }

        [Route("/login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectPermanent("/");

            return View();
        }

        [Route("/login")]
        [ValidateAntiForgeryToken()]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _signInManagerService
                .SignIn(model.PhoneNumber, model.Password);

            if (result.Success)
                return RedirectPermanent("/");

            AddErrors(result);

            return View(model);
        }

        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
                await _signInManagerService.Signout();
            return RedirectPermanent("/");
        }
    }
}