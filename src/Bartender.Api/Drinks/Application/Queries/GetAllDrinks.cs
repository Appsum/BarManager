using System.Collections.Generic;

using Bartender.Api.Drinks.Domain;

namespace Bartender.Api.Drinks.Application.Queries
{
    public class GetAllDrinks: IQuery<IReadOnlyCollection<Drink>>
    { }
}