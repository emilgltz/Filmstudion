using f1lmstudion.Domain.Models;
namespace f1lmstudion.Domain.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        IEnumerable<User> GetAllUsers();
        User GetById(int id);
        User Create(User user, string password);
    }
}
