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
        private readonly IFilmsGenresService _movieService;

        public FilmsGenresController(IFilmsGenresService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("GetAllMovies")]
        public async Task<IActionResult> GetMovies()
        {
            return await _movieService.GetAllMovies();
        }

        [HttpGet("GetMovie/{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            return await _movieService.GetMovieById(id);
        }

        [HttpPost("CreateMovie")]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> CreateMovie([FromBody] CreateFilm request)
        {
            return await _movieService.CreateMovie(request);
        }

        [HttpPut("UpdateMovie/{id}")]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] UpdateFilm request)
        {
            return await _movieService.UpdateMovie(id, request);
        }

        [HttpDelete("DeleteMovie/{id}")]
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            return await _movieService.DeleteMovie(id);
        }
    }
}
