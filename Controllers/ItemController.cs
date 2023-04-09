using Cogburn_Shop.Entities;
using Cogburn_Shop.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;

namespace Cogburn_Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly MongoDBService _repository;

        public ItemController(MongoDBService mongoDBService)
        {
            _repository = mongoDBService;
        }
        
        //GET /item
        [HttpGet]
        public async Task<List<Item>> GetItems()
        {
            return await _repository.GetAsync();
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Item item) 
        {
            await _repository.CreateAsync(item);
            return CreatedAtAction(nameof(GetItems), new { item = item.Id }, item);
        }
        
        /*
        // GET /items/{id}
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Item>> GetOneItem(string id)
        {
            return await _repository.GetAsync(id);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> AddToItems(string name, [FromBody] string description)
        {
            await _repository.AddToItemsAsync(name, description);
            return NoContent();
        }
        */

        //DELETE /item/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
