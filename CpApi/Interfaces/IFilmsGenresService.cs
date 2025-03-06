using CpApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CpApi.Interfaces
{
    public interface IFilmsGenresService
    {
        Task<IActionResult> GetAllFilmsAsync();
        Task<IActionResult> CreateFilmGenreAsync([FromBody] CreateFilmGenre newFilm);
        Task<IActionResult> EditFilmAsync([FromBody] FilmInfo filmInfo);
        Task<IActionResult> GetFilmAsync([FromQuery] int Id);
        Task<IActionResult> DeleteFilmAsync([FromQuery] int Id);

    }
}
