using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maker_checker_v1.models.entities
{

    public class Role
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Role name is required")]
        [MaxLength(20, ErrorMessage = "Role name cannot be longer than 20 characters")]
        public string Name { get; set; }
        public List<Rule> Rules { get; set; } = new List<Rule>();
        public List<User> Users { get; set; } = new List<User>();
        public Role(string name)
        {
            this.Name = name;

        }

    }
}