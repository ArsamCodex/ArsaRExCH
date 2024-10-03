using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class bitpooll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTradeActivities");

            migrationBuilder.CreateTable(
                name: "BitcoinPools",
                columns: table => new
                {
                    BitcoinPoolId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PoolName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoolCurrentBalance = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BitcoinPools", x => x.BitcoinPoolId);
                });

            migrationBuilder.CreateTable(
                name: "poolTransactions",
                columns: table => new
                {
                    BitcoinPoolTransactionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BitcoinPoolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_poolTransactions", x => x.BitcoinPoolTransactionsId);
                    table.ForeignKey(
                        name: "FK_poolTransactions_BitcoinPools_BitcoinPoolId",
                        column: x => x.BitcoinPoolId,
                        principalTable: "BitcoinPools",
                        principalColumn: "BitcoinPoolId",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "poolTransactions");

            migrationBuilder.DropTable(
                name: "BitcoinPools");

            migrationBuilder.CreateTable(
                name: "UserTradeActivities",
                columns: table => new
                {
                    UserTradeActivityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentBalance = table.Column<double>(type: "float", nullable: false),
                    PairName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTradeActivities", x => x.UserTradeActivityID);
                });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 10, 1, 21, 1, 18, 264, DateTimeKind.Local).AddTicks(146));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 10, 1, 21, 1, 18, 264, DateTimeKind.Local).AddTicks(222));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 10, 1, 21, 1, 18, 264, DateTimeKind.Local).AddTicks(228));
        }
    }
}
