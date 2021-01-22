using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.SignalR;

using Waitress.Events;

namespace Waitress.Handlers
{
    public class TablesNotificationHandler : INotificationHandler<OrderPlacedMessageReceived>
    {
        private readonly IHubContext<TablesHub> _tablesHub;

        public TablesNotificationHandler(IHubContext<TablesHub> tablesHub)
        {
            _tablesHub = tablesHub;
        }
        public async Task Handle(OrderPlacedMessageReceived notification, CancellationToken cancellationToken)
        {
            // The Deliver method parameter MUST EXACTLY match the method which the client is listening on
            await _tablesHub.Clients.All.SendAsync("Deliver", notification.OrderedDrinks, cancellationToken);
        }
    }
}