using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Data.Entities;

namespace University.Data.Configurations
    {
    public class AppUserMapping : IEntityTypeConfiguration<AppUser>
        {
        public void Configure ( EntityTypeBuilder<AppUser> builder )
            {
            builder.ToTable ("Users");
            builder.HasKey (t => t.Id);
            builder.Property (t => t.Id).HasColumnName ("UserId");
            }
        }
    }