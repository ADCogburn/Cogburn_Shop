using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cogburn_Shop.Entities
{
    [BsonIgnoreExtraElements]
    public record Item
    {
        [BsonId]
        public string? Id { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
 
    }
}
