using System;

namespace BarManager.Api.Drinks.Domain
{
    public class Drink
    {
        public Guid Id { get; }
        public string Name { get; }
        public Drink(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
