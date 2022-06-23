namespace maker_checker_v1.models.entities
{
    public class ServiceType
    {
        public static int nbr = 0;
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public Validation validation { get; set; }
        public ServiceType(string name)
        {
            this.Id = nbr++;
            this.Name = name;
            this.validation = new Validation(this.Id);
        }


    }
}