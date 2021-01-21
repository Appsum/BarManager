using System.Collections.Generic;

namespace Bartender.Api.Drinks.Domain
{
    public class Order
    {
        private readonly IDictionary<string, int> _orderedDrinks = new Dictionary<string, int>();

        public void OrderDrink(string name, int amount)
        {
            if (_orderedDrinks.ContainsKey(name))
            {
                _orderedDrinks[name] += amount;
            }

            _orderedDrinks.Add(name, amount);
        }

        public IReadOnlyDictionary<string, int> GetOrder() => new Dictionary<string, int>(_orderedDrinks);
    }
}