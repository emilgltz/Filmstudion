using f1lmstudion.Domain.Models;
using f1lmstudion.Domain.Repositories;
using f1lmstudion.Domain.Services;

namespace f1lmstudion.Services
{
    public class FilmCopyService : IFilmCopyService
    {
        private readonly IFilmCopyRepository _filmCopyRepository;

        public FilmCopyService(IFilmCopyRepository filmCopyRepository)
        {
            _filmCopyRepository = filmCopyRepository;
        }

        public void CreateCopies(int filmCopies, int movieId)
        {
            _filmCopyRepository.CreateCopies(filmCopies, movieId);
        }

        public void CreateCopies(int oldCopies, int newCopies, int movieId)
        {
            _filmCopyRepository.CreateCopies(oldCopies, newCopies, movieId);
        }

        public void DeleteCopies(int amount, IEnumerable<FilmCopy> filmCopies)
        {
            _filmCopyRepository.DeleteCopies(amount, filmCopies);
        }

        public void Add<T>(T entity) where T : class
        {
            _filmCopyRepository.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _filmCopyRepository.Delete(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _filmCopyRepository.SaveChangesAsync());
        }

        public async Task<IEnumerable<FilmCopy>> GetAllRentedFilmCopiesAsync()
        {
            return await _filmCopyRepository.GetAllRentedFilmCopiesAsync();
        }

        public async Task<IEnumerable<FilmCopy>> GetAllFilmCopiesByMovieIdAsync(int movieId)
        {
            return await _filmCopyRepository.GetAllFilmCopiesByMovieIdAsync(movieId);
        }

        public async Task<FilmCopy> GetAvaibleFilmCopyByMovieIdAsync(int movieId)
        {
            return await _filmCopyRepository.GetAvaibleFilmCopyByMovieIdAsync(movieId);
        }
    }
}
