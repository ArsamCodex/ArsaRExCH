using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class Triggerorderprivced : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "879767f9-c499-4a12-8825-0278defcf093", "3fee5bb3-e366-4cd8-a454-61ebbb266b10" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "879767f9-c499-4a12-8825-0278defcf093");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3fee5bb3-e366-4cd8-a454-61ebbb266b10");

            migrationBuilder.AddColumn<decimal>(
                name: "OrderPriceTriggerd",
                table: "propTrdaes",
                type: "decimal(18,8)",
                precision: 18,
                scale: 8,
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a5dae255-3662-4b9c-92b1-50cc629026d9", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ff3311f8-cb24-4d2b-b2e9-890d47619fae", 0, "a671dcd1-b726-4f32-9f50-1aba056b4c05", "ARMINTTWAT@GMAIL.COM", true, null, false, null, "ARMINTTWAT@GMAIL.COM", "ARMINTTWAT@GMAIL.COM", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "646f86ad-eb26-490d-9fee-3581fafbb7c7", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 11, 14, 16, 51, 31, 240, DateTimeKind.Local).AddTicks(5383));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 11, 14, 16, 51, 31, 240, DateTimeKind.Local).AddTicks(5466));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 11, 14, 16, 51, 31, 240, DateTimeKind.Local).AddTicks(5474));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a5dae255-3662-4b9c-92b1-50cc629026d9", "ff3311f8-cb24-4d2b-b2e9-890d47619fae" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a5dae255-3662-4b9c-92b1-50cc629026d9", "ff3311f8-cb24-4d2b-b2e9-890d47619fae" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a5dae255-3662-4b9c-92b1-50cc629026d9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ff3311f8-cb24-4d2b-b2e9-890d47619fae");

            migrationBuilder.DropColumn(
                name: "OrderPriceTriggerd",
                table: "propTrdaes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "879767f9-c499-4a12-8825-0278defcf093", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3fee5bb3-e366-4cd8-a454-61ebbb266b10", 0, "2f1cf191-a6cb-4cbc-9032-52a580060d5a", "ARMINTTWAT@GMAIL.COM", true, null, false, null, "ARMINTTWAT@GMAIL.COM", "ARMINTTWAT@GMAIL.COM", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "111ef492-fe96-4041-98ee-6f606bf3a660", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 11, 13, 20, 33, 29, 547, DateTimeKind.Local).AddTicks(6109));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 11, 13, 20, 33, 29, 547, DateTimeKind.Local).AddTicks(6159));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 11, 13, 20, 33, 29, 547, DateTimeKind.Local).AddTicks(6162));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "879767f9-c499-4a12-8825-0278defcf093", "3fee5bb3-e366-4cd8-a454-61ebbb266b10" });
        }
    }
}
