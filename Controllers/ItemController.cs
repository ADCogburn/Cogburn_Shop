using Cogburn_Shop.Entities;
using Cogburn_Shop.Services;
using Cogburn_Shop.DTOs;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using ZstdSharp.Unsafe;

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
        public async Task<ActionResult<Item>> Get(Guid id)
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
        public async Task<IActionResult> Post(Item item)
        {
            item = new()
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Price = item.Price,
                Description = item.Description
            };
            
            await _repository.CreateAsync(item);
            return CreatedAtAction(nameof(GetItems), new { id = item.Id }, item);
        }


        //PUT /item/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] Item updatedItem)
        {
            var item = await _repository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(id, updatedItem);

            return NoContent();
        }


        //DELETE /item/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await _repository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
