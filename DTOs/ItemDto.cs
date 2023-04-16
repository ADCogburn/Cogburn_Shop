namespace Cogburn_Shop.DTOs
{
    public record ItemDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

    }
}
