namespace maker_checker_v1
{
    //todo : discuss this with ahmed : +appraoch 1 calculate max nbr by querying the data, complexe query  
    //todo :                           +approach 2 calculate max nbr by calling count on ServiceType.Validation.Rules[i].Role.users.count()
    public class ServiceTypeToAdmin
    {
        public int Id { get; set; }
        public string Name { get; set; } = "intrabank";
        public List<RuleToAdmin> Rules { get; set; } = new List<RuleToAdmin>();
    }
}