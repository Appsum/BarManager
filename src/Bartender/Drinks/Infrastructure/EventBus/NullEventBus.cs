using System.Threading.Tasks;

using Bartender.Drinks.Application.EventBus;

namespace Bartender.Drinks.Infrastructure.EventBus
{
    public class NullEventBus : IEventBus
    {
        public Task Publish<TMessage>(TMessage message) where TMessage : IMessage => Task.CompletedTask;
    }
}