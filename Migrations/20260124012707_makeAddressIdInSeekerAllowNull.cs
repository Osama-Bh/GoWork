using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoWork.Migrations
{
    /// <inheritdoc />
    public partial class makeAddressIdInSeekerAllowNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TbSeekers_AddressId",
                table: "TbSeekers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TbSkills",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "TbSeekers",
                type: "int",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_TbSkills_Name",
                table: "TbSkills",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbSeekers_AddressId",
                table: "TbSeekers",
                column: "AddressId",
                unique: true,
                filter: "[AddressId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TbSkills_Name",
                table: "TbSkills");

            migrationBuilder.DropIndex(
                name: "IX_TbSeekers_AddressId",
                table: "TbSeekers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TbSkills",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "AddressId",
                table: "TbSeekers",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbSeekers_AddressId",
                table: "TbSeekers",
                column: "AddressId",
                unique: true);
        }
    }
}
