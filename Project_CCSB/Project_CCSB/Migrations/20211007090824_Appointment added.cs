using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_CCSB.Migrations
{
    public partial class Appointmentadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleLicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppointmentType = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => new { x.Date, x.Time, x.VehicleLicensePlate });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
