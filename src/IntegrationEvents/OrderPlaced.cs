using System.Collections.Generic;
using System.Linq;

namespace IntegrationEvents
{
    public class OrderPlaced
    {
        public OrderPlaced(IEnumerable<Order> orders)
        {
            Orders = orders.ToList();
        }

        public IReadOnlyCollection<Order> Orders { get; set; }

        public class Order
        {
            public Order(string drinkName, int amount)
            {
                DrinkName = drinkName;
                Amount = amount;
            }

            public string DrinkName { get; }
            public int Amount { get; }
        }
    }
}