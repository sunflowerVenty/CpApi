using CpApi.Interfaces;
using CpApi.Requests;
using CpApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CpApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsGenresController : ControllerBase
    {
        IFilmsGenresService _filmsGenresService;

        public FilmsGenresController(IFilmsGenresService filmsGenresService)
        {
            _filmsGenresService = filmsGenresService;
        }

        [HttpGet]
        [Route("getAllFilms")]
        public async Task<IActionResult> GetAllFilms()
        {
            return await _filmsGenresService.GetAllFilmsAsync();
        }

        [HttpPost]
        [Route("createFilm")]
        public async Task<IActionResult> CreateFilmGenre([FromBody] CreateFilmGenre newFilm)
        {
            return await _filmsGenresService.CreateFilmGenreAsync(newFilm);
        }

        [HttpPut]
        [Route("editFilm")]
        public async Task<IActionResult> EditFilm([FromBody] FilmInfo filmInfo)
        {
            return await _filmsGenresService.EditFilmAsync(filmInfo);
        }

        [HttpGet]
        [Route("getFilm/{Id}")]
        public async Task<IActionResult> GetFilm([FromQuery] int Id)
        {
            return await _filmsGenresService.GetFilmAsync(Id);
        }

        [HttpDelete]
        [Route("deleteFilm/{Id}")]
        public async Task<IActionResult> DeleteFilm(int Id)
        {
            return await _filmsGenresService.DeleteFilmAsync(Id);
        }
    }
}
