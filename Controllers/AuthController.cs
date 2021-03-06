using System.Security.Claims;
using AutoMapper;
using maker_checker_v1.models.entities;
using maker_checker_v1.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using maker_checker_v1.Utils;
namespace maker_checker_v1.models.DTO
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;

        public AuthController(UnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _secret = _configuration["JWT:Secret"];
            _issuer = _configuration["JWT:Issuer"];
            _audience = _configuration["JWT:Audience"];
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserLoginDTO userModel)
        {
            if (await _unitOfWork.Users.Exists(u => u.Username == userModel.Username))
                // if (await _userRepository.Exists(userModel.Username))
                throw new CustomException("useranme |User already exists") { StatusCode = 400 };
            //
            var userToBeCreated = new User()
            {
                Username = userModel.Username,
                Password = entities.User.CreateHash(userModel.Password),
            };
            await _unitOfWork.Users.Insert(userToBeCreated);
            if (!await _unitOfWork.Save())
                throw new CustomException("user |Error while saving user") { StatusCode = 500 };

            //generating token
            var payload = new Claim[]{
                new Claim("sub",userToBeCreated.Id.ToString()),
                new Claim("username",userToBeCreated.Username),
                new Claim(ClaimTypes.Role,"Client")
             };

            var token = userToBeCreated.generateToken(payload, _issuer, _audience, _secret);

            var claimsIdentity = new ClaimsIdentity(
                payload, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

            return Ok(userToBeCreated);
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserToReturn>> Login(UserLoginDTO userModel)
        {
            User? user = await _unitOfWork.Users.Get(u => u.Username == userModel.Username, new List<string> { "Role" });
            //todo make a standart error format
            if (user == null)
                throw new CustomException("username|User not found") { StatusCode = 400 };
            // return BadRequest("User does not exist");
            if (!entities.User.CompareHash(userModel.Password, user.Password))
                throw new CustomException("password| Wrong password") { StatusCode = 400 };

            var payload = new Claim[]{
                new Claim("sub",user.Id.ToString()),
                new Claim("username",user.Username),
                new Claim(ClaimTypes.Role,user.Role.Name),
                new Claim("roleId", user.RoleId.ToString())
             };

            var token = user.generateToken(payload, _issuer, _audience, _secret);
            var claimsIdentity = new ClaimsIdentity(
                payload, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            return Ok(_mapper.Map<UserToReturn>(user));
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("logout successful");
        }
        [HttpGet("getUser")]
        [Authorize]
        public async Task<ActionResult<UserToReturn>> GetUser()
        {
            var user = await _unitOfWork.Users.Get(u => u.Id == int.Parse(User.FindFirst("sub")!.Value), new List<string> { "Role" });
            return Ok(_mapper.Map<UserToReturn>(user));
        }


    }
}