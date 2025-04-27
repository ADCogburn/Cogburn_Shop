using Cogburn_Shop.Data;
using Cogburn_Shop.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cogburn_Shop.Services
{
    public class ItemService
    {
        private readonly AppDbContext _dbContext;

        public ItemService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _dbContext.Items.ToListAsync();
        }

        public async Task<Item?> GetItemAsync(Guid id)
        {
            return await _dbContext.Items.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task CreateItemAsync(Item item)
        {
            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(Guid id, Item updatedItem)
        {
            var existingItem = await _dbContext.Items.FindAsync(id);
            if (existingItem == null)
                return;

            existingItem.Name = updatedItem.Name;
            existingItem.Description = updatedItem.Description;
            existingItem.Price = updatedItem.Price;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await _dbContext.Items.FindAsync(id);
            if (item == null)
                return;

            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
