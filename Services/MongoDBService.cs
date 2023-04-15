using Cogburn_Shop.Entities;
using Cogburn_Shop.DTOs;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;

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
            var items = await _itemsCollection.Find(_ => true).ToListAsync();
            return items;
        }

        public async Task<Item?> GetAsync(Guid id)
        {
             return await _itemsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
 

        public async Task CreateAsync(Item items)
        {
            await _itemsCollection.InsertOneAsync(items);
        }
        
        public async Task UpdateAsync(Guid id, Item updatedItem)
        {
            await _itemsCollection.ReplaceOneAsync(x => x.Id == id, updatedItem);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _itemsCollection.DeleteOneAsync(x => x.Id == id);
        }
      }
    }

