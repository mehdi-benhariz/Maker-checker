using maker_checker_v1.models.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maker_checker_v1.configurations
{
    public class RequestConfiguration : IEntityTypeConfiguration<Request>
    {

        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasKey(r => r.Id);
            //generate key automatically
            builder.Property(r => r.Name).IsRequired().HasMaxLength(20);
            builder.Property(r => r.Amount).IsRequired().HasDefaultValue(0);
            builder.HasOne<ServiceType>(r => r.ServiceType).WithMany(st => st.Requests).HasForeignKey(r => r.ServiceTypeId);
            builder.HasOne<ValidationProgress>(r => r.ValidationProgress).WithOne(vp => vp.Request);

        }
    }
}