using AutoMapper;
using f1lmstudion.Domain.Models;
using f1lmstudion.Resources;
using f1lmstudion.Resources.Movies;
using f1lmstudion.Resources.Open;
namespace f1lmstudion.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<FilmStudio, OpenFilmStudioResource>();
            CreateMap<Movie, MovieResource>();
            CreateMap<Movie, CreateUpdateMovieResource>();
            CreateMap<Movie, OpenMovieResource>();
            CreateMap<FilmCopy, FilmCopyResource>();
        }
    }
}
