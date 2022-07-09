namespace maker_checker_v1.models.DTO
{
    public class RequestForCreationDTO
    {
        public string Description { get; set; }
        public float Amount { get; set; } = 0;
        public int ServiceTypeId { get; set; }

        public RequestForCreationDTO(string description, float amount, int serviceTypeId)
        {
            this.Description = description;
            this.Amount = amount;
            this.ServiceTypeId = serviceTypeId;
        }
    }
}