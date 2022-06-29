using maker_checker_v1.models.entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maker_checker_v1.configurations
{
    public class ValidationConfiguration : IEntityTypeConfiguration<Validation>
    {
        public void Configure(EntityTypeBuilder<Validation> builder)
        {
            builder.HasKey(v => v.Id);
            //the foriegn key is in validation table
            // builder.HasOne(v => v.ServiceType).WithOne(st => st.Validation).HasForeignKey<Validation>(v => v.ServiceTypeId);
            builder.HasMany(v => v.Rules).WithOne(r => r.Validation).HasForeignKey(r => r.ValidationId);
            builder.Navigation(v => v.Rules).AutoInclude();
        }
    }
}