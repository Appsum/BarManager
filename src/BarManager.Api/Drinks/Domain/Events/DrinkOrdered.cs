using System;

using MediatR;

namespace BarManager.Api.Drinks.Domain.Events
{
    public class DrinkOrdered : INotification
    {
        public DrinkOrdered(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }
        public string Name { get; }
    }
}