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
                
        // GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> Get(string id)
        {
            var item = await _repository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }
        
        //POST /item
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Item item)
        {
            await _repository.CreateAsync(item);
            return CreatedAtAction(nameof(GetItems), new { item = item.Id }, item);
        }

        //PUT /item/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] Item updatedItem)
        {
            await _repository.UpdateAsync(id, updatedItem);
            return NoContent();
        }

        //DELETE /item/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
