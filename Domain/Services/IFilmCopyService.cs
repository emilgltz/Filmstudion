using f1lmstudion.Domain.Models;
namespace f1lmstudion.Domain.Services
{
    public interface IFilmCopyService
    {
        void CreateCopies(int filmCopies, int movieId);
        void CreateCopies(int oldCopies, int newCopies, int movieId);
        void DeleteCopies(int amount, IEnumerable<FilmCopy> filmCopies);
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<FilmCopy>> GetAllRentedFilmCopiesAsync();
        Task<IEnumerable<FilmCopy>> GetAllFilmCopiesByMovieIdAsync(int movieId);
        Task<FilmCopy> GetAvaibleFilmCopyByMovieIdAsync(int movieId);
    }
}

