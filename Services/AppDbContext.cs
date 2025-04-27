using Cogburn_Shop.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cogburn_Shop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
    }
}
