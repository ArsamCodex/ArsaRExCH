using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class shhs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserClientId",
                table: "Wallet",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserClientId",
                table: "Wallet",
                column: "UserClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_Users_UserClientId",
                table: "Wallet",
                column: "UserClientId",
                principalTable: "Users",
                principalColumn: "UserClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_Users_UserClientId",
                table: "Wallet");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_UserClientId",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "UserClientId",
                table: "Wallet");
        }
    }
}
