using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class innn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDatesRecord_AspNetUsers_ApplicationUserId",
                table: "UserDatesRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDatesRecord",
                table: "UserDatesRecord");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "56d2f9d7-4eef-4fcc-b068-7f195daae945", "6ed10cbf-525f-4c01-aa41-b2fc3992d96a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56d2f9d7-4eef-4fcc-b068-7f195daae945");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ed10cbf-525f-4c01-aa41-b2fc3992d96a");

            migrationBuilder.RenameTable(
                name: "UserDatesRecord",
                newName: "UserDatesRecords");

            migrationBuilder.RenameIndex(
                name: "IX_UserDatesRecord_ApplicationUserId",
                table: "UserDatesRecords",
                newName: "IX_UserDatesRecords_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDatesRecords",
                table: "UserDatesRecords",
                column: "UserDatesRecordId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ad016f4f-3058-4c61-8933-9834d24310eb", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8bb9a9e0-a2fa-4ea2-9465-3eaa8ed42699", 0, "1860e87b-992a-4365-82db-12fc64dff8da", "arminttwat@gmail.com", true, null, false, null, "NEWUSER@EXAMPLE.COM", "arminttwat@gmail.com", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "edbdffab-fa61-40c9-b553-a8a53d379dce", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 9, 19, 21, 22, 31, 715, DateTimeKind.Local).AddTicks(2838));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 9, 19, 21, 22, 31, 715, DateTimeKind.Local).AddTicks(2899));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 9, 19, 21, 22, 31, 715, DateTimeKind.Local).AddTicks(2908));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ad016f4f-3058-4c61-8933-9834d24310eb", "8bb9a9e0-a2fa-4ea2-9465-3eaa8ed42699" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserDatesRecords_AspNetUsers_ApplicationUserId",
                table: "UserDatesRecords",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDatesRecords_AspNetUsers_ApplicationUserId",
                table: "UserDatesRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDatesRecords",
                table: "UserDatesRecords");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ad016f4f-3058-4c61-8933-9834d24310eb", "8bb9a9e0-a2fa-4ea2-9465-3eaa8ed42699" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad016f4f-3058-4c61-8933-9834d24310eb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8bb9a9e0-a2fa-4ea2-9465-3eaa8ed42699");

            migrationBuilder.RenameTable(
                name: "UserDatesRecords",
                newName: "UserDatesRecord");

            migrationBuilder.RenameIndex(
                name: "IX_UserDatesRecords_ApplicationUserId",
                table: "UserDatesRecord",
                newName: "IX_UserDatesRecord_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDatesRecord",
                table: "UserDatesRecord",
                column: "UserDatesRecordId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "56d2f9d7-4eef-4fcc-b068-7f195daae945", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6ed10cbf-525f-4c01-aa41-b2fc3992d96a", 0, "2ecc642e-c0bd-4955-90ed-79307bc18ce0", "arminttwat@gmail.com", true, null, false, null, "NEWUSER@EXAMPLE.COM", "arminttwat@gmail.com", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "f4cade14-e503-41d5-b6c2-76fdfa24fa4f", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 9, 19, 21, 18, 22, 337, DateTimeKind.Local).AddTicks(6160));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 9, 19, 21, 18, 22, 337, DateTimeKind.Local).AddTicks(6208));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 9, 19, 21, 18, 22, 337, DateTimeKind.Local).AddTicks(6211));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "56d2f9d7-4eef-4fcc-b068-7f195daae945", "6ed10cbf-525f-4c01-aa41-b2fc3992d96a" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserDatesRecord_AspNetUsers_ApplicationUserId",
                table: "UserDatesRecord",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
