using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class bitpooll1121 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "TxHash",
                table: "poolTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "receiverAdress",
                table: "poolTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Daterefilled",
                table: "BitcoinPools",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 10, 4, 1, 17, 13, 929, DateTimeKind.Local).AddTicks(2543));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 10, 4, 1, 17, 13, 929, DateTimeKind.Local).AddTicks(2594));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 10, 4, 1, 17, 13, 929, DateTimeKind.Local).AddTicks(2598));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_poolTransactions_AspNetUsers_UserId",
                table: "poolTransactions");

            migrationBuilder.DropIndex(
                name: "IX_poolTransactions_UserId",
                table: "poolTransactions");

            migrationBuilder.DropColumn(
                name: "TxHash",
                table: "poolTransactions");

            migrationBuilder.DropColumn(
                name: "receiverAdress",
                table: "poolTransactions");

            migrationBuilder.DropColumn(
                name: "Daterefilled",
                table: "BitcoinPools");

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
                value: new DateTime(2024, 10, 3, 11, 4, 17, 349, DateTimeKind.Local).AddTicks(7909));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 10, 3, 11, 4, 17, 349, DateTimeKind.Local).AddTicks(7958));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 10, 3, 11, 4, 17, 349, DateTimeKind.Local).AddTicks(7963));

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
    }
}
