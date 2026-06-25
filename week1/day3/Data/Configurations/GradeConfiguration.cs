using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using university.Models;

namespace university.Data.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grades");

            builder.HasKey(g => g.Id);

            builder.HasOne(g => g.Assignment)
                   .WithMany(a => a.Grades)
                   .HasForeignKey(g => g.AssignmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(g => g.Student)
                   .WithMany(u => u.Grades)
                   .HasForeignKey(g => g.StudentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}