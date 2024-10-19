using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class listtrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trade_BitcoinPoolId",
                table: "Trade");

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 10, 19, 13, 46, 38, 746, DateTimeKind.Local).AddTicks(8258));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 10, 19, 13, 46, 38, 746, DateTimeKind.Local).AddTicks(8310));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 10, 19, 13, 46, 38, 746, DateTimeKind.Local).AddTicks(8314));

            migrationBuilder.CreateIndex(
                name: "IX_Trade_BitcoinPoolId",
                table: "Trade",
                column: "BitcoinPoolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trade_BitcoinPoolId",
                table: "Trade");

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 10, 19, 13, 32, 2, 269, DateTimeKind.Local).AddTicks(9670));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 10, 19, 13, 32, 2, 269, DateTimeKind.Local).AddTicks(9719));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 10, 19, 13, 32, 2, 269, DateTimeKind.Local).AddTicks(9722));

            migrationBuilder.CreateIndex(
                name: "IX_Trade_BitcoinPoolId",
                table: "Trade",
                column: "BitcoinPoolId",
                unique: true);
        }
    }
}
