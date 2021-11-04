using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_CCSB.Migrations
{
    public partial class Changedinvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VehicleLicensePlate",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_VehicleLicensePlate",
                table: "Invoices",
                column: "VehicleLicensePlate");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Vehicles_VehicleLicensePlate",
                table: "Invoices",
                column: "VehicleLicensePlate",
                principalTable: "Vehicles",
                principalColumn: "LicensePlate",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Vehicles_VehicleLicensePlate",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_VehicleLicensePlate",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "VehicleLicensePlate",
                table: "Invoices");
        }
    }
}
