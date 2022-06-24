using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maker_checker_v1.models.entities
{
    public class ServiceType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public Validation Validation { get; set; }
        public List<Request> Requests { get; internal set; }

        public ServiceType(string name)
        {
            this.Name = name;
            this.Validation = new Validation(this.Id);
        }


    }
}