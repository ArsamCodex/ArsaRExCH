using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class Addairdrop1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2e06ec6c-138c-4d73-a42a-2b774d085fc7", "23a44c43-bcc7-4a44-a0a4-f25da7c7089a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e06ec6c-138c-4d73-a42a-2b774d085fc7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "23a44c43-bcc7-4a44-a0a4-f25da7c7089a");

            migrationBuilder.CreateTable(
                name: "AirDrops",
                columns: table => new
                {
                    AirDropID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirDropBalance = table.Column<int>(type: "int", nullable: false),
                    TimeClick = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HowMannyClickInTottal = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirDrops", x => x.AirDropID);
                    table.ForeignKey(
                        name: "FK_AirDrops_AspNetUsers_ApplicationUserId",
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

            migrationBuilder.CreateIndex(
                name: "IX_AirDrops_ApplicationUserId",
                table: "AirDrops",
                column: "ApplicationUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirDrops");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2e06ec6c-138c-4d73-a42a-2b774d085fc7", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "23a44c43-bcc7-4a44-a0a4-f25da7c7089a", 0, "087002c0-2676-45cf-b666-0e09320f5d2a", "arminttwat@gmail.com", true, null, false, null, "NEWUSER@EXAMPLE.COM", "arminttwat@gmail.com", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "82f4b30a-14b1-4c8a-9a29-d29b3b03b180", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 9, 25, 14, 30, 14, 636, DateTimeKind.Local).AddTicks(1172));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 9, 25, 14, 30, 14, 636, DateTimeKind.Local).AddTicks(1235));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 9, 25, 14, 30, 14, 636, DateTimeKind.Local).AddTicks(1241));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2e06ec6c-138c-4d73-a42a-2b774d085fc7", "23a44c43-bcc7-4a44-a0a4-f25da7c7089a" });
        }
    }
}
