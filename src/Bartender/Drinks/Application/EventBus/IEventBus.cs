using System.Threading.Tasks;

namespace Bartender.Api.Drinks.Application.EventBus
{
    public interface IEventBus
    {
        Task Publish(IMessage message);
    }
}