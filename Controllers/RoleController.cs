using maker_checker_v1.data;
using maker_checker_v1.models.entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace maker_checker_v1.Controllers
{
    [Route("api/Role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RequestContext _requestContext;

        public RoleController(RequestContext requestContext)
        {
            _requestContext = requestContext;

        }
        [HttpGet]
        public async Task<IEnumerable<Role>> Get()
        {
            //todo:use a generic repository to get all roles

            var roles = await _requestContext.Set<Role>().ToListAsync();
            return roles;
        }
        [HttpGet("{roleId}")]
        public async Task<ActionResult<Role>> Get(int roleId)
        {
            var role = await _requestContext.Set<Role>().FindAsync(roleId);
            if (role == null)
                return NotFound("Role not found");
            return role;
        }

        [HttpPost]
        public async Task<ActionResult<Role>> AddRole([FromBody] string roleName)
        {
            if (String.IsNullOrEmpty(roleName))
                return BadRequest("Role name cannot be empty");
            var role = await _requestContext.AddAsync(new Role(roleName));
            if (await _requestContext.SaveChangesAsync() < 0)
                return BadRequest("error while saving Role");
            return Ok();
        }
        [HttpDelete("{roleId}")]
        public async Task<ActionResult<Role>> DeleteRole(int roleId)
        {
            var role = await _requestContext.Set<Role>().FindAsync(roleId);
            if (role == null)
                return NotFound("Role not found");
            _requestContext.Set<Role>().Remove(role);
            if (await _requestContext.SaveChangesAsync() < 0)
                return BadRequest("error while deleting Role");
            return Ok("deleted");
        }

    }
}