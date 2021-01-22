using System.Collections.Generic;

using Bartender.Drinks.Application.EventBus;

namespace Bartender.Drinks.Domain.Events
{
    public class OrderPlaced : IMessage
    {
        public OrderPlaced(Order order)
        {
            Order = order.GetOrder();
        }

        public IReadOnlyDictionary<string, int> Order { get; }
    }
}