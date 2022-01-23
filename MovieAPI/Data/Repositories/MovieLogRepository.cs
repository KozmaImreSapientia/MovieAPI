using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MovieAPI.Data.Entities;
using MovieAPI.Data.Repositories.Interfaces;
using MovieAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Data.Repositories
{
    public class MovieLogRepository : BaseRepository<MovieLog>, IMovieLogRepository
    {
        public MovieLogRepository(IOptions<MongoDatabaseSettings> bookStoreDatabaseSettings, DbContext dbContext) : base(dbContext, bookStoreDatabaseSettings.Value.MovieLogsCollectionName)
        {
        }

        public async Task<IEnumerable<UsagePerDay>> Report()
        {
            var group = await _collection.Aggregate()//.Group(x =>x.TimeStamp.ToShortDateString(),g => new UsagePerDay{ Count = g.Count(), Date =g.Key})
               .Group(new BsonDocument {
                   { "_id", new BsonDocument("$dateToString", new BsonDocument{
                           { "format", "%Y-%m-%d" },
                           { "date", "$timestamp"}
                        })
                   },
                   { "count", new BsonDocument("$sum", 1) }
               })
               .As<UsagePerDay>()
               .ToListAsync();

            return group;
        }
    }
}
