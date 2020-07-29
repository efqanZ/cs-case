using CiSeCase.Core.Models;

namespace CiSeCase.Core.Events
{
    public class AddProductToBasketEvent : BaseEvent
    {
        public Basket BasketItem { get; set; }
        public AddProductToBasketEvent(Basket basketItem)
        {
            this.BasketItem = basketItem;
        }
    }
}