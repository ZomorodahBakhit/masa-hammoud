using Microsoft.EntityFrameworkCore;
using University.Data.Entities;

namespace University.Data.Context
    {
    public class UniversityDbContext : DbContext
        {
        public UniversityDbContext (
            DbContextOptions<UniversityDbContext> options )
            : base (options)
            { }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating ( ModelBuilder modelBuilder )
            {
            modelBuilder.ApplyConfigurationsFromAssembly (typeof (UniversityDbContext).Assembly);
            }
        }
    }