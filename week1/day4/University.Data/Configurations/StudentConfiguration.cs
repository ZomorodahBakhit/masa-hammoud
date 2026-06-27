using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using University.Data.Entities;


namespace University.Data.Configurations
    {
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
        {
        public void Configure ( EntityTypeBuilder<Student> builder )
            {
            builder.ToTable ("Students");
            builder.HasKey (s => s.Id);
            builder.Property (s => s.Id)
                .ValueGeneratedOnAdd ();
            builder.Property (s => s.Name)
                .IsRequired ()
                .HasMaxLength (100);
            builder.Property (s => s.Email)
                .IsRequired ()
                .HasMaxLength (150);
            builder.HasIndex (s => s.Email)
                   .IsUnique ();
            }
        }
    }