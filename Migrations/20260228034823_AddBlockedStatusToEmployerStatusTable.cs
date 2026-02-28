using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoWork.Migrations
{
    /// <inheritdoc />
    public partial class AddBlockedStatusToEmployerStatusTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TbEmployerStatuses",
                columns: new[] { "Id", "IsActive", "Name", "SortOrder" },
                values: new object[] { 6, true, "Blocked", 60 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TbEmployerStatuses",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
