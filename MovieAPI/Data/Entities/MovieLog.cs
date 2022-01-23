using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace MovieAPI.Data.Entities
{
    public class MovieLog : IEntity<string>
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Id { get; set; }

        /// <summary>
        /// Search token used for request
        /// </summary>
        [BsonElement("search_token")]
        public string SearchToken { get; set; }

        /// <summary>
        /// ImdbID field of the fetched movie based on search token
        /// </summary>
        [BsonElement("imdbID")]
        public string ImdbID { get; set; }

        /// <summary>
        /// Time it took to process the request
        /// </summary>
        [BsonElement("processing_time_ms")]
        public long ProcessingTimeInMs { get; set; }

        /// <summary>
        /// Timestamp of the moment the request is received
        /// </summary>
        [BsonElement("timestamp")]
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// IP Address of the client performing the request
        /// </summary>
        [BsonElement("ip_address")]
        public string IpAddress { get; set; }
    }
}
