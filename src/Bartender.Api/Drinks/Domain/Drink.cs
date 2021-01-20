using System;

namespace Bartender.Api.Drinks.Domain
{
    public class Drink
    {
        public Drink(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}