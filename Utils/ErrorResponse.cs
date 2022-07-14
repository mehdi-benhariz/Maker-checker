namespace maker_checker_v1.Utils
{
    public class ErrorResponse
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public ErrorResponse(string title, string content)
        {
            this.Title = title;
            this.Content = content;

        }

    }
}