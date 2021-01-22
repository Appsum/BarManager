using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;

namespace Waitress
{
    public class TablesHub : Hub
    {
        public async Task DeliverOrder(IDictionary<string, int> order)
            // The Deliver method parameter MUST EXACTLY match the method which the client is listening on
            => await Clients.All.SendAsync("Deliver", order);
    }
}
