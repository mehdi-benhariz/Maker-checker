using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace maker_checker_v1.models.entities
{
    public class Rule
    {
        public Guid Id { get; set; }
        //foreign key for role
        public int RoleId { get; set; }
        public Role Role { get; set; }
        //! a rule must have a validation or validationProgress , but not both 
        public int ValidationId { get; set; }
        public virtual Validation Validation { get; set; }
        public int? ValidationProgressId { get; set; }
        public virtual ValidationProgress ValidationProgress { get; set; }
        [Range(0, byte.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public byte Nbr { get; set; } = 0;
        public Rule(byte nbr = 0)
        {
            Id = Guid.NewGuid();
            this.Nbr = nbr;
        }


    }
}