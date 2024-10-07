using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class livevhatadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lifeChat",
                columns: table => new
                {
                    LiveChatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeAndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lifeChat", x => x.LiveChatId);
                    table.ForeignKey(
                        name: "FK_lifeChat_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_lifeChat_ApplicationUserId",
                table: "lifeChat",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lifeChat");

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
        }
    }
}
