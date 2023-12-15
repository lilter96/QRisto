using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QRisto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUserAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ChangedAt", "ConcurrencyStamp", "CreatedDate", "ModificationDate", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("71d683ef-0da0-4bdd-b04a-2ad1ff00045c"), new DateTime(2023, 12, 15, 20, 15, 31, 345, DateTimeKind.Utc).AddTicks(620), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Provider", "PROVIDER" },
                    { new Guid("e1f9254d-78f5-4ac9-b7be-e507a75b0a12"), new DateTime(2023, 12, 15, 20, 15, 31, 345, DateTimeKind.Utc).AddTicks(606), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "ADMIN" },
                    { new Guid("ff623023-9948-496e-8667-15077013f6af"), new DateTime(2023, 12, 15, 20, 15, 31, 345, DateTimeKind.Utc).AddTicks(622), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Default", "DEFAULT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccessToken", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModificationDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("be0b3b74-13ad-4888-97ef-c44429cb2114"), 0, null, "0e75aa0d-f6ad-49de-a8b4-79377598af5b", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEHrhQg4KyXbIOZxJPkAxM1sJmR5seLigvXTV5UKHxXl0e/MWo2wiBfh59IUeGou9dw==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "18f2f0ca-4c58-41ec-9c5f-022ddae729fb", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("e1f9254d-78f5-4ac9-b7be-e507a75b0a12"), new Guid("be0b3b74-13ad-4888-97ef-c44429cb2114") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("71d683ef-0da0-4bdd-b04a-2ad1ff00045c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ff623023-9948-496e-8667-15077013f6af"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e1f9254d-78f5-4ac9-b7be-e507a75b0a12"), new Guid("be0b3b74-13ad-4888-97ef-c44429cb2114") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e1f9254d-78f5-4ac9-b7be-e507a75b0a12"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("be0b3b74-13ad-4888-97ef-c44429cb2114"));
        }
    }
}
