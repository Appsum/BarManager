using System.Threading.Tasks;

using Bartender.Drinks.Application.EventBus;

namespace Bartender.Drinks.Infrastructure.EventBus
{
    public class NullEventBus : IEventBus
    {
        public Task Publish(IMessage message) => Task.CompletedTask;
    }
}