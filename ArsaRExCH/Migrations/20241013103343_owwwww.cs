using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class owwwww : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adminWarningMessages",
                columns: table => new
                {
                    AdminWarningMessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adminWarningMessages", x => x.AdminWarningMessageId);
                });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 10, 13, 11, 33, 42, 298, DateTimeKind.Local).AddTicks(9523));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 10, 13, 11, 33, 42, 298, DateTimeKind.Local).AddTicks(9570));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 10, 13, 11, 33, 42, 298, DateTimeKind.Local).AddTicks(9573));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adminWarningMessages");

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 10, 7, 23, 54, 5, 526, DateTimeKind.Local).AddTicks(1081));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 10, 7, 23, 54, 5, 526, DateTimeKind.Local).AddTicks(1125));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 10, 7, 23, 54, 5, 526, DateTimeKind.Local).AddTicks(1128));
        }
    }
}
