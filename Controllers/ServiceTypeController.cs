using maker_checker_v1.data;
using maker_checker_v1.models.entities;
using maker_checker_v1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace maker_checker_v1.Controllers
{
    [Route("api/ServiceType")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly ServiceTypeRepository _serviceTypeRepository;
        private readonly RequestContext _requestContext;

        public ServiceTypeController(RequestContext requestContext, ServiceTypeRepository serviceTypeRepository)
        {
            _serviceTypeRepository = serviceTypeRepository ?? throw new System.ArgumentNullException(nameof(requestContext));
            _requestContext = requestContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceType>>> Get()
        {
            var serviceTypes = await _serviceTypeRepository.getServiceTypes();
            return Ok(serviceTypes);
        }
        [HttpGet("{serviceTypeId}")]
        public async Task<ActionResult<ServiceType>> Get(int serviceTypeId)
        {

            var serviceType = await _serviceTypeRepository.getServiceType(serviceTypeId);
            if (serviceType == null)
                return NotFound("Service type not found");
            return Ok(serviceType);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceType>> Post(ServiceTypeForCreationDTO serviceType)
        {
            //validate service type
            if (serviceType.Name == "")
                return BadRequest("Service type name cannot be empty");

            if (await _serviceTypeRepository.Exists(serviceType.Name))
                return BadRequest("Service type already exists");

            var serviceTypeToCreate = new ServiceType(serviceType.Name);
            _serviceTypeRepository.Add(serviceTypeToCreate);
            if (!await _serviceTypeRepository.SaveChangesAsync())
                return BadRequest("Error creating service type");

            return CreatedAtAction(nameof(Get), new
            {
                serviceTypeId = serviceTypeToCreate.Id
            }, serviceTypeToCreate);
        }
        [HttpDelete("{serviceTypeId}")]
        public async Task<ActionResult<ServiceType>> Delete(int serviceTypeId)
        {
            var serviceType = await _serviceTypeRepository.getServiceType(serviceTypeId);
            if (serviceType == null)
                return NotFound("Service type not found");
            _serviceTypeRepository.Remove(serviceType);
            if (await _serviceTypeRepository.SaveChangesAsync())
                return Ok(serviceType);
            return BadRequest("Error deleting service type");
        }

    }
}