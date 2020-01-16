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
        public ServiceResult<int> Create(ProductCreateDto dto)
        {
            var entity = dto.ToEntity();

            var result = CreateValidation(entity);

            if (result.Success)
            {
                entity.CountView = 0;
                entity.CreateDate = DateTime.Now;
                entity.Ranking = 0;
                entity.Status = Domain.Enums.ProductStatus.Production;

                _repository.Insert(entity);

                if (_repository.Save() > 0)
                    result.Data = entity.Id;
                else
                    result.AddError("Error");
            }

            return result;
        }
        private ServiceResult<int> CreateValidation(Product product)
        {
            var result = new ServiceResult<int>(true);

            if (string.IsNullOrEmpty(product.Title))
                result.AddError("TitleIsRequired");

            if (string.IsNullOrEmpty(product.Description))
                result.AddError("DescriptionIsRequired");

            if (!string.IsNullOrEmpty(product.Title) && product.Title.Length > 100)
                result.AddError("TitleLengthHaveNot100Character");

            if (!string.IsNullOrEmpty(product.Description) && product.Description.Length > 500)
                result.AddError("DescriptionLengthHaveNot500Character");

            if (!string.IsNullOrEmpty(product.Dimensions) && product.Dimensions.Length > 300)
                result.AddError("DimensionsLengthHaveNot300Character");

            if (!string.IsNullOrEmpty(product.Extension) && product.Extension.Length > 100)
                result.AddError("ExtensionLengthHaveNot100Character");

            return result;
        }
        public ServiceResult SetUserUpoadImage(SetImageDto dto)
        {
            var result = new ServiceResult(true);

            var product = _repository.Get(dto.Id);

            if (product == null)
                result.AddError("EntityNotFoundByKey");

            product.UserUpoadImage = dto.ImageName;

            _repository.Update(product);

            if (_repository.Save() == 0) result.AddError("Error");

            return result;
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
        public ServiceResult SetFile(SetFileDto dto)
        {
            var result = new ServiceResult(true);

            var product = _repository.Get(dto.Id);

            if (product == null)
                result.AddError("EntityNotFoundByKey");

            product.File = dto.FileName;
            product.Status = ProductStatus.Wating;

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
