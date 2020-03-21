using DownloadableProduct.DataAccess.Repositories;
using DownloadableProduct.Domain.Dto.CartBank;
using DownloadableProduct.Domain.Dto.Checkout;
using DownloadableProduct.Domain.Dto.Pagination;
using DownloadableProduct.Domain.Dto.Product;
using DownloadableProduct.Domain.Dto.Purchase;
using DownloadableProduct.Domain.Dto.User;
using DownloadableProduct.Domain.Enums;
using DownloadableProduct.Services.Mapping;
using DownloadableProduct.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DownloadableProduct.Services
{
    public class UserService
    {
        private readonly PurchaseRepository _purchaseRepository;
        private readonly CheckoutRepository _checkoutRepository;
        private readonly UserRepository _userRepository;
        private readonly ProductRepository _productRepository;
        private readonly PaymentRepository _paymentRepository;
        private readonly CartBankRepository _cartBankRepository;
        public UserService(PurchaseRepository purchaseRepository, CheckoutRepository checkoutRepository,
            UserRepository userRepository,
            ProductRepository productRepository,
            PaymentRepository paymentRepository,
            CartBankRepository cartBankRepository)
        {
            _purchaseRepository = purchaseRepository;
            _checkoutRepository = checkoutRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _paymentRepository = paymentRepository;
            _cartBankRepository = cartBankRepository;
        }
        public ServiceResult<int> PurchaseRequest(ParchaseRequestDto dto)
        {
            var result = new ServiceResult<int>(true);

            var purchase = dto.ToEntity();

            purchase.CreateDate = DateTime.Now;
            purchase.IsSuccess = false;

            var purchaseID = _purchaseRepository.InsertWithSave(purchase);

            var paymentId = _paymentRepository.InsertWithSave(new Domain.Entities.Payment
            {
                CreateData = DateTime.Now,
                Price = dto.Price,
                Type = 1,
                UserId = dto.UserId,
                ValueId = purchaseID,
                IsSuccess = false
            });

            result.Data = paymentId;

            return result;
        }
        public ServiceResult CheckoutRequest(CheckoutRequestDto dto)
        {
            var count = _checkoutRepository.CountWatingForUser(dto.UserId);

            if (count == 0)
            {
                var wallet = _userRepository.GetWallet(dto.UserId);
                if (dto.Price > wallet)
                    return ServiceResult.Error("CheckoutPriceInvalid");
                var entity = dto.ToEntity();

                entity.CreateDate = DateTime.Now;
                entity.Status = CheckoutStatus.Wating;

                _checkoutRepository.Insert(entity);

                _checkoutRepository.Save();

                return ServiceResult.Okay();
            }

            return ServiceResult.Error("NoCreateCheckout");
        }
        public ServiceResult SuccessPayment(int id)
        {
            var result = new ServiceResult(true);

            var payment = _paymentRepository.Get(id);

            if (payment == null)
                result.AddError("Error");
            else
            {
                var user = _userRepository.GetEntity(payment.UserId);
                var purchase = _purchaseRepository.Get(payment.ValueId);
                var product = _productRepository.Get(purchase.ProductId);

                payment.IsSuccess = true;
                payment.ResponseDate = DateTime.Now;

                // محاسبه سود کاربر
                var profit = (payment.Price * 70) / 100;

                user.Wallet += profit;

                //
                purchase.IsSuccess = true;

                //
                product.CountBuy++;

                _userRepository.Update(user);

                _paymentRepository.Update(payment);

                _purchaseRepository.Update(purchase);

                _productRepository.Update(product);

                _purchaseRepository.Save();
            }
            return result;
        }
        public ServiceResult ErrorPayment(int id)
        {
            var result = new ServiceResult(true);

            var payment = _paymentRepository.Get(id);

            if (payment == null)
                result.AddError("Error");
            else
            {
                payment.IsSuccess = false;
                payment.ResponseDate = DateTime.Now;

                _paymentRepository.Update(payment);

                _paymentRepository.Save();
            }
            return result;
        }
        public ServiceResult<long> GetWallet(string userId)
        {
            return new ServiceResult<long>(true, _userRepository.GetWallet(userId));
        }

        public ServiceResult<PaginationDto<ProductDto>> GetAllConfirmed(int pageNumber, int pageSize)
        {
            var data = _productRepository.GetAllConfirmed(pageNumber, pageSize);
            var users = _userRepository.Get(data.Data.Select(c => c.UserId).ToList());
            return new ServiceResult<PaginationDto<ProductDto>>(true, data.ToDto().SetUser(users));
        }

        public ServiceResult<PaginationDto<CheckoutDto>> GetAllChecoutForUser(int pageNumber, int pageSize, string userId)
        {
            var data = _checkoutRepository.GetAllForUser(pageNumber, pageSize, userId);
            return new ServiceResult<PaginationDto<CheckoutDto>>(true, data.ToDto());
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
        public ServiceResult UpdateProduct(ProductUpdateDto dto, string currentUserId)
        {
            var result = UpdateProductValidation(dto);

            var product = _productRepository.Get(dto.Id);

            if (product == null)
                result.AddError("ProductNotFound");
            else
            {
                if (currentUserId == product.UserId)
                {
                    if (product.Status == ProductStatus.Production)
                    {
                        product.Description = dto.Description;
                        product.Dimensions = dto.Dimensions;
                        product.Extension = dto.Extension;
                        product.Price = dto.Price;
                        product.Title = dto.Title;
                        _productRepository.Update(product);
                        if (_productRepository.Save() == 0)
                            result.AddError("Error");
                    }
                    else
                        result.AddError("ProductNotProduction");
                }
                else result.AddError("ProductNotForUser");
            }

            return result;
        }
        private ServiceResult UpdateProductValidation(ProductUpdateDto dto)
        {
            var result = new ServiceResult(true);

            if (string.IsNullOrEmpty(dto.Title))
                result.AddError("TitleIsRequired");

            if (string.IsNullOrEmpty(dto.Description))
                result.AddError("DescriptionIsRequired");

            if (!string.IsNullOrEmpty(dto.Title) && dto.Title.Length > 100)
                result.AddError("TitleLengthHaveNot100Character");

            if (!string.IsNullOrEmpty(dto.Description) && dto.Description.Length > 500)
                result.AddError("DescriptionLengthHaveNot500Character");

            if (!string.IsNullOrEmpty(dto.Dimensions) && dto.Dimensions.Length > 300)
                result.AddError("DimensionsLengthHaveNot300Character");

            if (!string.IsNullOrEmpty(dto.Extension) && dto.Extension.Length > 100)
                result.AddError("ExtensionLengthHaveNot100Character");

            return result;
        }

        public ServiceResult<int> CreateProdut(ProductCreateDto dto)
        {
            var result = CreateProductValidation(dto);

            var entity = dto.ToEntity();

            if (result.Success)
            {
                entity.CountView = 0;
                entity.CreateDate = DateTime.Now;
                entity.Ranking = 0;
                entity.Status = Domain.Enums.ProductStatus.Production;

                _productRepository.Insert(entity);

                if (_productRepository.Save() > 0)
                    result.Data = entity.Id;
                else
                    result.AddError("Error");
            }

            return result;
        }
        private ServiceResult<int> CreateProductValidation(ProductCreateDto product)
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

            var product = _productRepository.Get(dto.Id);

            if (product == null)
                result.AddError("EntityNotFoundByKey");

            product.UserUpoadImage = dto.ImageName;
            product.UserUpoadImageDate = DateTime.Now;

            _productRepository.Update(product);

            if (_productRepository.Save() == 0) result.AddError("Error");

            return result;
        }
        public ServiceResult SetFile(SetFileDto dto)
        {
            var result = new ServiceResult(true);

            var product = _productRepository.Get(dto.Id);

            if (product == null)
                result.AddError("EntityNotFoundByKey");

            product.File = dto.FileName;
            product.Status = ProductStatus.Wating;
            product.UploadFileDate = DateTime.Now;
            product.FileLength = dto.Length;

            _productRepository.Update(product);

            if (_productRepository.Save() == 0) result.AddError("Error");

            return result;
        }
        public ServiceResult<List<PurchaseDto>> GetAllSuccessPurchase(string userId)
        {
            var data = _purchaseRepository.GetAllSuccess(userId).ToDto();
            return new ServiceResult<List<PurchaseDto>>(true, data);
        }
        public ServiceResult<List<ProductDto>> Buy(string userId)
        {
            var purchaseSuc = _purchaseRepository.GetAllSuccess(userId);
            var purchaseSucIds = purchaseSuc.Select(c => c.ProductId).ToList();
            var data = _productRepository.GetAllByIds(purchaseSucIds);
            return new ServiceResult<List<ProductDto>>(true, data.ToDto());
        }
        public ServiceResult CreateCartBank(CartBankCreateDto createDto)
        {
            _cartBankRepository.Insert(new Domain.Entities.CartBank
            {
                CartNumber = createDto.CartNumber,
                UserId = createDto.UserId,
                Status = CartBankStatus.Wating
            });
            if (_cartBankRepository.Save() > 0)
                return ServiceResult.Okay();
            return ServiceResult.Error();

        }
        public ServiceResult<List<CartBankDto>> GetAllCartBank(string userId)
        {
            var data = _cartBankRepository.GetAll(userId);
            return new ServiceResult<List<CartBankDto>>(true, data.ToDto());
        }
        public ServiceResult Delete(int id, string currentUserId)
        {
            var cartBank = _cartBankRepository.Get(id);
            if (cartBank == null)
                return ServiceResult.Error("EntityNotFoundByKey");
            if (currentUserId == cartBank.UserId)
            {
                _cartBankRepository.Delete(cartBank);
                if (_cartBankRepository.Save() > 0)
                    return ServiceResult.Okay();
                return ServiceResult.Error();
            }
            return ServiceResult.Error("CartBankHaveNotYou");
        }
        public ServiceResult<List<CartBankDto>> GetAllCartBankSucess(string userId)
        {
            var data = _cartBankRepository.GetAllCartBankSucess(userId);
            return new ServiceResult<List<CartBankDto>>(true, data.ToDto());
        }
        public ServiceResult<int> CountProductConfirm(string userId)
        {
            var count = _productRepository.CountConfirmed(userId);
            return new ServiceResult<int>(true, count);
        }
        public ServiceResult<int> CountProduct(string userId)
        {
            var count = _productRepository.Count(userId);
            return new ServiceResult<int>(true, count);
        }
        public ServiceResult<long> GetIncome(string userId)
        {
            long sum = 0;
            var purchases = _purchaseRepository.GetAllSuccess(userId);
            purchases.ForEach(c =>
            {
                sum += (c.Product.Price * 70) / 100;
            });
            return new ServiceResult<long>(true, sum);
        }
        public ServiceResult<int> CountBuy(string userId)
        {
            var count = _purchaseRepository.CountSuccess(userId);
            return new ServiceResult<int>(true, count);
        }
    }
}
