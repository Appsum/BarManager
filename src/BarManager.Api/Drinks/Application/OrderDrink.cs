using System;

namespace BarManager.Api.Drinks.Application
{
    public class OrderDrink
    {
        public OrderDrink(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}