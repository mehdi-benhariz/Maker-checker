using maker_checker_v1.models.entities;
using maker_checker_v1.Services;
using Microsoft.AspNetCore.Mvc;

namespace maker_checker_v1.Controllers
{
    [Route("api/ServiceType/{serviceTypeId}/Validation")]
    [ApiController]
    //todo add a middleware to check if the service type exists
    public class ValidationController : ControllerBase
    {
        private readonly ServiceTypeRepository _serviceTypeRepository;
        private readonly ValidationRepository _validationRepository;

        public ValidationController(ValidationRepository validationRepository, ServiceTypeRepository serviceTypeRepository)
        {
            _serviceTypeRepository = serviceTypeRepository ?? throw new System.ArgumentNullException(nameof(serviceTypeRepository));
            _validationRepository = validationRepository ?? throw new System.ArgumentNullException(nameof(validationRepository));
        }

        [HttpGet("{validationId}")]
        public async Task<ActionResult<Validation>> Get(int serviceTypeId, int? validationId)
        {
            int id = validationId ?? serviceTypeId;
            if (!await _validationRepository.Exists(id))
                return NotFound("Validation not found");
            var validation = await _validationRepository.getValidation(id);
            return Ok(validation);
        }
        [HttpPost]
        public async Task<ActionResult<Validation>> Post(int serviceTypeId, ValidationForCreationDTO validation)
        {
            if (!await _serviceTypeRepository.Exists(serviceTypeId))
                return NotFound("Service type not found");

            var serviceType = await _serviceTypeRepository.getServiceType(serviceTypeId);
            //convert the validation to a validation entity

            // serviceType.ValidationId = validation;
            // _validationRepository.Add(validation);
            var validationToCreate = new Validation(serviceTypeId)
            {
                Rules = validation.Rules,

            };
            _validationRepository.Add(validationToCreate);
            if (!await _validationRepository.SaveChangesAsync())
                return BadRequest("Error creating validation");
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
                    validation.Rules.Add(validationToBeChanged.Rules[i]);
                //for now its static , but later it will be dynamic(count all users with specific role)
                var maxNbrByRole = 3;
                if (validation.Rules[i].nbr > maxNbrByRole)
                    return BadRequest("Rule nbr is higher than nbr of Rules for service type");
                //todo later validate that the model contain the same Rules as the validationToBeChanged.Rules[]
                if (validationToBeChanged.Rules[i].nbr != validation.Rules[i].nbr)
                    validationToBeChanged.Rules[i].nbr = validation.Rules[i].nbr;
            }
            await _validationRepository.SaveChangesAsync();

            return Ok(validationToBeChanged);
        }
    }
}