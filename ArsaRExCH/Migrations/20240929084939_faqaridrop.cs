using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class faqaridrop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "airDropFaqs",
                columns: table => new
                {
                    AirDropFaqId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_airDropFaqs", x => x.AirDropFaqId);
                });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 9, 29, 9, 49, 38, 160, DateTimeKind.Local).AddTicks(5800));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 9, 29, 9, 49, 38, 160, DateTimeKind.Local).AddTicks(5851));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 9, 29, 9, 49, 38, 160, DateTimeKind.Local).AddTicks(5855));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "airDropFaqs");

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
    }
}
