using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QRisto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypesForWorkingInterval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a679542f-f1c2-4270-8dc0-9f9f65591d1d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d91f4f33-041d-4f08-b85c-8be637c40474"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f903ce82-e8f0-45e9-a29e-30d2237001dd"), new Guid("fc6b279f-6c4b-469e-a8ef-5617637a8c13") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f903ce82-e8f0-45e9-a29e-30d2237001dd"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("fc6b279f-6c4b-469e-a8ef-5617637a8c13"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ChangedAt", "ConcurrencyStamp", "CreatedDate", "ModificationDate", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("17bc30dc-8137-40b5-b337-ac2e3b810be0"), new DateTime(2023, 12, 15, 20, 38, 17, 784, DateTimeKind.Utc).AddTicks(8607), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Default", "DEFAULT" },
                    { new Guid("a652e256-5977-4ebc-8ac1-a17ed049d1b6"), new DateTime(2023, 12, 15, 20, 38, 17, 784, DateTimeKind.Utc).AddTicks(8592), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "ADMIN" },
                    { new Guid("fd241037-441b-422c-9985-c981aba67037"), new DateTime(2023, 12, 15, 20, 38, 17, 784, DateTimeKind.Utc).AddTicks(8605), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Provider", "PROVIDER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccessToken", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModificationDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("42bcfbc1-3c3b-4dbc-97c5-41d8ab5325da"), 0, null, "917c30bb-f5cf-459b-a378-deb5c39e8520", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEMQxZEQGNkBh8s2O0MB3Gl2Bk8M/PQajyIhKCkUeoU8XBOKOVhrQYEorqZ21Df6TBg==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "47040ff7-7559-4f37-8128-93361a2861cb", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("a652e256-5977-4ebc-8ac1-a17ed049d1b6"), new Guid("42bcfbc1-3c3b-4dbc-97c5-41d8ab5325da") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("17bc30dc-8137-40b5-b337-ac2e3b810be0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fd241037-441b-422c-9985-c981aba67037"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("a652e256-5977-4ebc-8ac1-a17ed049d1b6"), new Guid("42bcfbc1-3c3b-4dbc-97c5-41d8ab5325da") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a652e256-5977-4ebc-8ac1-a17ed049d1b6"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("42bcfbc1-3c3b-4dbc-97c5-41d8ab5325da"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ChangedAt", "ConcurrencyStamp", "CreatedDate", "ModificationDate", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a679542f-f1c2-4270-8dc0-9f9f65591d1d"), new DateTime(2023, 12, 15, 20, 21, 47, 983, DateTimeKind.Utc).AddTicks(3273), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Default", "DEFAULT" },
                    { new Guid("d91f4f33-041d-4f08-b85c-8be637c40474"), new DateTime(2023, 12, 15, 20, 21, 47, 983, DateTimeKind.Utc).AddTicks(3271), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Provider", "PROVIDER" },
                    { new Guid("f903ce82-e8f0-45e9-a29e-30d2237001dd"), new DateTime(2023, 12, 15, 20, 21, 47, 983, DateTimeKind.Utc).AddTicks(3260), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccessToken", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModificationDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("fc6b279f-6c4b-469e-a8ef-5617637a8c13"), 0, null, "7be0e562-d354-4713-ad5f-12faed8b5eca", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEESznNikfbTvQ9pKceBiyKaSritvQjeA1dxP1bxauWUjeejqIfNQA4jGWM6NilJ1Fw==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2510633c-ae7c-49fe-a537-6e9f4171f9e2", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("f903ce82-e8f0-45e9-a29e-30d2237001dd"), new Guid("fc6b279f-6c4b-469e-a8ef-5617637a8c13") });
        }
    }
}
