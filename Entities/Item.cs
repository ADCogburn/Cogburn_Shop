using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Cogburn_Shop.Entities
{
    [BsonIgnoreExtraElements]
    public record Item
    {
        [BsonId]
        public string? Id { get; init; }

        [BsonElement("Name")]
        public string? Name { get; init; }

        [BsonElement("description")]
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        public decimal Price { get; init; }
 
    }
}
