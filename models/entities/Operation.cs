namespace maker_checker_v1.models.entities
{
    public class Operation
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public User? User { get; set; }
        public int validationProgressId { get; set; }
        public ValidationProgress? ValidationProgress { get; set; }
        public DateTime timestamp { get; set; } = DateTime.Now;
    }
}