using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Cogburn_Shop.Entities
{
    [BsonIgnoreExtraElements]
    public record Item
    {
        [BsonId]
        public string? Id { get; init; }
        [BsonElement("Name")]
        public string? Name { get; init; }
        [BsonElement]
        public string? Description { get; init; }
        public decimal Price { get; init; }
 
    }
}
