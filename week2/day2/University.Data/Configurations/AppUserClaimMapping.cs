using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Data.Entities;

namespace University.Data.Configurations
    {
    public class AppUserClaimMapping : IEntityTypeConfiguration<AppUserClaim>
        {
        public void Configure ( EntityTypeBuilder<AppUserClaim> builder )
            {
            builder.ToTable ("UserClaims");
            builder.HasKey (t => t.Id);
            builder.Property (t => t.Id).HasColumnName ("UserClaimId");
            }
        }
    }