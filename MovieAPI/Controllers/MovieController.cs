using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieLogService _movieService;
        private readonly IMovieService _openMovieService;

        public MovieController(IMovieLogService movieService, IMovieService openMovieService)
        {
            _movieService = movieService;
            _openMovieService = openMovieService;
        }

        [HttpGet(Name = "SearchMovie")]
        public async Task<ActionResult<OpenMovieGridModel>> Search(string title)
        {
            try
            {
                if (string.IsNullOrEmpty(title))
                {
                    return Ok("No movie title provided!");
                }

                MovieLogModel log = new MovieLogModel();
                log.Timetamp = DateTime.Now;

                Stopwatch stopwatch = Stopwatch.StartNew();
                stopwatch.Start();
                var model = await _openMovieService.Search(title);
                stopwatch.Stop();
                if (model == null)
                {
                    return Ok("Movie not found!");
                }

                log.ProcessingTimeInMs = stopwatch.ElapsedMilliseconds;
                log.SearchToken = title;
                log.ImdbID = model.ImdbID;
                await _movieService.AddMovieLogAsync(log);

                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
