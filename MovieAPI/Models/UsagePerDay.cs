using MongoDB.Bson.Serialization.Attributes;

namespace MovieAPI.Models
{
    public class UsagePerDay
    {
        [BsonElement("_id")]
        public string Date { get; set; }
        [BsonElement("count")]
        public int Count { get; set; }
    }
}
