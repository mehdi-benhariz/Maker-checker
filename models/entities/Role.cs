using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maker_checker_v1.models.entities
{

    public class Role
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Rule> Rules { get; set; } = new List<Rule>();
        public List<User> Users { get; set; } = new List<User>();
        public Role(string name)
        {
            this.Name = name;

        }
        public int MaxNbr() => Users.Count();
    }
}