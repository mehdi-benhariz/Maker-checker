using maker_checker_v1.models.entities;
using Microsoft.AspNetCore.Mvc;

namespace maker_checker_v1.Controllers
{
    [Route("api/ServiceType/{serviceTypeId}/Validation")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private readonly RequestDataStore _requestDataStore;

        public ValidationController(RequestDataStore requestDataStore)
        {
            _requestDataStore = requestDataStore;
        }


        [HttpGet("{validationId}")]
        public ActionResult<Validation> Get(int serviceTypeId, int validationId)
        {
            if (_requestDataStore.ServiceTypeExists(serviceTypeId))
                return NotFound("Service type not found");
            var serviceType = _requestDataStore.ServiceTypes.Find(s => s.Id == serviceTypeId);

            return serviceType.Validation;
        }
        [HttpPut]
        public ActionResult<Validation> setValidation(int serviceTypeId, ValidationForCreationDTO validation)
        {
            if (!_requestDataStore.ServiceTypeExists(serviceTypeId))
                return NotFound("Service type not found");
            var serviceType = _requestDataStore.ServiceTypes.Find(s => s.Id == serviceTypeId);
            var validationToBeChanged = serviceType.Validation;
            //validate rule from validation: nbr isn't higher nbr of rules for service type
            //compare rule with validationToBeChanged.rules[] , if it's different, update validationToBeChanged.rules[]
            for (int i = 0; i < validationToBeChanged.Rules.Count; i++)
            {
                if (validation.rules.ElementAtOrDefault(i) == null)
                    validation.rules.Add(validationToBeChanged.Rules.ElementAtOrDefault(i));

                var maxNbrByRole = _requestDataStore.GetRoleMaxNbr(serviceType.Validation.Rules[i].Role.Name);
                if (validation.rules[i].nbr > maxNbrByRole)
                    return BadRequest("Rule nbr is higher than nbr of rules for service type");
                //todo later validate that the model contain the same rules as the validationToBeChanged.rules[]
                if (validationToBeChanged.Rules[i].nbr != validation.rules[i].nbr)
                    validationToBeChanged.Rules[i].nbr = validation.rules[i].nbr;

            }

            return Ok(validationToBeChanged);
        }
    }
}