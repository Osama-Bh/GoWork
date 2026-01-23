using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoWork.Migrations
{
    /// <inheritdoc />
    public partial class deletePostalCodeFromAddressTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "TbAddresses");

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhoto",
                table: "TbSeekers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LogoUrl",
                table: "TbEmployers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePhoto",
                table: "TbSeekers");

            migrationBuilder.AlterColumn<string>(
                name: "LogoUrl",
                table: "TbEmployers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "TbAddresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
