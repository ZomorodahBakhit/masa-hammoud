using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Data.Entities;

namespace University.Data.Configurations
    {
    public class AppRoleMapping : IEntityTypeConfiguration<AppRole>
        {
        public void Configure ( EntityTypeBuilder<AppRole> builder )
            {
            builder.ToTable ("Roles");
            builder.HasKey (t => t.Id);
            builder.Property (t => t.Id).HasColumnName ("RoleId");
            }
        }
    }