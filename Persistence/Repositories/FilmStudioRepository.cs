using AutoMapper;
using Microsoft.EntityFrameworkCore;
using f1lmstudion.Domain.Models;
using f1lmstudion.Domain.Repositories;
using Filmstudion.Persistence;
namespace f1lmstudion.Persistence.Repositories
{
    public class FilmStudioRepository : BaseRepository, IFilmStudioRepository
    {
        public FilmStudioRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<IEnumerable<FilmStudio>> GetAllFilmStudiosAsync()
        {
            return await _context.FilmStudios.ToListAsync();
        }

        public async Task<FilmStudio> GetFilmStudioByIdAsync(int filmStudioid)
        {
            return await _context.FilmStudios.FirstOrDefaultAsync(f => f.FilmStudioId == filmStudioid);
        }

        public async Task<IEnumerable<FilmCopy>> GetRentedFilmCopiesAsync(int filmStudioid)
        {
            return await _context.FilmCopies.Where(f => f.FilmStudioId == filmStudioid).ToListAsync();
        }
    }
}
