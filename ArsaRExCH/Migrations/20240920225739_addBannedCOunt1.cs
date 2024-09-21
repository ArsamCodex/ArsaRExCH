using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class addBannedCOunt1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0c8c72e9-66b6-47f5-96c9-59de63f31810", "8a2fe7b8-f25d-4bf1-b56d-c9def41d343b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c8c72e9-66b6-47f5-96c9-59de63f31810");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8a2fe7b8-f25d-4bf1-b56d-c9def41d343b");

            migrationBuilder.CreateTable(
                name: "BanedCountris",
                columns: table => new
                {
                    BanedCountriesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IpAdressToBann = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanedCountris", x => x.BanedCountriesId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4b09f9f6-9a2e-4ef6-801b-5ed584ef4e2b", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8b421155-cb7d-4e8e-be56-71023015be41", 0, "055b733f-fcd3-44cf-84f5-abbb6e85c1cd", "arminttwat@gmail.com", true, null, false, null, "NEWUSER@EXAMPLE.COM", "arminttwat@gmail.com", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "17898b6d-d0f8-423d-802c-534a3fd7f53d", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 9, 20, 23, 57, 38, 499, DateTimeKind.Local).AddTicks(9606));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 9, 20, 23, 57, 38, 499, DateTimeKind.Local).AddTicks(9654));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 9, 20, 23, 57, 38, 499, DateTimeKind.Local).AddTicks(9657));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4b09f9f6-9a2e-4ef6-801b-5ed584ef4e2b", "8b421155-cb7d-4e8e-be56-71023015be41" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BanedCountris");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4b09f9f6-9a2e-4ef6-801b-5ed584ef4e2b", "8b421155-cb7d-4e8e-be56-71023015be41" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4b09f9f6-9a2e-4ef6-801b-5ed584ef4e2b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b421155-cb7d-4e8e-be56-71023015be41");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0c8c72e9-66b6-47f5-96c9-59de63f31810", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8a2fe7b8-f25d-4bf1-b56d-c9def41d343b", 0, "830b7b79-fb56-4eee-a7da-0f9a365b4cc4", "arminttwat@gmail.com", true, null, false, null, "NEWUSER@EXAMPLE.COM", "arminttwat@gmail.com", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "fdba7acd-7919-4b30-8f94-e95d8ebf637a", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 9, 19, 22, 57, 59, 675, DateTimeKind.Local).AddTicks(6394));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 9, 19, 22, 57, 59, 675, DateTimeKind.Local).AddTicks(6452));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 9, 19, 22, 57, 59, 675, DateTimeKind.Local).AddTicks(6455));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "0c8c72e9-66b6-47f5-96c9-59de63f31810", "8a2fe7b8-f25d-4bf1-b56d-c9def41d343b" });
        }
    }
}
