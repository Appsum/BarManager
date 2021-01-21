using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bartender.Configuration;
using Bartender.Drinks.Domain;
using Bartender.Drinks.Domain.Repositories;

using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;

namespace Bartender.Drinks.Infrastructure.Repositories
{
    public class AzureTableStorageDrinksRepository : IDrinksRepository
    {
        private readonly CloudTable _table;
        public AzureTableStorageDrinksRepository(CloudTableClient cloudTableClient, IOptions<TableStorageSettings> tableStorageSettingsOptions)
        {
            string tableName = tableStorageSettingsOptions.Value.TableName;
            _table = cloudTableClient.GetTableReference(tableName);
        }
        public async Task Add(Drink drink) 
            => await _table.ExecuteAsync(TableOperation.Insert(drink));

        public async Task Update(Guid id, Drink drink) 
            => await _table.ExecuteAsync(TableOperation.Replace(drink));

        public async Task Delete(Guid id)
        {
            TableResult result = await _table.ExecuteAsync(TableOperation.Retrieve(Drink.PartitionKeyDefault, id.ToString("N")));
            var drinkFromStorage = result.Result as Drink;
            await _table.ExecuteAsync(TableOperation.Delete(drinkFromStorage));
        }

        public async Task<Drink> GetById(Guid id)
        {
            TableResult result = await _table.ExecuteAsync(TableOperation.Retrieve(Drink.PartitionKeyDefault, id.ToString("N")));
            return result.Result as Drink;
        }

        // The Table Storage client only supports async methods on segmented queries, so for this demo, just return it synchronous
        // more info: https://www.vivien-chevallier.com/Articles/executing-an-async-query-with-azure-table-storage-and-retrieve-all-the-results-in-a-single-operation
        public Task<IReadOnlyCollection<Drink>> GetAll()
        {
            var query = (TableQuery<Drink>) _table.CreateQuery<Drink>().Where(x => x.PartitionKey == Drink.PartitionKeyDefault);
            IEnumerable<Drink> drinks = _table.ExecuteQuery(query);
            return Task.FromResult((IReadOnlyCollection<Drink>)drinks.ToList());
        }
    }
}
