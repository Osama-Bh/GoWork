using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoWork.Migrations
{
    /// <inheritdoc />
    public partial class CleanupExistingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
-- 1. Bridge tables that reference Skills
DELETE FROM [TbSeekerSkills];
DELETE FROM [TbJobSkills];

-- 2. Tables with required references to Currencies or Addresses
DELETE FROM [TbInterviews];
DELETE FROM [TbApplications];
DELETE FROM [TbJobs];

-- 3. Nullify optional references to Addresses to preserve Seekers and Employers
UPDATE [TbSeekers] SET [AddressId] = NULL;
UPDATE [TbEmployers] SET [AddressId] = NULL;

-- 4. Delete the core tables in reverse dependency order
DELETE FROM [TbAddresses];
DELETE FROM [TbGovernates];
DELETE FROM [TbCountries];
DELETE FROM [TbCurrencies];
DELETE FROM [TbSkills];
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
