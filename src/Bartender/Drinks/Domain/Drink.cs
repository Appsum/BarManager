using System;

namespace Bartender.Drinks.Domain
{
    public class Drink
    {
        public Drink(string name) : this(Guid.NewGuid(), name) { }

        public Drink(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; private set; }

        public void Rename(string newName)
        {
            Name = newName;
        }
    }
}