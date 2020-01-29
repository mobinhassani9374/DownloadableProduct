using DownloadableProduct.DataAccess.Repositories;
using DownloadableProduct.Domain.Dto.Checkout;
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
    }
}
