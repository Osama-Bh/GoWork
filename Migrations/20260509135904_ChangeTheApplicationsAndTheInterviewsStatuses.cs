using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GoWork.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTheApplicationsAndTheInterviewsStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TbApplicationStatuses",
                columns: new[] { "Id", "IsActive", "Name", "SortOrder" },
                values: new object[,]
                {
                    { 2, true, "Shortlisted", 20 },
                    { 9, true, "MissingInterview", 90 }
                });

            migrationBuilder.InsertData(
                table: "TbInterviewStatuses",
                columns: new[] { "Id", "IsActive", "Name", "SortOrder" },
                values: new object[] { 7, true, "MissingInterview", 70 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TbApplicationStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TbApplicationStatuses",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TbInterviewStatuses",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
