using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_CCSB.Migrations
{
    public partial class Changedincoivekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<decimal>(
                name: "InvoiceAmount",
                table: "Contracts",
                type: "decimal(18,4)",
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
    }
}
