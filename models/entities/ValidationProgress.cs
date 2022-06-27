using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maker_checker_v1.models.entities
{
    public class ValidationProgress
    {

        public int Id { get; set; }
        public int RequestId { get; set; }
        public Request Request { get; set; }
        public List<Rule> Rules = new List<Rule>();

        public bool IsValidated
        {
            get
            {
                bool hasValidation = Request.ServiceType.Validation != null;
                if (!hasValidation)
                    return false;
                bool countCompleted = Rules.Count == Rules.Count(r => r.Nbr == 0);
                return countCompleted;

            }
        }

        public ValidationProgress(int requestId)
        {
            this.RequestId = requestId;
        }
        // initRules();

    }
}