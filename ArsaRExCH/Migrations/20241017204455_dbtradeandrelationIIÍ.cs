using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class dbtradeandrelationIIÍ : Migration
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

            migrationBuilder.DropColumn(
                name: "BitcoinPoolId",
                table: "poolTransactions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "poolTransactions");

            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    TradeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TradePair = table.Column<int>(type: "int", nullable: false),
                    dateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    symbolI = table.Column<double>(type: "float", nullable: false),
                    SymbolII = table.Column<double>(type: "float", nullable: false),
                    TradeFee = table.Column<double>(type: "float", nullable: false),
                    IsMarketBuy = table.Column<bool>(type: "bit", nullable: false),
                    IsTradeDone = table.Column<bool>(type: "bit", nullable: false),
                    MyProperty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BitcoinPoolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.TradeId);
                    table.ForeignKey(
                        name: "FK_Trade_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trade_BitcoinPools_BitcoinPoolId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Trade_ApplicationUserId",
                table: "Trade",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Trade_BitcoinPoolId",
                table: "Trade",
                column: "BitcoinPoolId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trade");

            migrationBuilder.AddColumn<int>(
                name: "BitcoinPoolId",
                table: "poolTransactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "poolTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 10, 13, 20, 0, 4, 492, DateTimeKind.Local).AddTicks(3678));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 10, 13, 20, 0, 4, 492, DateTimeKind.Local).AddTicks(3728));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 10, 13, 20, 0, 4, 492, DateTimeKind.Local).AddTicks(3732));

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
