using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_CCSB.Migrations
{
    public partial class Invoicekeyschange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Invoices_InvoiceNumber",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Vehicles_VehicleLicensePlate",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_VehicleLicensePlate",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_InvoiceNumber",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "VehicleLicensePlate",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Contracts");

            migrationBuilder.AddColumn<decimal>(
                name: "InvoiceAmount",
                table: "Contracts",
                type: "decimal(18,4)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "Contracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                columns: new[] { "Amount", "InvoiceDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_InvoiceAmount_InvoiceDate",
                table: "Contracts",
                columns: new[] { "InvoiceAmount", "InvoiceDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Invoices_InvoiceAmount_InvoiceDate",
                table: "Contracts",
                columns: new[] { "InvoiceAmount", "InvoiceDate" },
                principalTable: "Invoices",
                principalColumns: new[] { "Amount", "InvoiceDate" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Invoices_InvoiceAmount_InvoiceDate",
                table: "Contracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_InvoiceAmount_InvoiceDate",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "InvoiceAmount",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceNumber",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "VehicleLicensePlate",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceNumber",
                table: "Contracts",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "InvoiceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_VehicleLicensePlate",
                table: "Invoices",
                column: "VehicleLicensePlate");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_InvoiceNumber",
                table: "Contracts",
                column: "InvoiceNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Invoices_InvoiceNumber",
                table: "Contracts",
                column: "InvoiceNumber",
                principalTable: "Invoices",
                principalColumn: "InvoiceNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Vehicles_VehicleLicensePlate",
                table: "Invoices",
                column: "VehicleLicensePlate",
                principalTable: "Vehicles",
                principalColumn: "LicensePlate",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
