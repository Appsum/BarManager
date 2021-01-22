using System.Collections.Generic;

namespace Waitress.Events
{
    public class OrderPlaced
    {
        public IDictionary<string, int> Order { get; set; }
    }
}
