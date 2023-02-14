using BookStoreApi.Models.Repositories;
using BookStoreApi.Models.Validators;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        private readonly GenreModelValidator _genreModelValidator;
        public GenreController(IGenreRepository genreRepository, GenreModelValidator genreModelValidator)
        {
            _genreRepository = genreRepository;
            _genreModelValidator = genreModelValidator;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            IEnumerable<GenreModel> genres = _genreRepository.GetGenres();
            return Ok(genres);
        }
        [HttpGet("{genreId}")]
        public IActionResult GetGenreById(int genreId)
        {
            GenreModel? genreModel = _genreRepository.GetGenreById(genreId);
            try
            {
                if (genreModel != null)
                {
                    _genreModelValidator.Validate(genreModel);
                    return Ok(genreModel);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult AddGenre([FromBody] GenreModel genreModel)
        {
            try
            {
                if (_genreRepository.AddGenre(genreModel))
                {
                    _genreModelValidator.Validate(genreModel);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }
        [HttpDelete("{genreId}")]
        public IActionResult DeleteGenre(int genreId)
        {
            try
            {
                if (_genreRepository.DeleteGenreById(genreId))
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }
        [HttpPut("{genreId}")]
        public IActionResult UpdateGenreById(int genreId, [FromBody] GenreModel genreModel)
        {
            try
            {

                if (_genreRepository.UpdateGenreById(genreId, genreModel))
                {
                    _genreModelValidator.Validate(genreModel);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NotFound();
        }
    }
}
