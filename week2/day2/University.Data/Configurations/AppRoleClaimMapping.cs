using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Data.Entities;

namespace University.Data.Configurations
    {
    public class AppRoleClaimMapping : IEntityTypeConfiguration<AppRoleClaim>
        {
        public void Configure ( EntityTypeBuilder<AppRoleClaim> builder )
            {
            builder.ToTable ("RoleClaims");
            builder.HasKey (t => t.Id);
            builder.Property (t => t.Id).HasColumnName ("RoleClaimId");
            }
        }
    }