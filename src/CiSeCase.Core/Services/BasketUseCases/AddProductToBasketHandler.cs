using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using CiSeCase.Core.Dtos.Request;
using CiSeCase.Core.Events;
using CiSeCase.Core.Interfaces.Manager;
using CiSeCase.Core.Interfaces.Repository;
using CiSeCase.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CiSeCase.Core.Services.BasketUseCases
{
    public class AddProductToBasketHandler : IRequestHandler<AddProductToBasketRequest, bool>
    {
        private readonly ILogger<AddProductToBasketHandler> _logger;
        private readonly IBasketRepository _basketRepo;
        private readonly IProductRepository _productRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMediator _mediatR;

        public AddProductToBasketHandler(ILogger<AddProductToBasketHandler> logger,
                                        IBasketRepository basketRepo,
                                        IProductRepository productRepo,
                                        IUserRepository userRepo,
                                        IMediator mediatR)
        {
            _logger = logger;
            _basketRepo = basketRepo;
            _productRepo = productRepo;
            _userRepo = userRepo;
            _mediatR = mediatR;
        }

        /// <summary>
        /// Sepete ürün ekleme/çıkarma işlemi yapar. AddProductToBasketRequest isteği geldiğinde Handle edilir. 
        /// Gelen istekteki ürün sepette yoksa eklenir. Yoksa istekte gelen Quantity kontrol edilir. 0 ise ürün sepetten kaldırılır, büyük ise güncellenir.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(AddProductToBasketRequest request, CancellationToken cancellationToken)
        {
            //ToDo: Cache ile kontrol edilebilir.
            var anyUser = await _userRepo.AnyAsync(p => p.Id == request.UserId);
            if (!anyUser)
                throw new ArgumentNullException("User Id not found.");//ToDo: Bu hata alındığında, bu UserId ile kayıtlı sepet verileri kaldırılabilir.(In-Progress event ile)

            //ToDo: Cache ile kontrol edilebilir.
            var product = await _productRepo.FirstOrDefaultAsync(p => p.Id == request.ProductId);
            if (product == null)
                throw new ArgumentNullException("Product Id not found."); //ToDo: Bu hata alındığında, tüm kullanıcı sepetlerinden bu ürün kaldırılabilir.(In Progress event ile)

            if (product.StockQuantity < request.Quantity)
                throw new ValidationException("Basket quantity is higher than the stock quantity!");

            var basketItem = await _basketRepo.FirstOrDefaultAsync(p => p.UserId == request.UserId
                                                                   && p.ProductId == request.ProductId);

            if (basketItem == null)
            {
                basketItem = new Basket
                {
                    ProductId = request.ProductId,
                    UserId = request.UserId,
                    Quantity = request.Quantity
                };
                await _basketRepo.CreateAsync(basketItem);
            }
            else
            {
                if (request.Quantity <= 0)
                    await _basketRepo.DeleteAsync(basketItem);
                else
                {
                    basketItem.Quantity = request.Quantity;
                    await _basketRepo.EditAsync(basketItem);
                }
            }

            await _mediatR.Publish(new AddProductToBasketEvent(basketItem));

            return true;
        }
    }
}