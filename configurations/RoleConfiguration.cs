using maker_checker_v1.models.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace maker_checker_v1.configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(20);
            // builder.HasMany(r => r.Users).WithOne(u => u.Role).HasForeignKey(u => u.RoleId);
        }
    }
}