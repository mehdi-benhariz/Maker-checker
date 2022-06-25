using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maker_checker_v1.models.entities
{
    public class ServiceType
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public Validation? Validation { get; set; }
        public List<Request> Requests { get; set; } = new List<Request>();

        public ServiceType(string name)
        {
            this.Name = name;
        }


    }
}