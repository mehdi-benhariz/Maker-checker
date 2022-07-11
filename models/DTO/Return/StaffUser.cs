namespace maker_checker_v1.models.DTO.Return
{
    public class StaffUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string CreationDate { get; set; }
    }
}