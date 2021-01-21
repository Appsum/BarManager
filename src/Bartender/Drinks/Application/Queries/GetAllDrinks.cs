using System.Collections.Generic;

using Bartender.Drinks.Domain;

namespace Bartender.Drinks.Application.Queries
{
    public class GetAllDrinks : IQuery<IReadOnlyCollection<Drink>> { }
}