using Cogburn_Shop.Entities;
using Cogburn_Shop.Services;
using Cogburn_Shop.DTOs;
using Microsoft.AspNetCore.Mvc;


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
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await _repository.GetItemsAsync()).Select(item => item.AsDto());
            return items;
        }
          
        
        // GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await _repository.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.AsDto();
        }
        

        //POST /item
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                Description = itemDto.Description
            };
            
            await _repository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id }, item.AsDto());
        }


        //PUT /item/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateItemDto itemDto)
        {
            var item = await _repository.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            Item updatedItem = item with
            {
                Name = itemDto.Name,
                Price = itemDto.Price,
                Description = itemDto.Description
            };

            await _repository.UpdateAsync(id, updatedItem);

            return NoContent();
        }


        //DELETE /item/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await _repository.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
