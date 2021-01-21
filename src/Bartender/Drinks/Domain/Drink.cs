using System;

using Microsoft.Azure.Cosmos.Table;

namespace Bartender.Drinks.Domain
{
    public class Drink : TableEntity
    {
        public static string PartitionKeyDefault = nameof(Drink);
        public Drink()
        {

        }
        public Drink(string name) : this(Guid.NewGuid(), name) { }

        public Drink(Guid id, string name) : this()
        {
            RowKey = id.ToString("N");
            PartitionKey = PartitionKeyDefault;
            Name = name;
        }

        public Guid Id => Guid.Parse(RowKey);
        public string Name { get; set; }

        public void Rename(string newName)
        {
            Name = newName;
        }
    }
}