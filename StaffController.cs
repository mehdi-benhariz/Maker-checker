using AutoMapper;
using maker_checker_v1.models.DTO;
using maker_checker_v1.models.DTO.Return;
using maker_checker_v1.models.entities;
using maker_checker_v1.Services;
using Microsoft.AspNetCore.Mvc;

namespace maker_checker_v1
{
    [ApiController]
    [Route("api/Staff")]
    public class StaffController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StaffController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StaffUser>>> Get()
        {
            var staff = await _unitOfWork.Users.GetAll(expression: u => (u.Role.Name != "Admin" && u.Role.Name != "Client"), includes: new List<String>() { "Role" });
            return Ok(_mapper.Map<IEnumerable<StaffUser>>(staff));
        }
        [HttpPatch("{id}")]
        //update user role from request body
        public async Task<ActionResult<StaffUser>> UpdateUserRole(int id, [FromBody] int RoleId)
        {
            var userToUpdate = await _unitOfWork.Users.Get(u => u.Id == id);
            if (userToUpdate == null)
                return NotFound("User not found");
            userToUpdate.RoleId = RoleId;

            _unitOfWork.Users.Update(userToUpdate);
            if (!await _unitOfWork.Save())
                return BadRequest("Error updating user");
            return Ok(_mapper.Map<StaffUser>(userToUpdate));
        }
        [HttpPost]
        public async Task<ActionResult> AddStuff(IEnumerable<UserCreationDTO> userModels)
        {
            //
            List<User> UsersToBeCreated = new List<User>();
            foreach (var userModel in userModels)
            {
                if (await _unitOfWork.Users.Exists(u => u.Username == userModel.Username))
                    return BadRequest("User already exists");
                var userToBeCreated = new User()
                {
                    Username = userModel.Username,
                    Password = models.entities.User.CreateHash(userModel.Password),
                    RoleId = userModel.RoleId
                };
                UsersToBeCreated.Add(userToBeCreated);
            }
            //

            await _unitOfWork.Users.InsertRange(UsersToBeCreated);

            if (!await _unitOfWork.Save())
                return BadRequest("problem occured while saving user");

            return Ok("addStuff successful");

        }
    }
}