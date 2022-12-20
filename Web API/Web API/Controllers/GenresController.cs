using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_API.Dtos;
using Web_API.Models;
using Web_API.Repository;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IRepository<Genre> _genreRepository;

        public GenresController(IRepository<Genre> genreRepository)
        {
            _genreRepository = genreRepository;
            _genreRepository.seed();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _genreRepository.GetAll();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync( [FromBody] GenreDto genreDto)
        {
            var genre = new Genre { Name = genreDto.Name };
            var newGenre = await _genreRepository.Add(genre);
            return Ok(newGenre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] GenreDto genreDto)
        {
            var genre = await _genreRepository.GetById(id);

            if(genre is null)
                return NotFound($"No genre was found with ID: {id}");

            genre.Name = genreDto.Name;
            var Updatedgenre =  _genreRepository.Update(genre);

            return Ok(Updatedgenre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genre = await _genreRepository.GetById(id);

            if (genre is null)
                return NotFound($"No genre was found with ID: {id}");

            var Deleteedgenre = _genreRepository.Delete(genre);
            return Ok(Deleteedgenre);
        }

    }
}
