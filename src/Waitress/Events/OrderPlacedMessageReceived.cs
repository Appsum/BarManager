using System.Collections.Generic;

using MediatR;

namespace Waitress.Events
{
    public class OrderPlacedMessageReceived : INotification
    {
        public OrderPlacedMessageReceived(IDictionary<string, int> orderedDrinks)
        {
            OrderedDrinks = orderedDrinks;
        }

        public IDictionary<string, int> OrderedDrinks { get; }
    }
}