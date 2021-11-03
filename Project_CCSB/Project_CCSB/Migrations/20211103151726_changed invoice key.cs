using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_CCSB.Migrations
{
    public partial class changedinvoicekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Invoices_InvoiceDate",
                table: "Contracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_InvoiceDate",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "InvoiceDate",
                table: "Contracts");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Contracts",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_InvoiceId",
                table: "Contracts",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Invoices_InvoiceId",
                table: "Contracts",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Invoices_InvoiceId",
                table: "Contracts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_InvoiceId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Contracts");

            migrationBuilder.AddColumn<DateTime>(
                name: "InvoiceDate",
                table: "Contracts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "InvoiceDate");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_InvoiceDate",
                table: "Contracts",
                column: "InvoiceDate");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Invoices_InvoiceDate",
                table: "Contracts",
                column: "InvoiceDate",
                principalTable: "Invoices",
                principalColumn: "InvoiceDate",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
