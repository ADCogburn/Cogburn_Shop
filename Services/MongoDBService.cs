using Cogburn_Shop.Entities;
using Cogburn_Shop.DTOs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq;

namespace Cogburn_Shop.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Item> _itemsCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _itemsCollection = database.GetCollection<Item>(mongoDBSettings.Value.CollectionName);
        }
        
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            var items = await _itemsCollection.Find(_ => true).ToListAsync();
            return items;
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            return await _itemsCollection.Find(item => item.Id == id).FirstOrDefaultAsync();
        }
 

        public async Task CreateItemAsync(Item item)
        {
            await _itemsCollection.InsertOneAsync(item);
        }
        
        public async Task UpdateAsync(Guid id, ItemDto updatedItem)
        {
            await _itemsCollection.ReplaceOneAsync(x => x.Id == id, updatedItem);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _itemsCollection.DeleteOneAsync(x => x.Id == id);
        }
      }
    }

