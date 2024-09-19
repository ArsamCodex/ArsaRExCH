using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class initii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "56791f32-8bde-43d4-aa4f-bdbeeef9e77f", "b4aa59ab-fc89-48da-979c-c9657a65c3a7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56791f32-8bde-43d4-aa4f-bdbeeef9e77f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b4aa59ab-fc89-48da-979c-c9657a65c3a7");

            migrationBuilder.RenameColumn(
                name: "UserIpAdress",
                table: "UserDatesRecords",
                newName: "UserIpAdressX");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "UserIpAdressX",
                table: "UserDatesRecords",
                newName: "UserIpAdress");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "56791f32-8bde-43d4-aa4f-bdbeeef9e77f", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b4aa59ab-fc89-48da-979c-c9657a65c3a7", 0, "a133f505-4d0a-43b1-9f2f-5c138836948f", "arminttwat@gmail.com", true, null, false, null, "NEWUSER@EXAMPLE.COM", "arminttwat@gmail.com", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "31c9d6ea-49bb-462a-bc56-f82d02e9b466", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 9, 19, 22, 55, 37, 992, DateTimeKind.Local).AddTicks(7198));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 9, 19, 22, 55, 37, 992, DateTimeKind.Local).AddTicks(7247));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 9, 19, 22, 55, 37, 992, DateTimeKind.Local).AddTicks(7252));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "56791f32-8bde-43d4-aa4f-bdbeeef9e77f", "b4aa59ab-fc89-48da-979c-c9657a65c3a7" });
        }
    }
}
