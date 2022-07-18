using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//create an enum for request status

namespace maker_checker_v1.models.entities
{
    public class Request
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public float Amount { get; set; } = 0;
        public string Status { get; set; }
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public ValidationProgress? ValidationProgress { get; set; }
        public int UserId { get; internal set; }
        public User User { get; internal set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public Request(int serviceTypeId, float amount = 0, string status = "Pending")
        {
            this.Amount = amount;
            this.Status = status;
            this.ServiceTypeId = serviceTypeId;

        }
        public string CalcStatus()
        {
            var prog = ValidationProgress.Progress();
            return prog == 100 ? "Approved" : "Pending";
        }

    }
}