using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class tradfeee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tradeFees",
                columns: table => new
                {
                    TradeFeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FeeInBtc = table.Column<double>(type: "float", nullable: false),
                    SetByAdminId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tradeFees", x => x.TradeFeeId);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tradeFees");

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 10, 17, 21, 44, 54, 683, DateTimeKind.Local).AddTicks(9152));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 10, 17, 21, 44, 54, 683, DateTimeKind.Local).AddTicks(9200));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 10, 17, 21, 44, 54, 683, DateTimeKind.Local).AddTicks(9203));
        }
    }
}
