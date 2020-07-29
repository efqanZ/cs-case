using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CiSeCase.Core.Dtos;
using CiSeCase.Core.Dtos.Request;
using CiSeCase.Core.Enums;
using CiSeCase.Core.Interfaces.Manager;
using CiSeCase.Core.Interfaces.Repository;
using CiSeCase.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CiSeCase.Core.Services.BasketUseCases
{
    public class GetBasketHandler : IRequestHandler<GetBasketRequest, List<BasketDto>>
    {
        private readonly ILogger<GetBasketHandler> _logger;
        private readonly IBasketRepository _basketRepo;
        private readonly IUserRepository _userRepo;
        private readonly ICacheManager _cacheManager;
        private readonly IMapManager _mapManager;

        public GetBasketHandler(ILogger<GetBasketHandler> logger,
                                        IBasketRepository basketRepo,
                                        IUserRepository userRepo,
                                        ICacheManager cacheManager,
                                        IMapManager mapManager)
        {
            _logger = logger;
            _basketRepo = basketRepo;
            _userRepo = userRepo;
            _cacheManager = cacheManager;
            _mapManager = mapManager;
        }
        /// <summary>
        /// Kullanıcının sepet bilgilerini çeker. Cache'de bilgi yok ise DB'den getir
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<BasketDto>> Handle(GetBasketRequest request, CancellationToken cancellationToken)
        {
            //ToDo: Cache ile kontrol edilebilir.
            var anyUser = await _userRepo.AnyAsync(p => p.Id == request.UserId);
            if (!anyUser)
                throw new ArgumentNullException("User Id not found.");//ToDo: Bu hata alındığında, bu UserId ile kayıtlı sepet verileri kaldırılabilir.(In-Progress event ile)

            string basketCacheKey = CacheKeyTemplate.BASKET_ID_KEY_TEMPLATE.Replace("#UserId#", request.UserId.ToString());

            var basketItems = _cacheManager.Get<List<Basket>>(basketCacheKey);
            if (basketItems == null || basketItems.Count <= 0)
            {
                var result = await _basketRepo.WhereAsync(p => p.UserId == request.UserId);
                basketItems = result.ToList();
            }

            var basketDtos = basketItems.Select(s => _mapManager.Map<Basket, BasketDto>(s)).ToList();
            return basketDtos;
        }
    }
}