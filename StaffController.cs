using AutoMapper;
using maker_checker_v1.models.DTO.Return;
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
            var staff = await _unitOfWork.Users.GetAll(expression: u => (u.Role.Name != "Admin" && u.Role.Name != "Client"));
            return Ok(staff);
        }

    }
}