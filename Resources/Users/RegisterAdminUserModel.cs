using System.ComponentModel.DataAnnotations;
namespace f1lmstudion.Resources.Users
{
    public class RegisterAdminUserModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public int FilmStudioId = 0;
        public bool IsAdmin = true;
    }
}
