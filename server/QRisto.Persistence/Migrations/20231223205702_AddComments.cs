using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRisto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("042a9760-e09b-4043-ae26-9c3eb5b693d5"),
                column: "ChangedAt",
                value: new DateTime(2023, 12, 23, 20, 57, 2, 654, DateTimeKind.Utc).AddTicks(2413));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6854d47e-833d-4b16-8298-8c5976315366"),
                column: "ChangedAt",
                value: new DateTime(2023, 12, 23, 20, 57, 2, 654, DateTimeKind.Utc).AddTicks(2429));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e63669b-8417-4b92-8268-44787689e272"),
                column: "ChangedAt",
                value: new DateTime(2023, 12, 23, 20, 57, 2, 654, DateTimeKind.Utc).AddTicks(2427));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("17043a77-6320-4401-9cae-d4197798e395"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "03531976-c849-42b3-8537-b68d8646bd67", "AQAAAAIAAYagAAAAEI+jbE5+Knqds3h54LwMnpPCpScelQNDfT/xgSOHyNnaFaRiCU03e2CrDAuLka9xmA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ServiceId",
                table: "Comments",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("042a9760-e09b-4043-ae26-9c3eb5b693d5"),
                column: "ChangedAt",
                value: new DateTime(2023, 12, 16, 19, 46, 10, 257, DateTimeKind.Utc).AddTicks(6983));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6854d47e-833d-4b16-8298-8c5976315366"),
                column: "ChangedAt",
                value: new DateTime(2023, 12, 16, 19, 46, 10, 257, DateTimeKind.Utc).AddTicks(6997));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e63669b-8417-4b92-8268-44787689e272"),
                column: "ChangedAt",
                value: new DateTime(2023, 12, 16, 19, 46, 10, 257, DateTimeKind.Utc).AddTicks(6995));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("17043a77-6320-4401-9cae-d4197798e395"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4cddb8c6-b85f-42de-8cf8-dbf157f2a73e", "AQAAAAIAAYagAAAAEB9LqvvZa5r7tLWX2hPq83oSb5MVhnCcNYSm+1VbqKNjI6uZSbSvIVf3ZoqgHiZT8g==" });
        }
    }
}
