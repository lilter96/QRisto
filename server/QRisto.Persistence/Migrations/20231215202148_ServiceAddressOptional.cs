using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QRisto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ServiceAddressOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Addresses_AddressId",
                table: "Services");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Addresses_AddressId",
                table: "Services",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Addresses_AddressId",
                table: "Services");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Addresses_AddressId",
                table: "Services",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
