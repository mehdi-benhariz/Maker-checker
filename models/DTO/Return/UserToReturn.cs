namespace maker_checker_v1
{
    public class UserToReturn
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        public UserToReturn(int id, string username, string role)
        {
            Id = id;
            Username = username;
            Role = role;
        }

    }
}