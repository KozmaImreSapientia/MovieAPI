using MovieAPI.Data.Entities;
using MovieAPI.Data.Repositories.Interfaces;
using MovieAPI.Models;
using MovieAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MovieAPI.Services
{
    public class MovieLogService : IMovieLogService
    {
        private readonly IMovieLogRepository _repository;

        public MovieLogService(IMovieLogRepository repository)
        {
            _repository = repository;
        }

        public async Task AddMovieLogAsync(MovieLogModel log)
        {
            if (log == null) { return; }

            MovieLog movieLog = new MovieLog()
            {
                ImdbID = log.ImdbID,
                IpAddress = GetLocalIPAddress(),
                ProcessingTimeInMs = log.ProcessingTimeInMs,
                SearchToken = log.SearchToken,
                TimeStamp = log.Timetamp
            };

            await _repository.InsertAsync(movieLog);
        }

        public async Task<IEnumerable<MovieLog>> GetMovieLogsAsync()
        {
            return await _repository.List();
        }

        public MovieLog GetByIdAsync(string id)
        {
            return _repository.Single(x => x.Id == id);
        }

        public async Task<IEnumerable<MovieLog>> SearchByDateAsync(DateTime startDate, DateTime endDate)
        {
            return await _repository.List(x => x.TimeStamp >= startDate.Date && x.TimeStamp <= endDate.Date);
        }

        public async Task<IEnumerable<UsagePerDay>> GetReportUsagePerDayAsync()
        {
            return await _repository.Report();
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
