using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MovieAPI.Data;
using MovieAPI.Data.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MovieAPI.Services
{
    public class ConfigureMongoDbIndexesService : IHostedService
    {
        private readonly DbContext _context;
        private readonly ILogger<ConfigureMongoDbIndexesService> _logger;

        public ConfigureMongoDbIndexesService(DbContext context, ILogger<ConfigureMongoDbIndexesService> logger)
            => (_context, _logger) = (context, logger);

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var collection = _context.MongoDatabase.GetCollection<MovieLog>("MovieLogs");

            _logger.LogInformation("Creating 'At' Index on MovieLogs");
            var indexKeysDefinition = Builders<MovieLog>.IndexKeys.Ascending(x => x.TimeStamp);
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<MovieLog>(indexKeysDefinition), cancellationToken: cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
