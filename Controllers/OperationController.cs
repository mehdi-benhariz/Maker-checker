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
        [Authorize]
        public async Task<ActionResult<Operation>> Post([FromBody] int requestId)
        {
            try
            {
                int UserId = Int32.Parse(_hContext.User.FindFirstValue("sub"));
                int RoleId = Int32.Parse(_hContext.User.FindFirstValue("roleId"));
                //
                var requestToFill = await _unitOfWork.Requests.Get(r => r.Id == requestId, includes: new List<string> { "ValidationProgress" });
                if (requestToFill == null)
                    throw new Exception("request |Request not found");
                //if validation progress doesn't exist create a one
                if (requestToFill.ValidationProgress == null)
                {
                    ValidationProgress valProg = new ValidationProgress(requestToFill.Id);
                    await _unitOfWork.ValidationProgresses.Insert(valProg);
                    if (!await _unitOfWork.Save())
                        throw new Exception("request |Error while saving validation progress");
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
                    throw new Exception("request |Error while saving operation");
                _unitOfWork.Operations.Detach(operation);
                //todo : find a better solution later

                var finalRequest = await _unitOfWork.Requests.Get(r => r.Id == requestId, includes: new List<string> { "ValidationProgress", "ServiceType", "ServiceType.Validation" });
                finalRequest.Status = finalRequest.CalcStatus();

                _unitOfWork.Requests.Update(finalRequest);
                if (!await _unitOfWork.Save())
                    throw new Exception("request |Error while saving request");
                return Ok(operation);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

    }
}