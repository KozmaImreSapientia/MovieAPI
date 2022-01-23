using MongoDB.Bson;
using MongoDB.Driver;
using MovieAPI.Data.Entities;
using MovieAPI.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieAPI.Data.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : IEntity<string>
    {
        private readonly DbContext _context;
        protected IMongoCollection<T> _collection { get; set; }

        public BaseRepository(DbContext context, string collectionName)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _collection = _context.MongoDatabase.GetCollection<T>(collectionName);
        }

        public async Task<T> InsertAsync(T instance)
        {
            instance.Id = ObjectId.GenerateNewId().ToString();
            await _collection.InsertOneAsync(instance);
            return instance;
        }

        public async Task<IReadOnlyList<T>> List(Expression<Func<T, bool>> condition = null, Func<T, string> order = null)
        {
            var set = _collection.AsQueryable<T>();
            if (condition != null)
            {
                set = (MongoDB.Driver.Linq.IMongoQueryable<T>)set.Where(condition);
            }

            if (order != null)
            {
                return set.OrderBy(order).ToList();
            }

            return await set.ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(Builders<T>.Filter.Eq(x => x.Id, id)).FirstOrDefaultAsync();
        }

        public T Single(Expression<Func<T, bool>> predicate = null)
        {
            var set = _collection.AsQueryable<T>();
            var query = (predicate == null ? set : set.Where(predicate));

            return query.FirstOrDefault();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var response = await _collection.DeleteOneAsync(Builders<T>.Filter.Eq(p => p.Id, id));
            if (response.IsAcknowledged && response.DeletedCount > 0)
            {
                return true;
            }
            return false;
        }

        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            var set = _collection.AsQueryable<T>();

            return (predicate == null ? set.Count() : set.Count(predicate));
        }
    }
}
