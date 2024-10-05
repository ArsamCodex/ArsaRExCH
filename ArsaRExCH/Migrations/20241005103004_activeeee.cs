using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class activeeee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Daterefilled",
                table: "BitcoinPools",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Daterefilled",
                table: "BitcoinPools",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

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
    }
}
