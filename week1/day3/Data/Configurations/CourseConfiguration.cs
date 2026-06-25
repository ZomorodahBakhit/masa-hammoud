using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using university.Models;

namespace university.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title).IsRequired().HasMaxLength(100);
            builder.Property(c => c.StartDate).IsRequired();
            builder.Property(c => c.EndDate).IsRequired();

            builder.HasOne(c => c.Teacher)
                   .WithMany(u => u.AuthoredCourses)
                   .HasForeignKey(c => c.TeacherId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Syllabus)
                   .WithMany()
                   .HasForeignKey(c => c.SyllabusId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}