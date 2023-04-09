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
        [HttpPut("{id}")]
        public async Task<IActionResult> AddToPlaylist(string id, [FromBody] string movieId) { }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) { }
        
        // GET /items/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = _repository.GetItem(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.AsDto();
        }
        
        //POST /items
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Description = itemDto.Description,
                Price = itemDto.Price
            };

            _repository.CreateItem(item);

            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }
        
        //PUT /item/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = _repository.GetItem(id);

            if (existingItem ==null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            _repository.UpdateItem(updatedItem);

            return NoContent();
        }

        //DELETE /item/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = _repository.GetItem(id);

            if (existingItem == null) { return NotFound(); }

            _repository.DeleteItem(id);

            return NoContent();
        }*/
    }
}
