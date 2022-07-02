

namespace maker_checker_v1.models.entities
{
    public class ValidationProgress
    {

        public int Id { get; set; }
        public int RequestId { get; set; }
        public Request Request { get; set; }
        public List<Operation> Operations = new List<Operation>();

        public bool IsValidated
        {
            get
            {//todo:change it later
                return Operations.Count > 0;
            }
        }

        public ValidationProgress(int requestId)
        {
            this.RequestId = requestId;
        }
        // initRules();

    }
}