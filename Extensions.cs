using Cogburn_Shop.DTOs;
using Cogburn_Shop.Entities;

namespace Cogburn_Shop
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price
            };
        }
    }
}
