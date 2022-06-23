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
        public ActionResult<Request> Post(RequestForCreationDTO request)
        {
            //validate request 
            var serviceType = _requestDataStore.ServiceTypes.Find(s => s.Id == request.serviceTypeId);
            if (serviceType == null)

                return NotFound("Service type not found");
            var requestToCreate = new Request(request.Name, request.serviceTypeId, request.Amount)
            {
                ServiceType = serviceType
            };

            _requestDataStore.Requests.Add(requestToCreate);
            return CreatedAtAction(nameof(Get), new
            {
                id = requestToCreate.Id
            }, requestToCreate);
        }


    }
}