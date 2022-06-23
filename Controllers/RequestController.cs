using maker_checker_v1.models.DTO;
using maker_checker_v1.models.entities;
using Microsoft.AspNetCore.Mvc;

namespace maker_checker_v1.Controllers
{
    [Route("api/Request")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly RequestDataStore _requestDataStore;

        public RequestController(RequestDataStore requestDataStore)
        {
            _requestDataStore = requestDataStore;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Request>> Get()
        {
            return _requestDataStore.Requests;
        }
        [HttpGet("{id}")]
        public ActionResult<Request> Get(int id)
        {
            var request = _requestDataStore.Requests.Find(r => r.Id == id);
            if (request == null)
            {
                return NotFound("Request not found");
            }
            return request;
        }
        [HttpPost]
        public ActionResult<Request> SubmitRequest(RequestForCreationDTO request)
        {
            //validate request 
            var serviceType = _requestDataStore.ServiceTypes.FirstOrDefault(s => s.Id == request.serviceTypeId);
            Console.WriteLine($"serviceType: {serviceType}");
            if (serviceType == null)
                return NotFound("Service type not found");
            var requestToCreate = new Request(request.Name, request.serviceTypeId, request.Amount)
            {
                ServiceType = serviceType,
            };
            requestToCreate.ValidationProgress = new ValidationProgress(requestToCreate.Id)
            {
                rules = serviceType.validation.rules
            };



            _requestDataStore.Requests.Add(requestToCreate);
            return CreatedAtAction(nameof(Get), new
            {
                id = requestToCreate.Id
            }, requestToCreate);
        }
        /// <summary>
        /// User with role:role can validate request with id:requestId
        /// </summary>
        /// <param name="requestId"></param>
        /// <body name="role"></body>
        /// <returns></returns>
        [HttpPut("validate/{requestId}")]
        public ActionResult<Request> validateRequest(int requestId, [FromBody] string role)
        {
            var request = _requestDataStore.Requests.Find(r => r.Id == requestId);
            if (request == null)
                return NotFound("Request not found");
            if (!request.ValidationProgress.isValidated)
            {
                var validationProgress = request.ValidationProgress;
                var rule = validationProgress.rules.First(r => r.role.Name == role);
                if (rule == null)
                    return NotFound("Rule not found");
                if (rule.nbr == 0)
                    return BadRequest("Rule already validated");
                else
                    rule.nbr--;
                //todo :add event listner to update validation progress

                return Ok(request);
            }
            return BadRequest("Request already validated");
        }
    }
}