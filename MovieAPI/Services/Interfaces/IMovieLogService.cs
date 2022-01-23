using MovieAPI.Data.Entities;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieAPI.Services.Interfaces
{
    public interface IMovieLogService
    {
        Task AddMovieLogAsync(MovieLogModel log);
        Task<IEnumerable<MovieLog>> GetMovieLogsAsync();
        MovieLog GetByIdAsync(string id);
        Task<IEnumerable<MovieLog>> SearchByDateAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<UsagePerDay>> GetReportUsagePerDayAsync();
        Task<bool> DeleteAsync(string id);
    }
}
