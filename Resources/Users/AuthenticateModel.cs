using System.ComponentModel.DataAnnotations;
namespace f1lmstudion.Resources.Users
{
    public class AuthenticateModel
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
