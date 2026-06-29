using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Data.Migrations
    {
    public partial class SeedRoles : Migration
        {
        protected override void Up ( MigrationBuilder migrationBuilder )
            {
            migrationBuilder.Sql (@"
        INSERT INTO Roles (Name, NormalizedName, ConcurrencyStamp)
        VALUES 
        ('Student', 'STUDENT', NEWID()),
        ('Teacher', 'TEACHER', NEWID())
    ");
            }

        protected override void Down ( MigrationBuilder migrationBuilder )
            {
            migrationBuilder.Sql (@"
        DELETE FROM Roles WHERE Name IN ('Student', 'Teacher')
    ");
            }
        }
    }
