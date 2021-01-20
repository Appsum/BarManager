using System;
using System.Collections.Generic;

namespace BarManager.Api.Drinks.Domain.Repositories
{
    public interface IDrinksRepository
    {
        void Add(Drink drink);
        void Update(Guid id, Drink drink);
        void Delete(Guid id);
        Drink GetById(Guid id);
        IReadOnlyCollection<Drink> GetAll();
    }
}
