using System.ComponentModel.DataAnnotations;
namespace f1lmstudion.Resources.Users
{
    public class RegisterFilmStudioModel
    {
        [Required]
        public string FilmStudioName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PresidentName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int Id { get; set; }
        public bool IsAdmin = false;
    }
}
