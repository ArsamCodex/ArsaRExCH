using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class chageclassrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_poolTransactions_AspNetUsers_UserId",
                table: "poolTransactions");

            migrationBuilder.DropIndex(
                name: "IX_poolTransactions_UserId",
                table: "poolTransactions");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "poolTransactions",
                newName: "BitcoinPoolId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "poolTransactions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 10, 6, 20, 47, 28, 751, DateTimeKind.Local).AddTicks(5680));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 10, 6, 20, 47, 28, 751, DateTimeKind.Local).AddTicks(5737));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 10, 6, 20, 47, 28, 751, DateTimeKind.Local).AddTicks(5740));

            migrationBuilder.CreateIndex(
                name: "IX_poolTransactions_BitcoinPoolId",
                table: "poolTransactions",
                column: "BitcoinPoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_poolTransactions_BitcoinPools_BitcoinPoolId",
                table: "poolTransactions",
                column: "BitcoinPoolId",
                principalTable: "BitcoinPools",
                principalColumn: "BitcoinPoolId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_poolTransactions_BitcoinPools_BitcoinPoolId",
                table: "poolTransactions");

            migrationBuilder.DropIndex(
                name: "IX_poolTransactions_BitcoinPoolId",
                table: "poolTransactions");

            migrationBuilder.RenameColumn(
                name: "BitcoinPoolId",
                table: "poolTransactions",
                newName: "ApplicationUserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "poolTransactions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 10, 5, 11, 30, 3, 836, DateTimeKind.Local).AddTicks(8231));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 10, 5, 11, 30, 3, 836, DateTimeKind.Local).AddTicks(8280));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 10, 5, 11, 30, 3, 836, DateTimeKind.Local).AddTicks(8284));

            migrationBuilder.CreateIndex(
                name: "IX_poolTransactions_UserId",
                table: "poolTransactions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_poolTransactions_AspNetUsers_UserId",
                table: "poolTransactions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
