using DownloadableProduct.DataAccess.Repositories;
using DownloadableProduct.Domain.Dto.Checkout;
using DownloadableProduct.Domain.Dto.Product;
using DownloadableProduct.Services.Mapping;
using DownloadableProduct.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadableProduct.Services
{
    public class AdminService
    {
        private readonly ProductRepository _productRepository;
        private readonly CheckoutRepository _checkoutRepository;
        private readonly UserRepository _userRepository;
        public AdminService(ProductRepository productRepository,
            CheckoutRepository checkoutRepository,
            UserRepository userRepository)
        {
            _productRepository = productRepository;
            _checkoutRepository = checkoutRepository;
            _userRepository = userRepository;
        }

        public ServiceResult<List<CheckoutDto>> GetAllWatingCheckout()
        {
            var checkouts = _checkoutRepository.GetAllWatingCheckout();
            var users = _userRepository.Get(checkouts.Select(c => c.UserId).ToList());
            return new ServiceResult<List<CheckoutDto>>(true, checkouts.ToDto(users));
        }

        public ServiceResult<ProductDto> GetProduct(int id)
        {
            var result = new ServiceResult<ProductDto>(true);

            var product = _productRepository.Get(id);

            if (product == null)
                result.AddError("EntityNotFoundByKey");

            else
            {
                var users = _userRepository.Get(new List<string>() { product.UserId });
                result.Data = product.ToDto().SetUser(users);
            }

            return result;
        }

        public ServiceResult UpdateProduct(ProductUpdateDto dto)
        {
            var result = new ServiceResult(true);

            var product = _productRepository.Get(dto.Id);

            if (product == null)
                result.AddError("ProductNotFound");

            product.Description = dto.Description;
            product.Dimensions = dto.Dimensions;
            product.Extension = dto.Extension;
            product.Price = dto.Price;
            product.Title = dto.Title;
            _productRepository.Update(product);
            _productRepository.Save();

            return result;
        }

        public ServiceResult Reject(int id, string message)
        {
            var checkout = _checkoutRepository.Get(id);
            checkout.RejectMessage = message;
            checkout.Status = Domain.Enums.CheckoutStatus.Rejected;
            _checkoutRepository.Update(checkout);
            _checkoutRepository.Save();
            return ServiceResult.Okay();
        }
        public ServiceResult CheckoutCinfirm(int id)
        {
            var result = new ServiceResult(true);

            var checkout = _checkoutRepository.Get(id);

            if (checkout == null)
                result.AddError("EntityNotFoundByKey");
            else
            {
                checkout.Status = Domain.Enums.CheckoutStatus.Confirmed;
                checkout.ResponseDate = DateTime.Now;

                var user = _userRepository.GetEntity(checkout.UserId);

                user.Wallet = user.Wallet - checkout.Price;

                _checkoutRepository.Update(checkout);
                _userRepository.Update(user);
                _checkoutRepository.Save();
            }

            return result;
        }
    }
}
