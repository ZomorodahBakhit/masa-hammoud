using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Data.Entities;

namespace University.Data.Configurations
    {
    public class AppUserRoleMapping : IEntityTypeConfiguration<AppUserRole>
        {
        public void Configure ( EntityTypeBuilder<AppUserRole> builder )
            {
            builder.ToTable ("UserRoles");
            builder.HasKey (t => new { t.UserId, t.RoleId });
            }
        }
    }