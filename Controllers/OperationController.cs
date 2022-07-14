using System.Security.Claims;
using AutoMapper;
using maker_checker_v1.models.entities;
using maker_checker_v1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maker_checker_v1.Controllers
{
    [Route("api/Operation")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;
        private readonly HttpContext _hContext;

        public OperationController(IMapper mapper, UnitOfWork unitOfWork, IHttpContextAccessor haccess)
        {
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
            _hContext = haccess.HttpContext ?? throw new System.ArgumentNullException(nameof(haccess));

        }


        [HttpPost]
        public async Task<ActionResult<Operation>> Post([FromBody] int requestId)
        {
            int UserId = Int32.Parse(_hContext.User.FindFirstValue("sub"));
            int RoleId = Int32.Parse(_hContext.User.FindFirstValue("roleId"));
            //
            var requestToFill = await _unitOfWork.Requests.Get(r => r.Id == requestId, includes: new List<string> { "ValidationProgress" });
            if (requestToFill == null)
                return NotFound("request doesn't exist");
            //if validation progress doesn't exist create a one
            if (requestToFill.ValidationProgress == null)
            {
                ValidationProgress valProg = new ValidationProgress(requestToFill.Id);
                await _unitOfWork.ValidationProgresses.Insert(valProg);
                if (!await _unitOfWork.Save())
                    return BadRequest("error while saving the progress");
                requestToFill.ValidationProgress = valProg;
            }
            else
                if (await _unitOfWork.Operations.Exists(o => o.userId == UserId &&
                         o.validationProgressId == requestToFill.ValidationProgress.Id))
                return Unauthorized("you don't have right to validate it!");

            var RuleToFill = await _unitOfWork.Rules.Get(expression: r => r.RoleId == RoleId
                                     && r.ValidationProgress.RequestId == requestId);

            // var operation = _mapper.Map<Operation>(operationDTO);
            var operation = new Operation()
            {
                userId = UserId,
                validationProgressId = requestToFill.ValidationProgress!.Id

            };
            await _unitOfWork.Operations.Insert(operation);
            if (!await _unitOfWork.Save())
                return BadRequest("problem occured while saving operation");
            return Ok(operation);
        }

    }
}