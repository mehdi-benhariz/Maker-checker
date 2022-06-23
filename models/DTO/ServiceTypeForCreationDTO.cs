namespace maker_checker_v1.Controllers
{
    public class ServiceTypeForCreationDTO
    {
        public string Name { get; set; }

        public ServiceTypeForCreationDTO(string name)
        {
            this.Name = name;
        }
    }

}