﻿using DownloadableProduct.Utillity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DNTPersianUtils.Core;

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
        protected void Swal(bool isSuccess, string message, bool messageIsCode)
        {
            Swal(isSuccess,errors.GetValueOrDefault(message));
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
            errors.Add("TitleLengthHaveNot100Character", "عنوان نباید بیش از 100 کاراکتر داشته باشد".ToPersianNumbers());
            errors.Add("DescriptionLengthHaveNot500Character", "توضیحات نباید بیش از 500 کارکتر داشته باشد".ToPersianNumbers());
            errors.Add("DimensionsLengthHaveNot300Character", "بعاد نباید بیش از 300 کارکتر داشته باشد".ToPersianNumbers());
            errors.Add("ExtensionLengthHaveNot100Character", "پسوند نباید بیش از 100 کارکتر داشته باشد".ToPersianNumbers());
            errors.Add("ImageIsNull", "عکسی را انتخاب نکرده اید");
            errors.Add("EntityNotFoundByKey", "داده ای با شناسه ارسالی یافت نشد");
            errors.Add("DuplicateUserName", "شماره همراه وارد شده تکراری است");
            errors.Add("ProductNotFound", "طرحی یافت نشد");
            errors.Add("ProductNotProduction", "اجازه ویرایش طرح را ندارید");
            errors.Add("ProductNotForUser", "طرح متعلق به شما نیست");
            errors.Add("NoCreateCheckout","امکان ایجاد درخواست تسویه حساب ندارید زیرا یک تسویه حساب در حال بررسی دارید");
            errors.Add("CheckoutPriceInvalid", "مبلغ درخواست تسویه حساب بیش از موجودی شما ی باشد");
            errors.Add("CartBankHaveNotYou", "کارت بانکی متعلق به شما نمی باشد");
            errors.Add("FileIsLarger","حجم فایل آپلودی زیاد می باشد");
            errors.Add("CartNumberNotFound","شماره کارت نامعتبر می باشد");
            errors.Add("CartNUmberHaveNotUser","شماره کارت متعلق به شما نمی باشد");
            errors.Add("CartNumberIsNotConfirmed","شماره کارت هنوز تایید نشده است");
            errors.Add("Checkout5000","حداقل مبلغ برای تسویه حساب باید 5000 تومان باشد".ToPersianNumbers());
        }
    }
}