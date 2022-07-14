namespace maker_checker_v1.models.DTO.Return
{
    public class RequestToStaff
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public float Amount { get; set; } = 0;
        public string ServiceType { get; set; }
        public int UserId { get; internal set; }
        public string Owner { get; internal set; }
        public string CreationDate { get; set; }
    }
}