using Azure.Messaging;
using CpApi.DataBaseContext;
using CpApi.Interfaces;
using CpApi.Model;
using CpApi.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace CpApi.Service
{
    public class FilmGenreService : IFilmsGenresService
    {
        private readonly ContextDb _context;

        public FilmGenreService(ContextDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetAllFilmsAsync()
        {
            var filmlist = from film in _context.Films
                                  join genre in _context.Genres on film.Genre_id equals genre.id_Genre
                                  select new
                                  {
                                      film.id_Film,
                                      film.Name,
                                      film.Info,
                                      genre.NameGenre,
                                      film.DateCreate,
                                      film.Rating
                                  };

            var movies = filmlist
            .Select(f => new FilmInfo
            {
                id_Film = f.id_Film,
                Name = f.Name,
                Info = f.Info,
                NameGenre = f.NameGenre,
                DateCreate = f.DateCreate,
                Rating = f.Rating
            })
            .ToList();

            return new OkObjectResult(new
            {
                data = new { movies = movies },
                status = true
            });
        }
        public async Task<IActionResult> CreateFilmGenreAsync([FromBody] CreateFilmGenre newFilm)
        {
            try
            {
                var genrecheck = await _context.Genres.FirstOrDefaultAsync(a => a.NameGenre == newFilm.Namegenre);
                if (newFilm.Rating > 10 && newFilm.Rating < 0) return new BadRequestObjectResult(new { status = false, MessageContent = "Рейтинг должен варьироваться в диапазоне от 0 до 10" });


                if (genrecheck == null)
                {
                    var creategenre = new Genres()
                    {
                        NameGenre = newFilm.Namegenre
                    };

                    await _context.Genres.AddAsync(creategenre);
                    await _context.SaveChangesAsync();

                    var film = new Films()
                    {
                        Genre_id = creategenre.id_Genre,
                        Name = newFilm.Name,
                        Info = newFilm.Info,
                        Rating = newFilm.Rating,
                        DateCreate = newFilm.DateCreate
                    };


                    await _context.Films.AddAsync(film);
                    await _context.SaveChangesAsync(); 
                }
                else
                {
                    var film = new Films()
                    {
                        Genre_id = genrecheck.id_Genre,
                        Name = newFilm.Name,
                        Info = newFilm.Info,
                        Rating = newFilm.Rating,
                        DateCreate = newFilm.DateCreate
                    };

                    await _context.Films.AddAsync(film);
                    await _context.SaveChangesAsync();

                }
                return new OkObjectResult(new { status = true });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { status = false, ex });
            }
        }
        public async Task<IActionResult> EditFilmAsync([FromBody] FilmInfo filmInfo)
        {
            try
            {
                if (filmInfo.Rating > 10 && filmInfo.Rating < 0) return new BadRequestObjectResult(new { status = false, MessageContent = "Рейтинг должен варьироваться в диапазоне от 0 до 10" });
                
                var genrecheck = await _context.Genres.FirstOrDefaultAsync(a => a.NameGenre == filmInfo.NameGenre);
                if (genrecheck == null)
                {
                    var creategenre = new Genres()
                    {
                        NameGenre = filmInfo.NameGenre
                    };

                    await _context.Genres.AddAsync(creategenre);
                    await _context.SaveChangesAsync();

                    var film = await _context.Films.FirstOrDefaultAsync(a => a.id_Film == filmInfo.id_Film);

                    film.Genre_id = creategenre.id_Genre;
                    film.Name = filmInfo.Name;
                    film.Info = filmInfo.Info;
                    film.Rating = filmInfo.Rating;
                    film.DateCreate = filmInfo.DateCreate;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    var film = await _context.Films.FirstOrDefaultAsync(a => a.id_Film == filmInfo.id_Film);

                    film.Genre_id = genrecheck.id_Genre;
                    film.Name = filmInfo.Name;
                    film.Info = filmInfo.Info;
                    film.Rating = filmInfo.Rating;
                    film.DateCreate = filmInfo.DateCreate;

                    await _context.SaveChangesAsync();

                }
                return new OkObjectResult(new { status = true });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(new { status = false, ex });
            }
        }
        public async Task<IActionResult> GetFilmAsync([FromQuery] int Id)
        {
            var film = await _context.Films.FirstOrDefaultAsync(a => a.id_Film == Id);

            if (film != null) return new OkObjectResult(new { status = true, film });
            else return new BadRequestObjectResult(new { status = false });
        }
        public async Task<IActionResult> DeleteFilmAsync([FromQuery] int Id)
        {
            var film = await _context.Films.FirstOrDefaultAsync(a => a.id_Film == Id);

            if (film is null)
            {
                return new NotFoundObjectResult(new { status = false, MessageContent = "Фильм не найден" });
            }

            _context.Remove(film);
            await _context.SaveChangesAsync();
            return new OkObjectResult(new { status = true });
        }
    }
}
