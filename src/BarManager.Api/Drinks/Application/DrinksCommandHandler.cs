using BarManager.Api.Drinks.Domain.Repositories;

namespace BarManager.Api.Drinks.Application
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