using AutoMapper;
using maker_checker_v1.models.entities;
using maker_checker_v1.Services;
using Microsoft.AspNetCore.Mvc;

namespace maker_checker_v1.Controllers
{
    [ApiController]
    [Route("api/Rule")]
    public class RuleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly RuleRepository _ruleRepository;
        private readonly ValidationRepository _validationRepository;

        public RuleController(IMapper mapper, RuleRepository ruleRepository, ValidationRepository validationRepository)
        {
            _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
            _ruleRepository = ruleRepository ?? throw new System.ArgumentNullException(nameof(ruleRepository));
            _validationRepository = validationRepository ?? throw new System.ArgumentNullException(nameof(validationRepository));

        }
        [HttpPost]
        public async Task<IActionResult> AddRule([FromBody] RuleForCreationDTO ruleDto, [FromHeader] int serviceTypeId)
        {
            var validation = await _validationRepository.getValidation(serviceTypeId);
            if (validation == null)
            {
                //create and save validation
                validation = new Validation
                {
                    ServiceTypeId = serviceTypeId,
                };
                _validationRepository.Add(validation);
                if (!await _validationRepository.Save())
                    return BadRequest("Could not save validation");

            }
            var rule = _mapper.Map<Rule>(ruleDto);
            rule.ValidationId = validation.Id;

            _ruleRepository.Add(rule);
            if (!await _ruleRepository.Save())
                return BadRequest("Could not save rule");

            return Ok(validation);
        }
    }
}