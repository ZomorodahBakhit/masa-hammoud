using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using university.Models;

namespace university.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName).IsRequired();
            builder.Property(u => u.PhoneNumber).IsRequired();
            builder.Property(u => u.Role).IsRequired();

            //Set a unique constraint on the EmailAddress field in Users
            builder.HasIndex(u => u.EmailAddress).IsUnique();
            builder.Property(u => u.EmailAddress).IsRequired();
        }
    }
}