
using maker_checker_v1.models.entities;

namespace maker_checker_v1.Controllers
{
    public class ValidationForCreationDTO
    {
        public List<RuleForCreationDTO> Rules { get; set; } = new List<RuleForCreationDTO>();
        public ValidationForCreationDTO()
        {
        }

    }
}