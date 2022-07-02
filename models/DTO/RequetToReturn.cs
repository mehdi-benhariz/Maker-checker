namespace maker_checker_v1.models.DTO
{
    public class RequetToReturn
    {
        public int Id { get; set; }
        public string serviceType { get; set; }

        public string Status { get; set; }
        public float Amount { get; set; } = 0;


    }
}