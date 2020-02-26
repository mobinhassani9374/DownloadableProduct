using DownloadableProduct.DataAccess.Repositories;
using DownloadableProduct.Domain.Dto.Product;
using DownloadableProduct.Domain.Entities;
using DownloadableProduct.Domain.Enums;
using DownloadableProduct.Services.Mapping;
using DownloadableProduct.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadableProduct.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;
        private readonly UserRepository _userRepository;
        public ProductService(ProductRepository repository,
            UserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        
        
        public ServiceResult SetSmallImage(SetImageDto dto)
        {
            var result = new ServiceResult(true);

            var product = _repository.Get(dto.Id);

            if (product == null)
                result.AddError("EntityNotFoundByKey");

            product.SmallImage = dto.ImageName;

            _repository.Update(product);

            if (_repository.Save() == 0) result.AddError("Error");

            return result;
        }
        public ServiceResult SetImage(SetImageDto dto)
        {
            var result = new ServiceResult(true);

            var product = _repository.Get(dto.Id);

            if (product == null)
                result.AddError("EntityNotFoundByKey");

            product.Image = dto.ImageName;
            product.Status = ProductStatus.Confirmed;

            _repository.Update(product);

            if (_repository.Save() == 0) result.AddError("Error");

            return result;
        }
        public ServiceResult<List<ProductDto>> GetAll(string userId)
        {
            return new ServiceResult<List<ProductDto>>(true, _repository.GetAll(userId).ToDto());
        }
        public ServiceResult<List<ProductDto>> GetAllAvailable()
        {
            var products = _repository.GetAllAvailable().ToDto();

            var users = _userRepository.Get(products.Select(c => c.UserId).ToList());

            products.ForEach(c =>
            {
                c.User = users.FirstOrDefault(i => i.Id == c.UserId);
            });

            return new ServiceResult<List<ProductDto>>(true, products);
        }
        public ServiceResult<List<ProductDto>> GetAllWating()
        {
            var products = _repository.GetAllWating().ToDto();

            var users = _userRepository.Get(products.Select(c => c.UserId).ToList());

            products.ForEach(c =>
            {
                c.User = users.FirstOrDefault(i => i.Id == c.UserId);
            });

            return new ServiceResult<List<ProductDto>>(true, products);
        }
        public ServiceResult<ProductDto> Get(int id)
        {
            var result = new ServiceResult<ProductDto>(true);

            var product = _repository.Get(id);

            if (product == null)
                result.AddError("EntityNotFoundByKey");

            else result.Data = product.ToDto();

            return result;
        }
    }
}
