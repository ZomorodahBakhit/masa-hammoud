using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using University.Data.Entities;

namespace University.Data.Context
    {
    public class UniversityDbContext : IdentityDbContext<
        AppUser,
        AppRole,
        int,
        AppUserClaim,
        AppUserRole,
        AppUserLogin,
        AppRoleClaim,
        AppUserToken>
        {
        public UniversityDbContext (
            DbContextOptions<UniversityDbContext> options )
            : base (options)
            { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating ( ModelBuilder modelBuilder )
            {
            base.OnModelCreating (modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly (typeof (UniversityDbContext).Assembly);
            }
        }
    }