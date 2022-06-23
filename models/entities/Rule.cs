namespace maker_checker_v1.models.entities
{
    public class Rule
    {
        public Role role { get; set; }
        public byte nbr { get; set; }
        public Rule(string roleName, byte nbr = 0)
        {
            this.role = new Role(roleName);
            this.nbr = nbr;
        }


    }
}