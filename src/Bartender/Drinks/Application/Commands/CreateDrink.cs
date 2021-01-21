namespace Bartender.Drinks.Application.Commands
{
    public class CreateDrink
    {
        public CreateDrink(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}