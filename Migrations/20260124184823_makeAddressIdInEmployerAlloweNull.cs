using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoWork.Migrations
{
    /// <inheritdoc />
    public partial class makeAddressIdInEmployerAlloweNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TbEmployers_AddressId",
                table: "TbEmployers");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "TbEmployers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_TbEmployers_AddressId",
                table: "TbEmployers",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TbEmployers_AddressId",
                table: "TbEmployers");

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "TbEmployers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbEmployers_AddressId",
                table: "TbEmployers",
                column: "AddressId",
                unique: true);
        }
    }
}
