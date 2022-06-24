using maker_checker_v1.models.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace maker_checker_v1.configurations
{
    public class RuleConfiguration : IEntityTypeConfiguration<Rule>
    {
        public void Configure(EntityTypeBuilder<Rule> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasOne(r => r.Role).WithMany(r => r.Rules).HasForeignKey(r => r.RoleId);
            builder.HasOne(r => r.ValidationProgress).WithMany(vp => vp.Rules).HasForeignKey(vp => vp.ValidationProgressId);
            builder.HasOne(r => r.Validation).WithMany(v => v.Rules).HasForeignKey(r => r.ValidationId);
        }
    }
}