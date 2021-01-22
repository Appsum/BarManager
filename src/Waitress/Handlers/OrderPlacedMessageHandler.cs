using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace Waitress.Events
{
    public class OrderPlacedMessageHandler : INotificationHandler<OrderPlacedMessageReceived>
    {
        public Task Handle(OrderPlacedMessageReceived notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
