using DownloadableProduct.DataAccess.Repositories;
using DownloadableProduct.Domain.Dto.User;
using DownloadableProduct.Services.Mapping;
using DownloadableProduct.Utillity;
using System;

namespace DownloadableProduct.Services
{
    public class UserService
    {
        private readonly PurchaseRepository _purchaseRepository;
        public UserService(PurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
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
        public ServiceResult SuccessPayment(int id)
        {
            var result = new ServiceResult(true);

            var purchase = _purchaseRepository.Get(id);

            if (purchase == null)
                result.AddError("Error");
            else
            {
                purchase.IsSuccess = true;
                purchase.PaymentDate = DateTime.Now;

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
    }
}
