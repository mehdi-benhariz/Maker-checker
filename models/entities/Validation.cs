namespace maker_checker_v1.models.entities
{
    public class Validation
    {
        public static int nbr = 0;
        public int Id { get; set; }
        public int servicesTypeId { get; set; }
        public DateTime timeStamp { get; set; } = DateTime.Now;
        public List<Rule> rules = new List<Rule>();

        public Validation(int servicesTypeId)
        {
            this.Id = nbr++;
            this.servicesTypeId = servicesTypeId;
            initRules();
        }

        private void initRules()
        {
            //fill the rules array (change it later from db)
            rules.Add(new Rule("A"));
            rules.Add(new Rule("B"));
            rules.Add(new Rule("C"));

        }
    }
}