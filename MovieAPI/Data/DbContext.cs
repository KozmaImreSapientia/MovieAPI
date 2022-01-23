using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MovieAPI.Data
{
    public class DbContext
    {
        public IMongoDatabase MongoDatabase { get; }
        public DbContext(IOptions<MongoDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            MongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);
        }
    }
}
