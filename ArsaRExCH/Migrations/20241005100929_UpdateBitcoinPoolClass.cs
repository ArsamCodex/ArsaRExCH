using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBitcoinPoolClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminUserId",
                table: "BitcoinPools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPoolActive",
                table: "BitcoinPools",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PoolOpenedDate",
                table: "BitcoinPools",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SuspendDate",
                table: "BitcoinPools",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 10, 5, 11, 9, 28, 236, DateTimeKind.Local).AddTicks(5135));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 10, 5, 11, 9, 28, 236, DateTimeKind.Local).AddTicks(5180));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 10, 5, 11, 9, 28, 236, DateTimeKind.Local).AddTicks(5183));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminUserId",
                table: "BitcoinPools");

            migrationBuilder.DropColumn(
                name: "IsPoolActive",
                table: "BitcoinPools");

            migrationBuilder.DropColumn(
                name: "PoolOpenedDate",
                table: "BitcoinPools");

            migrationBuilder.DropColumn(
                name: "SuspendDate",
                table: "BitcoinPools");

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
        }
    }
}
