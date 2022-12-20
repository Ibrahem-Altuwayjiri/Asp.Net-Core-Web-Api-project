using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API.Dtos;
using Web_API.Models;
using Web_API.Repository;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IRepository<Movie> _moviesRepository;

        public MoviesController(IRepository<Movie> moviesRepository)
        {
            _moviesRepository = moviesRepository;
            _moviesRepository.seed();
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _moviesRepository.GetAll();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var data = await _moviesRepository.GetById(id);

            if (data == null)
                return NotFound($"No movie was found with ID {id}");

            return Ok(data);
        }

        //set action name
        //[HttpGet("GetByGenreId")] //get genreId value from body
        [HttpGet("GetByGenreId/{genreId}")] //get genreId value from url
        public async Task<IActionResult> GetByGenreIdAsync(int genreId)
        {
            var movies = await _moviesRepository.GetAll(genreId);
            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MovieDto movieDto)
        {
            if (!await _moviesRepository.IsvalidGenre(movieDto.GenreId))
                return BadRequest("Invalid genere ID!");

            Movie movie = new Movie
            {
                GenreId = movieDto.GenreId,
                Rate = movieDto.Rate,
                Title = movieDto.Title,
                Year = movieDto.Year
            };

            var newMovie = await _moviesRepository.Add(movie);

            return Ok(newMovie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] MovieDto movieDto)
        {
            var movie = await _moviesRepository.GetById(id);
            if (movie == null)
                return NotFound($"No movie was found with ID {id}");

            if (!await _moviesRepository.IsvalidGenre(movieDto.GenreId))
                return BadRequest("Invalid genere ID!");

            movie.Title = movieDto.Title;
            movie.GenreId = movieDto.GenreId;
            movie.Year = movieDto.Year;
            movie.Rate = movieDto.Rate;


            _moviesRepository.Update(movie);

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _moviesRepository.GetById(id);

            if (movie == null)
                return NotFound($"No movie was found with ID {id}");

            _moviesRepository.Delete(movie);

            return Ok(movie);
        }
    }
}
