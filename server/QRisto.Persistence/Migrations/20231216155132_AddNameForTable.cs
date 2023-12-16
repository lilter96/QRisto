using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QRisto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNameForTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ChangedAt", "ConcurrencyStamp", "CreatedDate", "ModificationDate", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0c2a8fc6-4f0c-4ec5-9bc1-b3815e6fedc6"), new DateTime(2023, 12, 16, 15, 51, 32, 868, DateTimeKind.Utc).AddTicks(763), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Provider", "PROVIDER" },
                    { new Guid("70ed385c-9b54-4538-b86c-6c61dffe6683"), new DateTime(2023, 12, 16, 15, 51, 32, 868, DateTimeKind.Utc).AddTicks(765), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Default", "DEFAULT" },
                    { new Guid("c2aaf788-2677-4c15-8d33-86296c344d6d"), new DateTime(2023, 12, 16, 15, 51, 32, 868, DateTimeKind.Utc).AddTicks(741), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccessToken", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModificationDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("1d43a04f-1f91-45b8-9289-9d8a646369ac"), 0, null, "361ec950-7edb-4d39-932b-bb5b346b8eae", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@example.com", true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEJDoCTc2iZxx3ILxb1aderWlSWkvmfrR3AkDTK1Zsa5WkInMJkg9QgdVdayWQJXP7A==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e5f278ca-6e24-4155-ba4b-d7574057170c", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("c2aaf788-2677-4c15-8d33-86296c344d6d"), new Guid("1d43a04f-1f91-45b8-9289-9d8a646369ac") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0c2a8fc6-4f0c-4ec5-9bc1-b3815e6fedc6"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("70ed385c-9b54-4538-b86c-6c61dffe6683"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c2aaf788-2677-4c15-8d33-86296c344d6d"), new Guid("1d43a04f-1f91-45b8-9289-9d8a646369ac") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c2aaf788-2677-4c15-8d33-86296c344d6d"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("1d43a04f-1f91-45b8-9289-9d8a646369ac"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tables");

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
    }
}
