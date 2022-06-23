using maker_checker_v1.models.entities;
using Microsoft.AspNetCore.Mvc;

namespace maker_checker_v1.Controllers
{
    [Route("api/ServiceType")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly RequestDataStore _requestDataStore;
        public ServiceTypeController(RequestDataStore requestDataStore)
        {
            _requestDataStore = requestDataStore;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ServiceType>> Get()
        {
            var serviceTypes = _requestDataStore.ServiceTypes;

            return serviceTypes;
        }
        [HttpGet("{serviceTypeId}")]
        public ActionResult<ServiceType> Get(int serviceTypeId)
        {

            var serviceType = _requestDataStore.ServiceTypes.Find(s => s.Id == serviceTypeId);
            if (serviceType == null)
                return NotFound("Service type not found");
            return serviceType;
        }
        [HttpPost]
        public ActionResult<ServiceType> Post(ServiceTypeForCreationDTO serviceType)
        {
            //validate service type
            if (serviceType.Name == "")
                return BadRequest("Service type name cannot be empty");

            if (_requestDataStore.ServiceTypes.Find(s => s.Name == serviceType.Name) != null)
                return BadRequest("Service type already exists");

            var serviceTypeToCreate = new ServiceType(serviceType.Name);
            _requestDataStore.ServiceTypes.Add(serviceTypeToCreate);
            return CreatedAtAction(nameof(Get), new
            {
                serviceTypeId = serviceTypeToCreate.Id
            }, serviceTypeToCreate);
        }
    }
}