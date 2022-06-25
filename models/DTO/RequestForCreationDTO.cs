namespace maker_checker_v1.models.DTO
{
    public class RequestForCreationDTO
    {
        public string Name { get; set; }
        public float Amount { get; set; } = 0;
        public int ServiceTypeId { get; set; }

        public RequestForCreationDTO(string name, float amount, int serviceTypeId)
        {
            this.Name = name;
            this.Amount = amount;
            this.ServiceTypeId = serviceTypeId;
        }
    }
}