using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using university.Models;

namespace university.Data.Configurations
{
    public class SyllabusConfiguration : IEntityTypeConfiguration<Syllabus>
    {
        public void Configure(EntityTypeBuilder<Syllabus> builder)
        {
            builder.ToTable("Syllabuses");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Description).HasMaxLength(500);
        }
    }
}