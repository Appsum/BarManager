using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bartender.Drinks.Application.Queries;
using Bartender.Drinks.Domain;
using Bartender.Drinks.Domain.Repositories;

namespace Bartender.Drinks.Application
{
    public class DrinksQueryHandler : IQueryHandler<GetDrinkById, Drink>, IQueryHandler<GetAllDrinks, IReadOnlyCollection<Drink>>
    {
        private readonly IDrinksRepository _drinksRepository;
        public DrinksQueryHandler(IDrinksRepository drinksRepository)
        {
            _drinksRepository = drinksRepository;
        }

        public async Task<Drink> Handle(GetDrinkById request, CancellationToken cancellationToken) 
            => await _drinksRepository.GetById(request.DrinkId);

        public async Task<IReadOnlyCollection<Drink>> Handle(GetAllDrinks request, CancellationToken cancellationToken) 
            => await _drinksRepository.GetAll();
    }
}