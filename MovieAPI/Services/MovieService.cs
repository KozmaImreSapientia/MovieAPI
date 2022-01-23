using Microsoft.Extensions.Configuration;
using MovieAPI.Models;
using MovieAPI.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MovieAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly IConfiguration _configuration;

        public MovieService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Search movies by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<OpenMovieGridModel> Search(string title)
        {
            try
            {
                OpenMovieResponseModel model = new OpenMovieResponseModel();

                using (var client = new HttpClient())
                {
                    // http://www.omdbapi.com/?t=Today&apikey=884f560b
                    client.BaseAddress = new Uri(_configuration.GetSection("OpenMovieSettings:BaseUrl").Value);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var uri = $"?t={title}&apikey={_configuration.GetSection("OpenMovieSettings:ApiKey").Value}";
                    // HTTP GET
                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        model = JsonConvert.DeserializeObject<OpenMovieResponseModel>(json);
                        if (model.Response)
                        {
                            return new OpenMovieGridModel()
                            {
                                Title = model.Title,
                                Actors = model.Actors,
                                Awards = model.Awards,
                                Country = model.Country,
                                Director = model.Director,
                                Genre = model.Genre,
                                ImdbID = model.ImdbID,
                                ImdbRating = model.ImdbRating,
                                ImdbVotes = model.ImdbVotes,
                                Language = model.Language,
                                Poster = model.Poster,
                                Plot = model.Plot,
                                Rated = model.Rated,
                                Released = model.Released,
                                Runtime = model.Runtime,
                                Writer = model.Writer,
                                Year = model.Year
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return null;
        }
    }
}
