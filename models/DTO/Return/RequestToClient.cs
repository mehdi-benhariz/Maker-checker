namespace maker_checker_v1.models.DTO
{
    public class RequestToClient
    {
        public int Id { get; set; }
        public string serviceType { get; set; }
        public string Status { get; set; }
        public float Amount { get; set; } = 0;
        public byte Progress { get; set; } = 0;
        public string CreationDate { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");


    }
}