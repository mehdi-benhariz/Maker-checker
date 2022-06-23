
using maker_checker_v1.models.entities;

namespace maker_checker_v1.Controllers
{
    public class ValidationForCreationDTO
    {
        public List<Rule> rules { get; set; }
        public ValidationForCreationDTO()
        {
            rules = new List<Rule>();
        }

    }
}