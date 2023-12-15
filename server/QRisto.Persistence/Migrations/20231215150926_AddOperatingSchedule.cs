using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QRisto.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOperatingSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperatingSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    IsWorkingDay = table.Column<bool>(type: "bit", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperatingSchedules_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingIntervals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperatingScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingIntervals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingIntervals_OperatingSchedules_OperatingScheduleId",
                        column: x => x.OperatingScheduleId,
                        principalTable: "OperatingSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OperatingSchedules_ServiceId_Date",
                table: "OperatingSchedules",
                columns: new[] { "ServiceId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkingIntervals_OperatingScheduleId_StartTime_EndTime",
                table: "WorkingIntervals",
                columns: new[] { "OperatingScheduleId", "StartTime", "EndTime" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingIntervals");

            migrationBuilder.DropTable(
                name: "OperatingSchedules");
        }
    }
}
