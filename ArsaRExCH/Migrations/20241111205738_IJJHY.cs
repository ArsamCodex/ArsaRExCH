using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsaRExCH.Migrations
{
    /// <inheritdoc />
    public partial class IJJHY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7d48a4b4-b3fa-4b91-adc0-6cf3bc30e092", "916103ed-e5ac-44ee-98ce-bb3a38dc33c4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d48a4b4-b3fa-4b91-adc0-6cf3bc30e092");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "916103ed-e5ac-44ee-98ce-bb3a38dc33c4");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountForOrder",
                table: "propTrdaes",
                type: "decimal(18,8)",
                precision: 18,
                scale: 8,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)",
                oldPrecision: 18);

            migrationBuilder.AddColumn<int>(
                name: "orderTypeProp",
                table: "propTrdaes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PaiName",
                table: "Pair",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NetworkName",
                table: "Pair",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ffae1a40-479b-4318-8cad-2637fe2296ff", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6317d547-358c-410d-a7e9-fae514103231", 0, "45190970-ddef-4438-af2c-3e202b4629e7", "ARMINTTWAT@GMAIL.COM", true, null, false, null, "ARMINTTWAT@GMAIL.COM", "ARMINTTWAT@GMAIL.COM", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "04d0b4d9-db66-463c-b7be-bde057a0ff51", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 11, 11, 20, 57, 37, 544, DateTimeKind.Local).AddTicks(4994));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 11, 11, 20, 57, 37, 544, DateTimeKind.Local).AddTicks(5063));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 11, 11, 20, 57, 37, 544, DateTimeKind.Local).AddTicks(5070));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ffae1a40-479b-4318-8cad-2637fe2296ff", "6317d547-358c-410d-a7e9-fae514103231" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ffae1a40-479b-4318-8cad-2637fe2296ff", "6317d547-358c-410d-a7e9-fae514103231" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffae1a40-479b-4318-8cad-2637fe2296ff");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6317d547-358c-410d-a7e9-fae514103231");

            migrationBuilder.DropColumn(
                name: "orderTypeProp",
                table: "propTrdaes");

            migrationBuilder.AlterColumn<decimal>(
                name: "AmountForOrder",
                table: "propTrdaes",
                type: "decimal(18,0)",
                precision: 18,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,8)",
                oldPrecision: 18,
                oldScale: 8);

            migrationBuilder.AlterColumn<string>(
                name: "PaiName",
                table: "Pair",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NetworkName",
                table: "Pair",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7d48a4b4-b3fa-4b91-adc0-6cf3bc30e092", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "916103ed-e5ac-44ee-98ce-bb3a38dc33c4", 0, "20d696db-5e06-48be-a4f1-30183b20f616", "ARMINTTWAT@GMAIL.COM", true, null, false, null, "ARMINTTWAT@GMAIL.COM", "ARMINTTWAT@GMAIL.COM", "AQAAAAIAAYagAAAAEDUnZz/KjYxPuCxkRvVnTE9MIXt6Ffoo5LdJhV9qI7q2vqDUHQ6tBVrxE5+G+eYqPA==", null, false, "95e16a0a-06e6-4d69-84aa-c17a836f9192", false, "arminttwat@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 1,
                column: "ListedDate",
                value: new DateTime(2024, 11, 10, 22, 18, 34, 267, DateTimeKind.Local).AddTicks(8791));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 2,
                column: "ListedDate",
                value: new DateTime(2024, 11, 10, 22, 18, 34, 267, DateTimeKind.Local).AddTicks(8855));

            migrationBuilder.UpdateData(
                table: "Pair",
                keyColumn: "PairID",
                keyValue: 3,
                column: "ListedDate",
                value: new DateTime(2024, 11, 10, 22, 18, 34, 267, DateTimeKind.Local).AddTicks(8861));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7d48a4b4-b3fa-4b91-adc0-6cf3bc30e092", "916103ed-e5ac-44ee-98ce-bb3a38dc33c4" });
        }
    }
}
