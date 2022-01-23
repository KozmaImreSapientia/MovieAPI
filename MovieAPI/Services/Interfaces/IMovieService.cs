using MovieAPI.Models;
using System.Threading.Tasks;

namespace MovieAPI.Services.Interfaces
{
    public interface IMovieService
    {
        Task<OpenMovieGridModel> Search(string title);
    }
}
