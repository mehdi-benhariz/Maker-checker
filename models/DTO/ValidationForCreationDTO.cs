
using maker_checker_v1.models.entities;

namespace maker_checker_v1.Controllers
{
    public class ValidationForCreationDTO
    {
        public List<Rule> Rules { get; set; } = new List<Rule>();
        public ValidationForCreationDTO()
        {
        }

    }
}