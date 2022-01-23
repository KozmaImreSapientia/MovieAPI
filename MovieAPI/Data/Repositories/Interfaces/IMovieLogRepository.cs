using MovieAPI.Data.Entities;
using MovieAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Data.Repositories.Interfaces
{
    public interface IMovieLogRepository : IBaseRepository<MovieLog>
    {
        Task<IEnumerable<UsagePerDay>> Report();
    }
}
