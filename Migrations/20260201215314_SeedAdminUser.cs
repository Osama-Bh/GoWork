using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoWork.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<IdentityUser>();

            var admin = new IdentityUser
            {
                UserName = "gowork.platform@gmail.com",
                NormalizedUserName = "GOWORK.PLATFORM@GMAIL.COM",
                Email = "gowork.platform@gmail.com",
                NormalizedEmail = "GOWORK.PLATFORM@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = System.Guid.NewGuid().ToString(),
                ConcurrencyStamp = System.Guid.NewGuid().ToString(),
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            var passwordHash = hasher.HashPassword(admin, "Mohammed$1");

            // Insert user without specifying Id (auto-increment)
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[]
                {
                "UserName", "NormalizedUserName", "Email", "NormalizedEmail",
                "EmailConfirmed", "PasswordHash", "SecurityStamp",
                "ConcurrencyStamp", "PhoneNumberConfirmed", "TwoFactorEnabled",
                "LockoutEnabled", "AccessFailedCount"
                },
                values: new object[]
                {
                admin.UserName,
                admin.NormalizedUserName,
                admin.Email,
                admin.NormalizedEmail,
                true,
                passwordHash,
                admin.SecurityStamp,
                admin.ConcurrencyStamp,
                false,
                false,
                true,
                0
                }
            );

            // Insert into AspNetUserRoles with RoleId = 1 using the inserted user's Id
            migrationBuilder.Sql(@"
            INSERT INTO AspNetUserRoles (UserId, RoleId)
            SELECT Id, 1 FROM AspNetUsers WHERE Email = 'gowork.platform@gmail.com'
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DELETE FROM AspNetUserRoles 
            WHERE UserId = (SELECT Id FROM AspNetUsers WHERE Email = 'gowork.platform@gmail.com') 
              AND RoleId = 1
        ");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Email",
                keyValue: "gowork.platform@gmail.com"
            );
        }
    }
}
