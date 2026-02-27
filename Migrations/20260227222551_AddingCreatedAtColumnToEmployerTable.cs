using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoWork.Migrations
{
    /// <inheritdoc />
    public partial class AddingCreatedAtColumnToEmployerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TbEmployers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "TbEmployerStatuses",
                columns: new[] { "Id", "IsActive", "Name", "SortOrder" },
                values: new object[] { 5, true, "Rejected", 50 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TbEmployerStatuses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TbEmployers");
        }
    }
}
