using System.Security.Claims;
using AutoMapper;
using FluentValidation;
using maker_checker_v1.data.Validators;
using maker_checker_v1.models.DTO;
using maker_checker_v1.models.entities;
using maker_checker_v1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace maker_checker_v1.Controllers
{
    [Route("api/Request")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly RequestRepository _requestRepository;
        private readonly ServiceTypeRepository _serviceTypeRepository;
        private readonly IMapper _mapper;
        private readonly RequestValidator _validator;
        private readonly HttpContext _hContext;

        public RequestController(RequestRepository requestRepository, ServiceTypeRepository serviceTypeRepository, IMapper mapper, IHttpContextAccessor haccess)
        {
            _requestRepository = requestRepository ?? throw new System.ArgumentNullException(nameof(requestRepository));
            _serviceTypeRepository = serviceTypeRepository ?? throw new System.ArgumentNullException(nameof(serviceTypeRepository));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _validator = new RequestValidator();
            _hContext = haccess.HttpContext ?? throw new System.ArgumentNullException(nameof(haccess));
        }
        [HttpGet("client", Name = "GetClientRequests")]
        [Authorize(Policy = "Client")]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestsHistory([FromQuery] int pageNumber = 1)
        {
            var userId = _hContext.User.FindFirstValue("sub") ?? "1";
            var (requests, pagginationMetaData) = await _requestRepository.getRequestsHistory(Int32.Parse(userId), pageNumber);
            Response.Headers.Add("X-Paggination", JsonSerializer.Serialize(pagginationMetaData));

            return Ok(requests);
        }
        //impliment search and filter (maybe paggination)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> Get()
        {
            var requests = await _requestRepository.getRequests();
            return Ok(requests);

        }
        [HttpGet("{id}", Name = "GetRequest")]
        public async Task<ActionResult<Request>> GetAsync(int id)
        {
            var request = await _requestRepository.getRequest(id);
            if (request == null)
                return NotFound("Request not found");
            return Ok(request);
        }
        [HttpPost(Name = "submit request")]
        [Authorize(Policy = "Client")]
        public async Task<ActionResult<Request>> SubmitRequest([FromBody] RequestForCreationDTO request)
        {
            //validate request 

            var serviceType = await _serviceTypeRepository.getServiceType(request.ServiceTypeId);
            if (serviceType == null)
                return NotFound("Service type not found");

            // var requestToCreate = new Request(request.Name, request.ServiceTypeId, request.Amount);
            var requestToCreate = _mapper.Map<Request>(request);
            requestToCreate.ServiceType = serviceType;
            var userId = _hContext.User.FindFirstValue("sub");

            requestToCreate.UserId = Int32.Parse(userId);
            var validationResult = _validator.Validate(requestToCreate);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            _requestRepository.Add(requestToCreate);
            if (!await _requestRepository.Save())
                return BadRequest("Error creating request");
            // return Ok(requestToCreate);

            return CreatedAtRoute("GetRequest", new
            {
                id = requestToCreate.Id
            }, requestToCreate);


        }


        [HttpDelete("{id}", Name = "DeleteRequest")]
        public async Task<ActionResult<Request>> DeleteRequest(int id)
        {
            var request = await _requestRepository.getRequest(id);
            if (request == null)
                return NotFound("Request not found");
            _requestRepository.Remove(request);
            if (!await _requestRepository.Save())
                return BadRequest("Error deleting request");
            return Ok(request);
        }

    }
}