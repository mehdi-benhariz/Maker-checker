using AutoMapper;
using maker_checker_v1.models.entities;
using maker_checker_v1.Services;
using Microsoft.AspNetCore.Mvc;

namespace maker_checker_v1.Controllers
{
    // [Route("api/ServiceType/{serviceTypeId}/Validation")]
    // [ApiController]
    //todo add a middleware to check if the service type exists
    public class ValidationController : ControllerBase
    {
        private readonly ServiceTypeRepository _serviceTypeRepository;
        private readonly ValidationRepository _validationRepository;

        private readonly IMapper _mapper;
        private readonly RuleRepository _ruleRepository;

        public ValidationController(ValidationRepository validationRepository, RuleRepository ruleRepository, ServiceTypeRepository serviceTypeRepository, IMapper mapper)
        {
            _serviceTypeRepository = serviceTypeRepository ?? throw new System.ArgumentNullException(nameof(serviceTypeRepository));
            _validationRepository = validationRepository ?? throw new System.ArgumentNullException(nameof(validationRepository));
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _ruleRepository = ruleRepository ?? throw new System.ArgumentNullException(nameof(ruleRepository));
        }

        [HttpGet]
        public async Task<ActionResult<Validation>> Get(int serviceTypeId)
        {
            if (!await _validationRepository.Exists(serviceTypeId))
                return NotFound("Validation not found");
            var validation = await _validationRepository.getValidation(serviceTypeId);
            return Ok(validation);
        }
        [HttpPost]
        public async Task<ActionResult<Validation>> Post(int serviceTypeId, ValidationForCreationDTO validation)
        {
            if (!await _serviceTypeRepository.Exists(serviceTypeId))
                return NotFound("Service type not found");
            var serviceType = await _serviceTypeRepository.getServiceType(serviceTypeId);
            if (serviceType!.Validation != null)
                return BadRequest("Service type already has a validation");
            var validationToCreate = _mapper.Map<Validation>(validation);
            validationToCreate.ServiceTypeId = serviceTypeId;
            validationToCreate.ServiceType = serviceType;
            validationToCreate.Rules = new List<Rule>();
            // _validationRepository.Add(validationToCreate);
            // if (!await _validationRepository.SaveChangesAsync())
            //     return BadRequest("Error creating validation");
            foreach (RuleForCreationDTO item in validation.Rules)
            {
                Console.WriteLine($"item = {item}");
                Rule rule = _mapper.Map<Rule>(item);
                //we need to provide the validation id to the rule from database
                rule.ValidationId = 1;
                _ruleRepository.Add(rule);
                await _validationRepository.Save();
            }
            return CreatedAtAction(nameof(Get), new
            {
                serviceTypeId = serviceTypeId,
                validationId = validationToCreate.Id
            }, validation);
        }
        [HttpPut]
        public async Task<ActionResult<Validation>> setValidationAsync(int serviceTypeId, ValidationForCreationDTO validation)
        {
            if (!await _serviceTypeRepository.Exists(serviceTypeId))
                return NotFound("Service type not found");
            var serviceType = await _serviceTypeRepository.getServiceType(serviceTypeId);
            var validationToBeChanged = await _validationRepository.getValidation(serviceTypeId);
            if (validationToBeChanged == null)
                return NotFound("Validation not found");

            for (int i = 0; i < validationToBeChanged.Rules.Count; i++)
            {
                if (validation.Rules.ElementAtOrDefault(i) == null)
                    validation.Rules.Add(_mapper.Map<RuleForCreationDTO>(validationToBeChanged.Rules[i]));
                //for now its static , but later it will be dynamic(count all users with specific role)
                var maxNbrByRole = 3;
                if (validation.Rules[i].Nbr > maxNbrByRole)
                    return BadRequest("Rule nbr is higher than nbr of Rules for service type");
                //todo later validate that the model contain the same Rules as the validationToBeChanged.Rules[]
                if (validationToBeChanged.Rules[i].Nbr != validation.Rules[i].Nbr)
                    validationToBeChanged.Rules[i].Nbr = validation.Rules[i].Nbr;
            }
            if (!await _validationRepository.Save())
                return BadRequest("Error updating validation");

            return Ok(validationToBeChanged);
        }
    }
}