using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maker_checker_v1.models.entities
{
    public class Rule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //foreign key for role
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int ValidationId { get; set; }
        public Validation Validation { get; set; }
        public int ValidationProgressId { get; set; }
        public ValidationProgress ValidationProgress { get; set; }
        [Range(0, byte.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public byte nbr { get; set; } = 0;
        public Rule(byte nbr = 0)
        {
            this.nbr = nbr;
        }


    }
}