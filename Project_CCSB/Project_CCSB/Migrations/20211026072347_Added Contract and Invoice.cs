using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_CCSB.Migrations
{
    public partial class AddedContractandInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceNumber);
                });

            migrationBuilder.CreateTable(
                name: "Contrats",
                columns: table => new
                {
                    ContractID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VehicleLicensePlate = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: true),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrats", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK_Contrats_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contrats_Invoices_InvoiceNumber",
                        column: x => x.InvoiceNumber,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contrats_Vehicles_VehicleLicensePlate",
                        column: x => x.VehicleLicensePlate,
                        principalTable: "Vehicles",
                        principalColumn: "LicensePlate",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contrats_ApplicationUserId",
                table: "Contrats",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrats_InvoiceNumber",
                table: "Contrats",
                column: "InvoiceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Contrats_VehicleLicensePlate",
                table: "Contrats",
                column: "VehicleLicensePlate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contrats");

            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
