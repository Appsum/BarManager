using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using MediatR;

namespace Waitress.Events
{
    public class OrderPlacedMessageReceived : INotification
    {
        public OrderPlacedMessageReceived(IDictionary<string, int> orderedDrinks)
        {
            OrderedDrinks = orderedDrinks;
        }

        public IDictionary<string, int> OrderedDrinks { get; }

        public Stream AsCsvStringStreamForBlobStorage()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Name;Amount");
            foreach ((string name, int amount) in OrderedDrinks)
            {
                sb.AppendLine($"{name};{amount}");
            }

            sb.AppendLine($"TotalDrinksOrdered;{OrderedDrinks.Sum(x => x.Value)}");
            byte[] byteArray = Encoding.ASCII.GetBytes(sb.ToString());
            var stream = new MemoryStream(byteArray);
            return stream;
        }
    }
}