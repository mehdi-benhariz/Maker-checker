using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maker_checker_v1.models.entities
{
    public class ValidationProgress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RequestId { get; set; }

        public Request Request { get; set; }
        public List<Rule> Rules = new List<Rule>();

        public bool IsValidated
        {
            get
            {
                return (Rules.Count == Rules.Count(r => r.nbr == 0));
            }
        }

        public ValidationProgress(int requestId)
        {
            this.RequestId = requestId;
        }
        // initRules();

    }
}