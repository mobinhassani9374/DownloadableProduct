using DownloadableProduct.Identity.DataModel;
using DownloadableProduct.Utillity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DownloadableProduct.Identity
{
    public class SignInManagerService
    {
        private readonly SignInManager<User> _signInManager;
        public SignInManagerService(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task<ServiceResult> SignIn(string userName, string password)
        {
            var result = UserLoginValidation(userName, password);

            if (result.Success)
            {
                var signInResult = await _signInManager
               .PasswordSignInAsync(userName, password, true, false);

                if (signInResult.Succeeded)
                    return ServiceResult.Okay();

                return ServiceResult.Error("UserNotFound");
            }

            return result;
        }
        public async Task Signout()
        {
            await _signInManager.SignOutAsync();
        }

        private ServiceResult UserLoginValidation(string phoneNumber, string password)
        {
            var result = new ServiceResult(true);

            if (string.IsNullOrEmpty(phoneNumber))
                result.AddError("PhoneNumberIsRequired");

            if (!string.IsNullOrEmpty(phoneNumber) && !Validation.IsPhoneNumber(phoneNumber))
                result.AddError("InCorrectPhoneNumberStructure");

            if (string.IsNullOrEmpty(password))
                result.AddError("PasswordIsRequired");

            return result;
        }
    }
}
