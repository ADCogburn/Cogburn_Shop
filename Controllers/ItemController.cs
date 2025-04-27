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
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService)
        {
            _itemService = itemService;
        }
        
        //GET /item
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await _itemService.GetItemsAsync()).Select(item => item.AsDto());
            return items;
        }


        // GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await _itemService.GetItemAsync(id);

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
            if (itemDto == null)
            {
                return BadRequest("Item data is null");
            }

            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name ?? string.Empty,
                Price = itemDto.Price,
                Description = itemDto.Description
            };
            
            await _itemService.CreateItemAsync(item);
            return CreatedAtAction(
                actionName: nameof(GetItemAsync),
                routeValues: new { id = item.Id },
                value: item.AsDto());
        }


        // PUT /item/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateItemDto itemDto)
        {
            var item = await _itemService.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            // Manually update properties
            item.Name = itemDto.Name ?? string.Empty;
            item.Price = itemDto.Price;
            item.Description = itemDto.Description;

            await _itemService.UpdateAsync(id, item);

            return NoContent();
        }



        //DELETE /item/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await _itemService.GetItemAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await _itemService.DeleteAsync(id);

            return NoContent();
        }
    }
}
