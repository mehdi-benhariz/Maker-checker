namespace maker_checker_v1.models.entities
{
    public class Role
    {
        public static int nbr = 0;
        public int Id { get; set; }
        public string Name { get; set; }
        public Role(string name)
        {
            this.Id = nbr++;
            this.Name = name;

        }

    }
}