using DownloadableProduct.DataAccess.Repositories;
using DownloadableProduct.Domain.Dto.Checkout;
using DownloadableProduct.Domain.Dto.User;
using DownloadableProduct.Domain.Enums;
using DownloadableProduct.Services.Mapping;
using DownloadableProduct.Utillity;
using System;

namespace DownloadableProduct.Services
{
    public class UserService
    {
        private readonly PurchaseRepository _purchaseRepository;
        private readonly CheckoutRepository _checkoutRepository;
        private readonly UserRepository _userRepository;
        public UserService(PurchaseRepository purchaseRepository, CheckoutRepository checkoutRepository,
            UserRepository userRepository)
        {
            _purchaseRepository = purchaseRepository;
            _checkoutRepository = checkoutRepository;
            _userRepository = userRepository;
        }
        public ServiceResult<int> PurchaseRequest(ParchaseRequestDto dto)
        {
            var result = new ServiceResult<int>(true);

            var purchase = dto.ToEntity();

            purchase.CreateDate = DateTime.Now;
            purchase.IsSuccess = false;

            _purchaseRepository.Insert(purchase);

            if (_purchaseRepository.Save() > 0) result.Data = purchase.Id;

            else result.AddError("Error");

            return result;
        }
        public ServiceResult CheckoutRequest(CheckoutRequestDto dto)
        {
            var entity = dto.ToEntity();

            entity.CreateDate = DateTime.Now;
            entity.Status = CheckoutStatus.Wating;

            _checkoutRepository.Insert(entity);

            _checkoutRepository.Save();

            return ServiceResult.Okay();
        }
        public ServiceResult SuccessPayment(int id)
        {
            var result = new ServiceResult(true);

            var purchase = _purchaseRepository.GetWithDependency(id);

            if (purchase == null)
                result.AddError("Error");
            else
            {
                var user = _userRepository.GetEntity(purchase.UserId);

                purchase.IsSuccess = true;
                purchase.PaymentDate = DateTime.Now;

                // محاسبه سود کاربر
                var profit = (purchase.Product.Price * 70) / 100;

                user.Wallet += profit;

                _userRepository.Update(user);

                _purchaseRepository.Update(purchase);

                _purchaseRepository.Save();
            }
            return result;
        }
        public ServiceResult ErrorPayment(int id)
        {
            var result = new ServiceResult(true);

            var purchase = _purchaseRepository.Get(id);

            if (purchase == null)
                result.AddError("Error");
            else
            {
                purchase.IsSuccess = false;
                purchase.PaymentDate = DateTime.Now;

                _purchaseRepository.Update(purchase);

                _purchaseRepository.Save();
            }
            return result;
        }
        public ServiceResult<long> GetWallet(string userId)
        {
            return new ServiceResult<long>(true, _userRepository.GetWallet(userId));
        }
    }
}
