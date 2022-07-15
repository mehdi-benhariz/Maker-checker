namespace maker_checker_v1.models.DTO.Return
{
    public class RequestToAdmin : RequestToClient
    {
        public string Owner { get; set; }
        public string ProfileImg { get; set; } = "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2.2&w=160&h=160&q=80";

    }
}