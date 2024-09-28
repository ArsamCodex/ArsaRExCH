using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class Dailycounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DailyClickCount",
                table: "AirDrops",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastClickDate",
                table: "AirDrops",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 9, 28, 23, 53, 38, 493, DateTimeKind.Local).AddTicks(1206));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 9, 28, 23, 53, 38, 493, DateTimeKind.Local).AddTicks(1255));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 9, 28, 23, 53, 38, 493, DateTimeKind.Local).AddTicks(1258));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyClickCount",
                table: "AirDrops");

            migrationBuilder.DropColumn(
                name: "LastClickDate",
                table: "AirDrops");

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 9, 28, 14, 36, 34, 844, DateTimeKind.Local).AddTicks(9519));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 9, 28, 14, 36, 34, 844, DateTimeKind.Local).AddTicks(9568));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 9, 28, 14, 36, 34, 844, DateTimeKind.Local).AddTicks(9574));
        }
    }
}
