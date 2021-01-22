using System.Threading.Tasks;

namespace Bartender.Drinks.Application.EventBus
{
    public interface IEventBus
    {
        Task Publish<TMessage>(TMessage message) where TMessage : IMessage;
    }
}