using Cogburn_Shop.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

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
        
        public async Task<List<Item>> GetAsync()
        {
            return await _itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        /*
        public async Task<Item> GetAsync(string id)
        {
            return await _itemsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
        */

        public async Task CreateAsync(Item items)
        {
            await _itemsCollection.InsertOneAsync(items);
            return;
        }

         /*
        public async Task AddToItemsAsync(string id, string description)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("Id", id);
            UpdateDefinition<Item> updateDesc = Builders<Item>.Update.AddToSet<string>("Description", description);
            await _itemsCollection.UpdateOneAsync(filter, updateDesc);
            return;
        }
        */

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("Id", id);
            await _itemsCollection.DeleteOneAsync(filter);
            return;
        }
      }
    }

