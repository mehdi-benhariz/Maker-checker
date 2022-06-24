using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maker_checker_v1.models.entities
{
    public class Request
    {
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; } = 0;
        public string Status { get; set; }
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public int ValidationProgressId { get; set; }
        public ValidationProgress ValidationProgress { get; set; }

        public Request(string name, int serviceTypeId, float amount = 0, string status = "Pending")
        {
            this.Name = name;
            this.Amount = amount;
            this.Status = status;
            this.ServiceTypeId = serviceTypeId;

        }

    }
}