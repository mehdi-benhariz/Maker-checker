namespace maker_checker_v1.models.entities
{
    public class ValidationProgress
    {
        public static int nbr = 0;
        public int Id { get; set; }
        public int requestId { get; set; }
        public List<Rule> rules = new List<Rule>();

        public bool isValidated
        {
            get
            {
                return (rules.Count == rules.Count(r => r.nbr == 0));
            }
        }

        public ValidationProgress(int requestId)
        {
            this.Id = nbr++;
            this.requestId = requestId;
        }
        // initRules();

    }
}