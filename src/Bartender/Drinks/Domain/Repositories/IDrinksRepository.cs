using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bartender.Api.Drinks.Domain.Repositories
{
    public interface IDrinksRepository
    {
        Task Add(Drink drink);
        Task Update(Guid id, Drink drink);
        Task Delete(Guid id);
        Task<Drink> GetById(Guid id);
        Task<IReadOnlyCollection<Drink>> GetAll();
    }
}