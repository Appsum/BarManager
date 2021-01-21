using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bartender.Drinks.Domain;
using Bartender.Drinks.Domain.Repositories;

namespace Bartender.Drinks.Infrastructure.Repositories
{
    public class InMemoryDrinksRepository : IDrinksRepository
    {
        private readonly List<Drink> _drinks = new();

        public Task Add(Drink drink)
        {
            _drinks.Add(drink);
            return Task.FromResult(drink);
        }

        public Task Update(Guid id, Drink drink)
        {
            Drink drinkToUpdate = _drinks.FirstOrDefault(x => x.Id == id);
            if (drinkToUpdate != null)
            {
                // ReSharper disable once RedundantAssignment
                drinkToUpdate = drink;
            }

            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            Drink drinkToRemove = _drinks.FirstOrDefault(x => x.Id == id);
            if (drinkToRemove != null)
            {
                _drinks.Remove(drinkToRemove);
            }

            return Task.CompletedTask;
        }

        public Task<Drink> GetById(Guid id) => Task.FromResult(_drinks.FirstOrDefault(x => x.Id == id));

        public Task<IReadOnlyCollection<Drink>> GetAll() => Task.FromResult((IReadOnlyCollection<Drink>) _drinks.ToList());
    }
}