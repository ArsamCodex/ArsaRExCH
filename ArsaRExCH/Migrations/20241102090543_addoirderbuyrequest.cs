using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class addoirderbuyrequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "56727184-0881-4f10-923f-84c11e77189f", "cbbc3868-178c-46ba-ba5e-4886e95499bb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56727184-0881-4f10-923f-84c11e77189f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cbbc3868-178c-46ba-ba5e-4886e95499bb");

            migrationBuilder.AddColumn<double>(
                name: "RequestPriceFOrOrderBuy",
                table: "Trade",
                type: "float",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3247bfbe-d7b2-42c9-ac2f-96895c080fd0", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "afa019cb-8de6-4f21-8f56-576bbe30edc4", 0, "d4707499-f2f9-4e96-b46e-defa09945a39", "ARMINTTWAT@GMAIL.COM", true, null, false, null, "ARMINTTWAT@GMAIL.COM", "ARMINTTWAT@GMAIL.COM", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "c21b69b1-69fd-44bc-822a-3588576b98f7", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 11, 2, 9, 5, 42, 160, DateTimeKind.Local).AddTicks(7325));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 11, 2, 9, 5, 42, 160, DateTimeKind.Local).AddTicks(7383));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 11, 2, 9, 5, 42, 160, DateTimeKind.Local).AddTicks(7388));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3247bfbe-d7b2-42c9-ac2f-96895c080fd0", "afa019cb-8de6-4f21-8f56-576bbe30edc4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3247bfbe-d7b2-42c9-ac2f-96895c080fd0", "afa019cb-8de6-4f21-8f56-576bbe30edc4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3247bfbe-d7b2-42c9-ac2f-96895c080fd0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "afa019cb-8de6-4f21-8f56-576bbe30edc4");

            migrationBuilder.DropColumn(
                name: "RequestPriceFOrOrderBuy",
                table: "Trade");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "56727184-0881-4f10-923f-84c11e77189f", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "cbbc3868-178c-46ba-ba5e-4886e95499bb", 0, "b9807492-0e0f-4d7c-a62f-7d668263beaf", "ARMINTTWAT@GMAIL.COM", true, null, false, null, "ARMINTTWAT@GMAIL.COM", "ARMINTTWAT@GMAIL.COM", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "8cd5dc00-70ce-4f17-9853-3c838325d4d2", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 11, 1, 19, 45, 24, 814, DateTimeKind.Local).AddTicks(3282));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 11, 1, 19, 45, 24, 814, DateTimeKind.Local).AddTicks(3328));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 11, 1, 19, 45, 24, 814, DateTimeKind.Local).AddTicks(3332));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "56727184-0881-4f10-923f-84c11e77189f", "cbbc3868-178c-46ba-ba5e-4886e95499bb" });
        }
    }
}
