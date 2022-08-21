using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using f1lmstudion.Domain.Models;
using f1lmstudion.Domain.Services;
using f1lmstudion.Resources.Movies;

namespace f1lmstudion.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/v2/[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IFilmCopyService _filmCopyService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public MoviesController(IMovieService movieService, IFilmCopyService filmCopyService, IUserService userService, IMapper mapper)
        {
            _movieService = movieService;
            _filmCopyService = filmCopyService;
            _userService = userService;
            _mapper = mapper;
        }



        [HttpPut("{movieId}")]
        public async Task<ActionResult<MovieResource>> UpdateMovieAsync(int movieId, [FromBody] CreateUpdateMovieResource resource)
        {
            try
            {
                var userName = User.Identity.Name;
                var userId = int.Parse(userName);
                var user = _userService.GetById(userId);

                if (!user.IsAdmin) { return Unauthorized(); }

                var oldMovie = await _movieService.GetMovieByIdAsync(movieId);

                if (oldMovie == null) { return NotFound(); }

                if (oldMovie.AmountOfCopies != resource.AmountOfCopies)
                {
                    if (oldMovie.AmountOfCopies < resource.AmountOfCopies)
                    {
                        int oldCopies = oldMovie.AmountOfCopies;
                        int newCopies = resource.AmountOfCopies;
                        _filmCopyService.CreateCopies(oldCopies, newCopies, movieId);
                    }

                    else if (oldMovie.AmountOfCopies > resource.AmountOfCopies)
                    {
                        var amount = resource.AmountOfCopies;
                        var copies = await _filmCopyService.GetAllFilmCopiesByMovieIdAsync(movieId);
                        _filmCopyService.DeleteCopies(amount, copies);
                    }
                }
                var newMovie = _mapper.Map(resource, oldMovie);
                var result = _mapper.Map<Movie, MovieResource>(newMovie);
                result.AvailableFilmcopies = await _filmCopyService.GetAllFilmCopiesByMovieIdAsync(movieId);

                if (await _movieService.SaveChangesAsync()) { return Ok(result); }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest();
        }




        [HttpGet]
        public async Task<ActionResult<MovieResource[]>> GetAllMoviesAsync()
        {
            try
            {
                var movies = await _movieService.GetAllMoviesAsync();
                var moviesFilmCopies = _mapper.Map<MovieResource[]>(movies);

                foreach (var movie in moviesFilmCopies)
                {
                    var filmCopies = await _filmCopyService.GetAllFilmCopiesByMovieIdAsync(movie.MovieId);
                    var available = filmCopies.Where(f => f.IsRented == false);
                    movie.AvailablefCopies = available.Count();
                    movie.AvailableFilmcopies = available;
                }

                return moviesFilmCopies;
            }

            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MovieResource>> PostNewFilmAsync([FromBody] CreateUpdateMovieResource resource)
        {
            try
            {
                var userName = User.Identity.Name;
                var userId = int.Parse(userName);
                var user = _userService.GetById(userId);

                if (user.IsAdmin == false) { return Unauthorized(); }

                var movie = _mapper.Map<CreateUpdateMovieResource, Movie>(resource);
                _movieService.Add(movie);

                if (await _movieService.SaveChangesAsync())
                {
                    var newCopies = movie.AmountOfCopies;
                    var id = movie.MovieId;
                    _filmCopyService.CreateCopies(newCopies, id);

                    var result = _mapper.Map<Movie, MovieResource>(movie);
                    result.AvailableFilmcopies = await _filmCopyService.GetAllFilmCopiesByMovieIdAsync(id);

                    return Created("", result);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
        }
}
}
