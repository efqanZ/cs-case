using System.Threading;
using System.Threading.Tasks;
using CiSeCase.Core.Enums;
using CiSeCase.Core.Events;
using CiSeCase.Core.Interfaces.Manager;
using CiSeCase.Core.Interfaces.Repository;
using MediatR;

namespace CiSeCase.Core.Handlers
{
    public class AddProductToBasketEventHandler : INotificationHandler<AddProductToBasketEvent>
    {

        private readonly ICacheManager _cacheManager;
        private readonly IBasketRepository _basketRepo;
        public AddProductToBasketEventHandler(ICacheManager cacheManager,
                                            IBasketRepository basketRepo)
        {
            _cacheManager = cacheManager;
            _basketRepo = basketRepo;
        }
        public async Task Handle(AddProductToBasketEvent notification, CancellationToken cancellationToken)
        {
            var basketItems = await _basketRepo.WhereAsync(p => p.UserId == notification.BasketItem.UserId);
            string basketCacheKey = CacheKeyTemplate.BASKET_ID_KEY_TEMPLATE.Replace("#UserId#", notification.BasketItem.UserId.ToString());

            _cacheManager.Add(basketCacheKey, basketItems);
        }
    }
}