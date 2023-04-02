using System;
using System.Collections.Generic;
using Cogburn_Shop.Entities;

namespace Cogburn_Shop.Repositories
{
    public interface IItemsRepo
    {
        Item GetItem(Guid id);

        IEnumerable<Item> GetItems();

        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Guid id);
    }
}