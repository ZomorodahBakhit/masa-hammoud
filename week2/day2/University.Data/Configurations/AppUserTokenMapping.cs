using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Data.Entities;

namespace University.Data.Configurations
    {
    public class AppUserTokenMapping : IEntityTypeConfiguration<AppUserToken>
        {
        public void Configure ( EntityTypeBuilder<AppUserToken> builder )
            {
            builder.ToTable ("UserTokens");
            builder.HasKey (t => t.UserId);
            }
        }
    }