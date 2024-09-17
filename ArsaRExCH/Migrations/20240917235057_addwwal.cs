using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class addwwal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "174c8a32-540a-4139-bab8-f33f2fe6d0e3", "b1e23cad-3ba7-4e89-a60f-d46d0a90ddad" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "174c8a32-540a-4139-bab8-f33f2fe6d0e3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b1e23cad-3ba7-4e89-a60f-d46d0a90ddad");

            migrationBuilder.AddColumn<double>(
                name: "WiningAmountEth",
                table: "Bet",
                type: "float",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "29eb7a40-9280-4069-a600-b86e64e4f149", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "cf8c84c2-9653-4d9e-82cb-0430c5f45e14", 0, "19e977b4-061b-4f68-b799-85ce459f88ef", "arminttwat@gmail.com", true, false, null, "NEWUSER@EXAMPLE.COM", "arminttwat@gmail.com", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "cf500a6a-3d60-4b07-945b-87cb62aabc38", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 9, 18, 0, 50, 56, 317, DateTimeKind.Local).AddTicks(1843));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 9, 18, 0, 50, 56, 317, DateTimeKind.Local).AddTicks(1902));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 9, 18, 0, 50, 56, 317, DateTimeKind.Local).AddTicks(1907));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "29eb7a40-9280-4069-a600-b86e64e4f149", "cf8c84c2-9653-4d9e-82cb-0430c5f45e14" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "29eb7a40-9280-4069-a600-b86e64e4f149", "cf8c84c2-9653-4d9e-82cb-0430c5f45e14" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29eb7a40-9280-4069-a600-b86e64e4f149");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cf8c84c2-9653-4d9e-82cb-0430c5f45e14");

            migrationBuilder.DropColumn(
                name: "WiningAmountEth",
                table: "Bet");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "174c8a32-540a-4139-bab8-f33f2fe6d0e3", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b1e23cad-3ba7-4e89-a60f-d46d0a90ddad", 0, "7deb6170-c2f7-4d47-91b8-c7b93deef350", "arminttwat@gmail.com", true, false, null, "NEWUSER@EXAMPLE.COM", "arminttwat@gmail.com", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "da11d34e-4bf2-40ea-9c0b-a82e4bfd2101", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 9, 16, 21, 18, 26, 810, DateTimeKind.Local).AddTicks(4075));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 9, 16, 21, 18, 26, 810, DateTimeKind.Local).AddTicks(4132));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 9, 16, 21, 18, 26, 810, DateTimeKind.Local).AddTicks(4136));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "174c8a32-540a-4139-bab8-f33f2fe6d0e3", "b1e23cad-3ba7-4e89-a60f-d46d0a90ddad" });
        }
    }
}
