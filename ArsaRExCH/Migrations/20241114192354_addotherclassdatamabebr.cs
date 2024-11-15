using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class addotherclassdatamabebr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderTriggerdAt",
                table: "propTrdaes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "34dcdffc-17a1-46dc-9af5-cc0583f6481e", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d8015630-5782-43c7-bc88-a2c8cac93002", 0, "1bf38660-5e3d-472e-8345-b491ff79bc01", "ARMINTTWAT@GMAIL.COM", true, null, false, null, "ARMINTTWAT@GMAIL.COM", "ARMINTTWAT@GMAIL.COM", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "61c39ef5-0b3c-4421-82df-d3d7a8d46cc9", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 11, 14, 19, 23, 53, 272, DateTimeKind.Local).AddTicks(3974));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 11, 14, 19, 23, 53, 272, DateTimeKind.Local).AddTicks(4024));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 11, 14, 19, 23, 53, 272, DateTimeKind.Local).AddTicks(4028));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "34dcdffc-17a1-46dc-9af5-cc0583f6481e", "d8015630-5782-43c7-bc88-a2c8cac93002" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "34dcdffc-17a1-46dc-9af5-cc0583f6481e", "d8015630-5782-43c7-bc88-a2c8cac93002" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "34dcdffc-17a1-46dc-9af5-cc0583f6481e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d8015630-5782-43c7-bc88-a2c8cac93002");

            migrationBuilder.DropColumn(
                name: "OrderTriggerdAt",
                table: "propTrdaes");

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
    }
}
