using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoWork.Migrations
{
    /// <inheritdoc />
    public partial class addAddressToInterview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Locatoin",
                table: "TbInterviews");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "TbInterviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TbInterviews_AddressId",
                table: "TbInterviews",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbInterviews_TbAddresses_AddressId",
                table: "TbInterviews",
                column: "AddressId",
                principalTable: "TbAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbInterviews_TbAddresses_AddressId",
                table: "TbInterviews");

            migrationBuilder.DropIndex(
                name: "IX_TbInterviews_AddressId",
                table: "TbInterviews");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "TbInterviews");

            migrationBuilder.AddColumn<string>(
                name: "Locatoin",
                table: "TbInterviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
