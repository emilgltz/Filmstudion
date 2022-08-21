using AutoMapper;
using Filmstudion.Persistence;
namespace f1lmstudion.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly IMapper _mapper;
        protected readonly AppDbContext _context;
        

        public BaseRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
