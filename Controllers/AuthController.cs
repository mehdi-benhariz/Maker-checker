using maker_checker_v1.data;
using maker_checker_v1.models.entities;
using Microsoft.AspNetCore.Mvc;

namespace maker_checker_v1.models.DTO
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        private readonly RequestContext _context;

        public AuthController(RequestContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Register(User user)
        {
            // _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("register")]
        public async Task<ActionResult> Login(User user)
        {
            // _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("logout")]
        public async Task<ActionResult> Logout(User user)
        {
            // _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}