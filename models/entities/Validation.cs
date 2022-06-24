using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maker_checker_v1.models.entities
{
    public class Validation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ServicesTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public List<Rule> Rules = new List<Rule>();

        public Validation(int servicesTypeId)
        {
            this.ServicesTypeId = servicesTypeId;
            initRules();
        }

        private void initRules()
        {
            //fill the rules array (change it later from db)
            // rules.Add(new Rule()
            // {
            //     role = new Role("A"),
            // });
            // rules.Add(new Rule()
            // {
            //     role = new Role("B"),
            // });
            // rules.Add(new Rule()
            // {
            //     role = new Role("C"),
            // });

        }
    }
}