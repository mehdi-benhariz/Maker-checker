using maker_checker_v1.data;
using maker_checker_v1.models.entities;
using maker_checker_v1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace maker_checker_v1.Controllers
{
    [Route("api/Role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public RoleController(UnitOfWork unitOfWork, RequestContext requestContext, RoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));

        }

        [HttpGet]
        public async Task<IEnumerable<Role>> Get()
        {
            return await _unitOfWork.Roles.GetAll();
        }
        [HttpGet("staff")]
        public async Task<IEnumerable<Role>> GetStaffRoles()
        {
            return await _unitOfWork.Roles.GetAll(r => r.Name != "Admin" && r.Name != "Client");
        }


        [HttpGet("{roleId}")]
        public async Task<ActionResult<Role>> Get(int roleId)
        {
            var role = await _unitOfWork.Roles.Get(r => r.Id == roleId);
            if (role == null)
                return NotFound("Role not found");
            return role;
        }

        [HttpPost]
        public async Task<ActionResult<Role>> AddRole([FromBody] string roleName)
        {
            if (String.IsNullOrEmpty(roleName))
                return BadRequest("Role name cannot be empty");
            if (await _unitOfWork.Roles.Exists(r => r.Name == roleName))
                return BadRequest("Role already exists");
            await _unitOfWork.Roles.Insert(new Role(roleName));
            if (!await _unitOfWork.Save())
                return BadRequest("error while saving Role");
            return Ok();
        }

        [HttpDelete("{roleId}")]
        public async Task<ActionResult<Role>> DeleteRole(int roleId)
        {
            var role = await _unitOfWork.Roles.Get(r => r.Id == roleId);
            if (role == null)
                return NotFound("Role not found");
            _unitOfWork.Roles.Delete(role);

            if (await _unitOfWork.Save())
                return BadRequest("error while deleting Role");
            return Ok("deleted");
        }

    }
}