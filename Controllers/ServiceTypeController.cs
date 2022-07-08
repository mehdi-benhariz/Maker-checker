using AutoMapper;
using maker_checker_v1.data;
using maker_checker_v1.data.Validators;
using maker_checker_v1.models.entities;
using maker_checker_v1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace maker_checker_v1.Controllers
{
    [Route("api/ServiceType")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly ServiceTypeRepository _serviceTypeRepository;
        private readonly IMapper _mapper;
        private readonly ServiceTypeValidator _validator;

        public ServiceTypeController(ServiceTypeRepository serviceTypeRepository, IMapper mapper)
        {
            _serviceTypeRepository = serviceTypeRepository ?? throw new System.ArgumentNullException(nameof(ServiceTypeRepository));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(IMapper));
            _validator = new ServiceTypeValidator();
        }
        [HttpGet]
        [Authorize]
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
            var serviceTypeToCreate = _mapper.Map<ServiceType>(serviceType);
            var validationResult = _validator.Validate(serviceTypeToCreate);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (await _serviceTypeRepository.Exists(serviceType.Name))
                return BadRequest("Service type already exists");

            // var serviceTypeToCreate = new ServiceType(serviceType.Name);
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
            if (!await _serviceTypeRepository.SaveChangesAsync())
                return BadRequest("Error deleting service type");
            return Ok(serviceType);
        }

    }
}