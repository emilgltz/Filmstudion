using f1lmstudion.Domain.Models;
namespace f1lmstudion.Domain.Services
{
    public interface IFilmStudioService
    {
        void Add<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<FilmStudio>> GetAllFilmStudiosAsync();
        Task<FilmStudio> GetFilmStudioByIdAsync(int FilmStudioid);
        Task<IEnumerable<FilmCopy>> GetRentedFilmCopiesAsync(int FilmStudioid);
    }
}
