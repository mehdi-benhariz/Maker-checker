using AutoMapper;
using maker_checker_v1.models.entities;
using maker_checker_v1.Services;
using Microsoft.AspNetCore.Mvc;

namespace maker_checker_v1.Controllers
{
    [Route("api/Operation")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public OperationController(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
        }

        [HttpPost]
        public async Task<ActionResult<Operation>> Post(OperationForCreationDTO operationDTO)
        {
            var operation = _mapper.Map<Operation>(operationDTO);
            await _unitOfWork.Operations.Insert(operation);
            if (!await _unitOfWork.Save())
                return BadRequest("problem occured while saving operation");
            return Ok(_mapper.Map<Operation>(operation));
        }

    }
}