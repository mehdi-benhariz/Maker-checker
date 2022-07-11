using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maker_checker_v1.models.entities
{
    public class Validation
    {
        public int Id { get; set; }
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public ICollection<Rule> Rules = new List<Rule>();



    }
}