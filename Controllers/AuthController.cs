using System.Security.Claims;
using System.Text;
using maker_checker_v1.data;
using maker_checker_v1.models.entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace maker_checker_v1.models.DTO
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;

        public AuthController(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _secret = _configuration["JWT:Secret"];
            _issuer = _configuration["JWT:Issuer"];
            _audience = _configuration["JWT:Audience"];
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserLoginDTO userModel)
        {
            if (await _userRepository.Exists(userModel.Username))
                return BadRequest("User already exists");
            //
            var userToBeCreated = new User()
            {
                Username = userModel.Username,
                Password = entities.User.CreateHash(userModel.Password),
                //todo change it later
            };
            _userRepository.Add(userToBeCreated);
            if (!await _userRepository.Save())
                return BadRequest("problem occured while saving user");
            //generating token
            var payload = new Claim[]{
                new Claim("sub",userToBeCreated.Id.ToString()),
                new Claim("username",userToBeCreated.Username),
                new Claim(ClaimTypes.Role,userToBeCreated.Role.Name)
             };

            var token = userToBeCreated.generateToken(payload, _issuer, _audience, _secret);

            var claimsIdentity = new ClaimsIdentity(
                payload, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Ok(userToBeCreated);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDTO userModel)
        {
            User? user = await _userRepository.GetByUsername(userModel.Username);
            if (user == null)
                return BadRequest("User does not exist");
            if (!entities.User.CompareHash(userModel.Password, user.Password))
                return BadRequest("Wrong password");
            //todo: create token after taking ahmed opinion on its implementation

            var payload = new Claim[]{
                new Claim("sub",user.Id.ToString()),
                new Claim("username",user.Username),
                new Claim(ClaimTypes.Role,user.Role.Name)
             };

            var token = user.generateToken(payload, _issuer, _audience, _secret);
            var claimsIdentity = new ClaimsIdentity(
                payload, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Ok("login successful");
        }
        [Authorize(Roles = "Client")]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("logout successful");
        }
        [HttpPost("addStuff")]
        public async Task<ActionResult> AddStuff(IEnumerable<UserCreationDTO> userModels)
        {
            //
            List<User> UsersToBeCreated = new List<User>();
            foreach (var userModel in userModels)
            {
                if (await _userRepository.Exists(userModel.Username))
                    return BadRequest("User already exists");
                var userToBeCreated = new User()
                {
                    Username = userModel.Username,
                    Password = entities.User.CreateHash(userModel.Password),
                    RoleId = userModel.RoleId
                };
                UsersToBeCreated.Add(userToBeCreated);
            }
            //

            _userRepository.AddRange(UsersToBeCreated);

            if (!await _userRepository.Save())
                return BadRequest("problem occured while saving user");

            return Ok("addStuff successful");

        }


    }
}