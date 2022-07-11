namespace maker_checker_v1
{
    public class RuleToAdmin
    {
        public Guid Id { get; set; }
        public byte Nbr { get; set; } = 0;
        public int MaxNbr { get; set; } = 0;
        public int RoleId { get; set; }
        public string RoleName { get; set; } = "";


    }
}