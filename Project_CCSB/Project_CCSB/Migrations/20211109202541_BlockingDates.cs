using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_CCSB.Migrations
{
    public partial class BlockingDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockedDates",
                columns: table => new
                {
                    SelectedDateToBeBlocked = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedDates", x => x.SelectedDateToBeBlocked);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockedDates");
        }
    }
}
