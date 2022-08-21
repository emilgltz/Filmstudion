using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using f1lmstudion.Domain.Models;
using f1lmstudion.Domain.Services;
using f1lmstudion.Resources.Users;

namespace f1lmstudion.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFilmStudioService _filmStudioService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService, IFilmStudioService filmStudioService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _filmStudioService = filmStudioService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("register/admin")]
        public IActionResult RegisterAdmin([FromBody] RegisterAdminUserModel model)
        {
            var user = _mapper.Map<User>(model);



            _userService.Create(user, model.Password);
            return Ok();


        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Test);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Id = user.Id,
                Username = user.Email,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register/filmStudio")]
        public async Task<IActionResult> Register([FromBody] RegisterFilmStudioModel model)
        {
            
                var filmStudio = _mapper.Map<RegisterFilmStudioModel, FilmStudio>(model);
                var allFilmStudios = await _filmStudioService.GetAllFilmStudiosAsync();
                int id = allFilmStudios.Count() + 1;
                filmStudio.FilmStudioId = id;

                _filmStudioService.Add(filmStudio);

                var user = _mapper.Map<User>(model);
                if (await _filmStudioService.SaveChangesAsync())
                {
                    user.Email = model.Email;
                    user.FilmStudioId = filmStudio.FilmStudioId;
                    user.IsAdmin = false;
                  
                    _userService.Create(user, model.Password);
                }
                return Ok();
           
        }

      
    }
}
