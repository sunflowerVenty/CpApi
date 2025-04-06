using CpApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CpApi.Interfaces
{
    public interface IFilmsGenresService
    {
        Task<IActionResult> GetAllMovies();
        Task<IActionResult> GetMovieById(int id);
        Task<IActionResult> CreateMovie(CreateFilm request);
        Task<IActionResult> UpdateMovie(int id, UpdateFilm request);
        Task<IActionResult> DeleteMovie(int id);
    }
}
