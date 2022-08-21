using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using f1lmstudion.Domain.Models;
using f1lmstudion.Domain.Services;
using f1lmstudion.Resources.Open;

namespace f1lmstudion.Controllers
{
    [ApiController]
    [Route("/api/v2/[controller]")]
    public class OpenController : Controller
    {
        private readonly IFilmStudioService _filmStudioService;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public OpenController(IFilmStudioService filmStudioService, IMovieService movieService, IMapper mapper)
        {
            _filmStudioService = filmStudioService;
            _movieService = movieService;
            _mapper = mapper;
        }

        [HttpGet("filmstudios")]
        public async Task<ActionResult<OpenFilmStudioResource[]>> GetAllFilmStudiosAsync()
        {
            try
            {
                var filmStudios = await _filmStudioService.GetAllFilmStudiosAsync();
                return _mapper.Map<OpenFilmStudioResource[]>(filmStudios);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("filmstudios/{filmStudioId}")]
        public async Task<ActionResult<OpenFilmStudioResource>> GetFilmStudioByIdAsync(int filmStudioId)
        {
            try
            {
                var filmStudio = await _filmStudioService.GetFilmStudioByIdAsync(filmStudioId);
                if (filmStudio == null)
                {
                    return NotFound();
                }
                return _mapper.Map<OpenFilmStudioResource>(filmStudio);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("movies")]
        public async Task<ActionResult<OpenMovieResource[]>> GetAllMoviesAsync()
        {
            try
            {
                var movies = await _movieService.GetAllMoviesAsync();
                return _mapper.Map<OpenMovieResource[]>(movies);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("movies/{movieId}")]
        public async Task<ActionResult<OpenMovieResource>> GetMovieByIdAsync(int movieId)
        {
            try
            {
                var movie = await _movieService.GetMovieByIdAsync(movieId);
                if (movie == null)
                {
                    return NotFound();
                }
                return _mapper.Map<Movie, OpenMovieResource>(movie);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
