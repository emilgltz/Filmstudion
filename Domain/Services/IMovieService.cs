using f1lmstudion.Domain.Models;
namespace f1lmstudion.Domain.Services
{
    public interface IMovieService
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int MovieId);
        Task<IEnumerable<FilmCopy>> GetAvailableFilmCopiesAsync(int MovieId);
    }
}
