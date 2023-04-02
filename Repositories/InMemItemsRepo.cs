using System.Collections.Generic;
using Cogburn_Shop.Entities;

namespace Cogburn_Shop.Repositories
{
    public class InMemItemsRepo : IItemsRepo
    {

        private readonly List<Item> items = new()
        {
            new Item { Id= Guid.NewGuid(), Name = "Red Potion", Description = "This potion heals you!", Price = 15 },
            new Item { Id= Guid.NewGuid(), Name = "Blue Potion", Description = "This potion gives you mana!", Price = 14 },
            new Item { Id= Guid.NewGuid(), Name = "Green Potion", Description = "This potion cures poison!", Price = 8 },
            new Item { Id= Guid.NewGuid(), Name = "Basic Sword", Description = "This is the basic sword.", Price = 20 }
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id) => items.FirstOrDefault(item => item.Id == id);

        public void CreateItem(Item item)
        {
            items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;


        }

        public void DeleteItem(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);
        }
    }
}
