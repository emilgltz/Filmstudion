using AutoMapper;
using f1lmstudion.Domain.Models;
using f1lmstudion.Resources;
using f1lmstudion.Resources.Movies;
using f1lmstudion.Resources.Users;
namespace f1lmstudion.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<RegisterAdminUserModel, User>();
            CreateMap<RegisterFilmStudioModel, FilmStudio>();
            CreateMap<RegisterFilmStudioModel, User>();
            CreateMap<CreateUpdateMovieResource, Movie>();
            CreateMap<FilmCopyResource, FilmCopy>();
        }
    }
}
