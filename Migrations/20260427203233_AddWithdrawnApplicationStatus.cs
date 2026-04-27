using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoWork.Migrations
{
    /// <inheritdoc />
    public partial class AddWithdrawnApplicationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TbApplicationStatuses",
                columns: new[] { "Id", "IsActive", "Name", "SortOrder" },
                values: new object[] { 7, true, "Withdrawn", 70 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TbApplicationStatuses",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
