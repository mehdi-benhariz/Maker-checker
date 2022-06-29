using maker_checker_v1.models.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maker_checker_v1.configurations
{
    public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Navigation(st => st.Validation).AutoInclude();
            builder.Property(s => s.Name).IsRequired().HasMaxLength(20);
            // builder.HasMany(s => s.Requests).WithOne(s => s.ServiceType).HasForeignKey(s => s.ServiceTypeId);
            builder.HasOne(s => s.Validation).WithOne(v => v.ServiceType).HasForeignKey<Validation>(v => v.ServiceTypeId);

        }
    }
}