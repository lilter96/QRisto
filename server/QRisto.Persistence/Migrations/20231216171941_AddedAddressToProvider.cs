using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QRisto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedAddressToProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2d5dd6be-4a20-4a6a-acc8-dbc1a6ebebda"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3bde2bae-9623-42e7-9cf5-452e17bcea5f"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("32ad8282-8fb2-44e8-9d34-8442f44fa17b"), new Guid("26b24173-9e5e-4d26-99dc-b6057f6dac2a") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("32ad8282-8fb2-44e8-9d34-8442f44fa17b"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("26b24173-9e5e-4d26-99dc-b6057f6dac2a"));

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Providers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ChangedAt", "ConcurrencyStamp", "CreatedDate", "DeletedBy", "DeletedDate", "ModificationDate", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("c100382b-03b8-44e8-a889-87a9a2e66634"), new DateTime(2023, 12, 16, 17, 19, 41, 115, DateTimeKind.Utc).AddTicks(6018), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Default", "DEFAULT" },
                    { new Guid("f484aaa3-6e21-45f3-beb0-1ed2ec4f51dc"), new DateTime(2023, 12, 16, 17, 19, 41, 115, DateTimeKind.Utc).AddTicks(6015), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Provider", "PROVIDER" },
                    { new Guid("fbd1cd7a-eac2-4286-b5ce-643171139bdd"), new DateTime(2023, 12, 16, 17, 19, 41, 115, DateTimeKind.Utc).AddTicks(5993), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccessToken", "ConcurrencyStamp", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModificationDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("a7faffdc-89f6-4ddc-9837-806325aebe41"), 0, null, "ec7a0569-f9c7-40d3-99d5-3aae7399337e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "admin@example.com", true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAENfl0sGeSXHiPCGCNpiNWdnO6V6U+b0utlAjT5X3n/GEzIK/omOKhNUXJ4/SCFdtWw==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "c38820cd-0f27-4f61-ba92-6bdada4c373f", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("fbd1cd7a-eac2-4286-b5ce-643171139bdd"), new Guid("a7faffdc-89f6-4ddc-9837-806325aebe41") });

            migrationBuilder.CreateIndex(
                name: "IX_Providers_AddressId",
                table: "Providers",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_Addresses_AddressId",
                table: "Providers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Providers_Addresses_AddressId",
                table: "Providers");

            migrationBuilder.DropIndex(
                name: "IX_Providers_AddressId",
                table: "Providers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c100382b-03b8-44e8-a889-87a9a2e66634"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f484aaa3-6e21-45f3-beb0-1ed2ec4f51dc"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("fbd1cd7a-eac2-4286-b5ce-643171139bdd"), new Guid("a7faffdc-89f6-4ddc-9837-806325aebe41") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fbd1cd7a-eac2-4286-b5ce-643171139bdd"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a7faffdc-89f6-4ddc-9837-806325aebe41"));

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Providers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ChangedAt", "ConcurrencyStamp", "CreatedDate", "DeletedBy", "DeletedDate", "ModificationDate", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2d5dd6be-4a20-4a6a-acc8-dbc1a6ebebda"), new DateTime(2023, 12, 16, 16, 34, 45, 613, DateTimeKind.Utc).AddTicks(5180), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Default", "DEFAULT" },
                    { new Guid("32ad8282-8fb2-44e8-9d34-8442f44fa17b"), new DateTime(2023, 12, 16, 16, 34, 45, 613, DateTimeKind.Utc).AddTicks(5156), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "ADMIN" },
                    { new Guid("3bde2bae-9623-42e7-9cf5-452e17bcea5f"), new DateTime(2023, 12, 16, 16, 34, 45, 613, DateTimeKind.Utc).AddTicks(5177), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Provider", "PROVIDER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccessToken", "ConcurrencyStamp", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModificationDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("26b24173-9e5e-4d26-99dc-b6057f6dac2a"), 0, null, "1a7123c8-e371-482f-b376-c8c0ab2a2930", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), null, "admin@example.com", true, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEJXoOW0PT/f7PlYCe8zWjmyMDlD2pZm8V1itM5mYPi3hggjJjCbBNqXDUOVs5nyXGA==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2ad50813-d91e-4d27-a999-6f822a2cc1a0", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("32ad8282-8fb2-44e8-9d34-8442f44fa17b"), new Guid("26b24173-9e5e-4d26-99dc-b6057f6dac2a") });
        }
    }
}
