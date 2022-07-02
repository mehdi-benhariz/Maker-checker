namespace maker_checker_v1.models.entities
{
    public class Operation
    {
        public int Id { get; set; }
        public DateTime timestamp { get; set; } = DateTime.Now;
        public int userId { get; set; }
        public int validationProgressId { get; set; }
    }
}