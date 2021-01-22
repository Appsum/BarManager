using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Waitress.Events;

namespace Waitress.Handlers
{
    public class ReceiptNotificationHandler : INotificationHandler<OrderPlacedMessageReceived>
    {
        public Task Handle(OrderPlacedMessageReceived notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}