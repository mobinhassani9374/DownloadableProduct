using DownloadableProduct.Identity.DataModel;
using DownloadableProduct.Utillity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace DownloadableProduct.Identity
{
    public class UserManagerService
    {
        private readonly UserManager<User> _userManager;
        public UserManagerService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ServiceResult> UserRegistration(User user, string password, string confirmPassword)
        {
            var result = UserRegistrationValidation(user, password, confirmPassword);

            if (result.Success)
            {
                var identityResult = await _userManager.CreateAsync(user, password);

                result.Success = identityResult.Succeeded;

                if (!result.Success)
                {
                    result.Success = false;
                    result.Errors = identityResult.Errors.Select(c => new ErrorResult
                    {
                        Code = c.Code
                    }).ToList();
                }
                else
                    await _userManager.AddToRoleAsync(user, "User");
            }

            return result;
        }
        private ServiceResult UserRegistrationValidation(User user, string password, string confirmPassword)
        {
            var result = new ServiceResult(true);

            if (string.IsNullOrEmpty(user.PhoneNumber))
                result.AddError("PhoneNumberIsRequired");

            if (!string.IsNullOrEmpty(user.PhoneNumber) && !Validation.IsPhoneNumber(user.PhoneNumber))
                result.AddError("InCorrectPhoneNumberStructure");

            if (string.IsNullOrEmpty(user.FullName))
                result.AddError("FullNameIsRequired");

            if (string.IsNullOrEmpty(password))
                result.AddError("PasswordIsRequired");

            if (!string.IsNullOrEmpty(password) && password.Length < 6)
                result.AddError("PasswordHaveSixCharacter");

            if (string.IsNullOrEmpty(confirmPassword))
                result.AddError("ConfirmPasswordIsRequired");

            if (confirmPassword != password)
                result.AddError("ThePasswordDoesNotMatchIt");

            return result;
        }
    }
}
