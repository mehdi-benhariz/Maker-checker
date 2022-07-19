namespace maker_checker_v1.Utils
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; } = 500;
        public CustomException(string message) : base(message)
        {
        }
    }
}