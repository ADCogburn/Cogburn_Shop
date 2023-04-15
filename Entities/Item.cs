using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Cogburn_Shop.Entities
{
    [BsonIgnoreExtraElements]
    public record Item
    {
        [BsonId]
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid Id { get; init; }

        [BsonElement("Name")]
        public string? Name { get; init; }

        [BsonElement("description")]
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        public decimal Price { get; init; }
 
    }
}
