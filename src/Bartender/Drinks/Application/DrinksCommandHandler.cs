using Bartender.Drinks.Domain.Repositories;

namespace Bartender.Drinks.Application
{
    public class DrinksCommandHandler
    {
        private readonly IDrinksRepository _drinksRepository;

        public DrinksCommandHandler(IDrinksRepository drinksRepository)
        {
            _drinksRepository = drinksRepository;
        }
    }
}