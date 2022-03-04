using Microsoft.AspNetCore.Mvc;
using MovieAPI.Attributes;
using MovieAPI.Data.Entities;
using MovieAPI.Models;
using MovieAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [ApiKey]
    public class MovieLoggerController : ControllerBase
    {
        private readonly IMovieLogService _movieLogService;

        public MovieLoggerController(IMovieLogService movieLogService)
        {
            _movieLogService = movieLogService;
        }

        /// <summary>
        /// Get all stored request entries
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieLog>>> GetMovieLogs()
        {
            return Ok(await _movieLogService.GetMovieLogsAsync());
        }

        /// <summary>
        /// Get a single request entry
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<MovieLog> GetById(string id)
        {
            var checkForHexRegExp = new Regex("^[0-9a-fA-F]{24}$");

            if (string.IsNullOrEmpty(id) || !checkForHexRegExp.Match(id).Success) return BadRequest($"Bad format id {id} is requested!");

            var movieLogs = _movieLogService.GetByIdAsync(id);
            if(movieLogs == null) return BadRequest("There are no movies!");
            return Ok(movieLogs);
        }

        /// <summary>
        /// Search on date period
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("filter/{startDate}/{endDate}")]
        public async Task<ActionResult<MovieLog>> SearchByDate(DateTime startDate, DateTime endDate)
        {
            return Ok(await _movieLogService.SearchByDateAsync(startDate, endDate.AddDays(1)));
        }

        /// <summary>
        /// Report usage per day(DD-MM-YYYY)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [HttpGet("usagePerDay")]
        public async Task<ActionResult<IEnumerable<UsagePerDay>>> GetReportUsagePerDay()
        {
            return Ok(await _movieLogService.GetReportUsagePerDayAsync());
        }

        /// <summary>
        ///  Delete an request entry
        /// </summary>
        /// <param name="movieLog"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            return Ok(await _movieLogService.DeleteAsync(id));
        }
    }
}
