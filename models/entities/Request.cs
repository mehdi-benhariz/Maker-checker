namespace maker_checker_v1.models.entities
{
    public class Request
    {
        public static int nbr = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; } = 0;
        public string Status { get; set; }

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public ValidationProgress ValidationProgress { get; set; }
        public Request(string name, int serviceTypeId, float amount = 0, string status = "Pending")
        {
            this.Id = nbr++;
            this.Name = name;
            this.Amount = amount;
            this.Status = status;
            this.ServiceTypeId = serviceTypeId;
            ValidationProgress = new ValidationProgress(this.Id);

        }

    }
}