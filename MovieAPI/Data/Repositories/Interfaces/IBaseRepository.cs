using MovieAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieAPI.Data.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : IEntity<string>
    {
        Task<T> InsertAsync(T instance);
        Task<IReadOnlyList<T>> List(Expression<Func<T, bool>> condition = null, Func<T, string> order = null);
        Task<T> GetByIdAsync(string id);
        T Single(Expression<Func<T, bool>> predicate = null);
        Task<bool> DeleteAsync(string id);
        int Count(Expression<Func<T, bool>> predicate = null);
    }
}
