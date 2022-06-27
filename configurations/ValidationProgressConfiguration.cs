using maker_checker_v1.models.entities;
using Microsoft.EntityFrameworkCore;

namespace maker_checker_v1.configurations
{
    public class ValidationProgressConfiguration : IEntityTypeConfiguration<ValidationProgress>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ValidationProgress> builder)
        {
            builder.HasKey(vp => vp.Id);
            builder.HasOne(vp => vp.Request).WithOne(r => r.ValidationProgress).HasForeignKey<ValidationProgress>(vp => vp.RequestId);
            builder.HasMany(vp => vp.Rules).WithOne(r => r.ValidationProgress).HasForeignKey(r => r.ValidationProgressId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}