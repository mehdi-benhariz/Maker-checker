using maker_checker_v1.models.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maker_checker_v1.configurations
{
    public class ValidationProgressConfiguration : IEntityTypeConfiguration<ValidationProgress>
    {
        public void Configure(EntityTypeBuilder<ValidationProgress> builder)
        {
            builder.HasKey(vp => vp.Id);
            builder.HasMany<Operation>(vp => vp.Operations).WithOne(o => o.ValidationProgress).HasForeignKey(o => o.validationProgressId);
            builder.Navigation<Operation>(vp => vp.Operations).AutoInclude();
            // builder.HasOne(vp => vp.Request).WithOne(r => r.ValidationProgress).HasForeignKey<ValidationProgress>(vp => vp.RequestId);
            // builder.HasMany(vp => vp.Rules).WithOne(r => r.ValidationProgress).HasForeignKey(r => r.ValidationProgressId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}