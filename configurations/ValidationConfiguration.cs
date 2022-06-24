using maker_checker_v1.models.entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Data.SqlTypes;

namespace maker_checker_v1.configurations
{
    public class ValidationConfiguration : IEntityTypeConfiguration<Validation>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Validation> builder)
        {
            builder.HasKey(v => v.Id);
            //the foriegn key is in validation table
            builder.HasOne(v => v.ServiceType).WithOne(st => st.Validation).HasForeignKey<Validation>(v => v.ServicesTypeId);
            builder.HasMany(v => v.Rules);

        }
    }
}