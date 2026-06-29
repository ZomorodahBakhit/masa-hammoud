using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Data.Entities;

namespace University.Data.Configurations
    {
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
        {
        public void Configure ( EntityTypeBuilder<Course> builder )
            {
            builder.ToTable ("Courses");
            builder.HasKey (c => c.Id);
            builder.Property (c => c.Id)
                .ValueGeneratedOnAdd ();
            builder.Property (c => c.Title)
                .IsRequired ()
                .HasMaxLength (100);
            builder.Property (c => c.StartDate)
                .IsRequired ();
            builder.Property (c => c.EndDate)
                .IsRequired ();
            }
        }
    }