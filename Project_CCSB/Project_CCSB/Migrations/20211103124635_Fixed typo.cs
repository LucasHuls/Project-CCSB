using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_CCSB.Migrations
{
    public partial class Fixedtypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contrats_AspNetUsers_ApplicationUserId",
                table: "Contrats");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrats_Invoices_InvoiceNumber",
                table: "Contrats");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrats_Vehicles_VehicleLicensePlate",
                table: "Contrats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contrats",
                table: "Contrats");

            migrationBuilder.RenameTable(
                name: "Contrats",
                newName: "Contracts");

            migrationBuilder.RenameIndex(
                name: "IX_Contrats_VehicleLicensePlate",
                table: "Contracts",
                newName: "IX_Contracts_VehicleLicensePlate");

            migrationBuilder.RenameIndex(
                name: "IX_Contrats_InvoiceNumber",
                table: "Contracts",
                newName: "IX_Contracts_InvoiceNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Contrats_ApplicationUserId",
                table: "Contracts",
                newName: "IX_Contracts_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "ContractID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_AspNetUsers_ApplicationUserId",
                table: "Contracts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Invoices_InvoiceNumber",
                table: "Contracts",
                column: "InvoiceNumber",
                principalTable: "Invoices",
                principalColumn: "InvoiceNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Vehicles_VehicleLicensePlate",
                table: "Contracts",
                column: "VehicleLicensePlate",
                principalTable: "Vehicles",
                principalColumn: "LicensePlate",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_AspNetUsers_ApplicationUserId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Invoices_InvoiceNumber",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Vehicles_VehicleLicensePlate",
                table: "Contracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "Contrats");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_VehicleLicensePlate",
                table: "Contrats",
                newName: "IX_Contrats_VehicleLicensePlate");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_InvoiceNumber",
                table: "Contrats",
                newName: "IX_Contrats_InvoiceNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_ApplicationUserId",
                table: "Contrats",
                newName: "IX_Contrats_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contrats",
                table: "Contrats",
                column: "ContractID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contrats_AspNetUsers_ApplicationUserId",
                table: "Contrats",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrats_Invoices_InvoiceNumber",
                table: "Contrats",
                column: "InvoiceNumber",
                principalTable: "Invoices",
                principalColumn: "InvoiceNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrats_Vehicles_VehicleLicensePlate",
                table: "Contrats",
                column: "VehicleLicensePlate",
                principalTable: "Vehicles",
                principalColumn: "LicensePlate",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
