using DownloadableProduct.Utillity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace DownloadableProduct.UI.Controllers
{
    public abstract class BaseController : Controller
    {
        Dictionary<string, string> errors = new Dictionary<string, string>();
        public BaseController()
        {
            SetErrors();
        }
        protected void AddErrors(ServiceResult serviceResult)
        {
            serviceResult.Errors.ForEach(c =>
            {
                ModelState.AddModelError("", errors.GetValueOrDefault(c.Code));
            });
        }
        protected void Swal(bool isSuccess, string message)
        {
            TempData.Clear();
            TempData.Add("serviceResult.Message", message);
            TempData.Add("serviceResult.Success", isSuccess);
        }
        protected string FullName
        {
            get
            {
                return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
            }
        }

        protected string UserId
        {
            get
            {
                return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            }
        }

        void SetErrors()
        {
            errors.Add("UserNotFound", "کاربری یافت نشد");
            errors.Add("PhoneNumberIsRequired", "شماره همراه نمی تواند فاقد مقدار باشد");
            errors.Add("InCorrectPhoneNumberStructure", "ساختار شماره همراه وارد شده صحیح نمی باشد");
            errors.Add("FullNameIsRequired", "نام و نام خانوداگی نمی تواند فاقد مقدار باشد");
            errors.Add("PasswordIsRequired", "رمزعبور نمی تواند فاقد باشد");
            errors.Add("PasswordHaveSixCharacter", "رمزعبور باید حداقل دارای شش کاراکتر باشد");
            errors.Add("ConfirmPasswordIsRequired", "تکرار رمزعبور نمی تواند فاقد مقدار باشد");
            errors.Add("ThePasswordDoesNotMatchIt", "رمزعبور با تکرارش مطابقت ندارد");
            errors.Add("Error", "در انجام عملیات خطایی رخ داد مجددا تلاش کنید");
            errors.Add("TitleIsRequired", "عنوان نمی تواند فاقد مقدار باشد");
            errors.Add("DescriptionIsRequired", "توضیحات نمی تواند فاقد مقدار باشد");
            errors.Add("TitleLengthHaveNot100Character", "عنوان نباید بیش از 100 کاراکتر داشته باشد");
            errors.Add("DescriptionLengthHaveNot500Character", "توضیحات نباید بیش از 500 کارکتر داشته باشد");
            errors.Add("DimensionsLengthHaveNot300Character", "بعاد نباید بیش از 300 کارکتر داشته باشد");
            errors.Add("ExtensionLengthHaveNot100Character", "پسوند نباید بیش از 100 کارکتر داشته باشد");
            errors.Add("ImageIsNull", "عکسی را انتخاب نکرده اید");
            errors.Add("EntityNotFoundByKey", "داده ای با شناسه ارسالی یافت نشد");
            errors.Add("DuplicateUserName", "شماره همراه وارد شده تکراری است");
        }
    }
}