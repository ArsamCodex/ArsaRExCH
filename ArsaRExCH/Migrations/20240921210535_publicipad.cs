using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class publicipad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "UserIpAdressPublic",
                table: "UserDatesRecords",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c463f524-8512-4f9e-8652-da19e665ab76", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "93edfa94-56aa-4e9c-be17-1c64f00cb7eb", 0, "b71b99d3-b2ec-404f-a2a0-ca5eeca26d9c", "arminttwat@gmail.com", true, null, false, null, "NEWUSER@EXAMPLE.COM", "arminttwat@gmail.com", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "fa55b47d-7572-467e-b642-ecd9f9f8ff11", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 9, 21, 22, 5, 34, 131, DateTimeKind.Local).AddTicks(5923));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 9, 21, 22, 5, 34, 131, DateTimeKind.Local).AddTicks(5973));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 9, 21, 22, 5, 34, 131, DateTimeKind.Local).AddTicks(5976));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c463f524-8512-4f9e-8652-da19e665ab76", "93edfa94-56aa-4e9c-be17-1c64f00cb7eb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c463f524-8512-4f9e-8652-da19e665ab76", "93edfa94-56aa-4e9c-be17-1c64f00cb7eb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c463f524-8512-4f9e-8652-da19e665ab76");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "93edfa94-56aa-4e9c-be17-1c64f00cb7eb");

            migrationBuilder.DropColumn(
                name: "UserIpAdressPublic",
                table: "UserDatesRecords");

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
    }
}
