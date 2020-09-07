using Microsoft.EntityFrameworkCore.Migrations;

namespace CreditBook.Migrations
{
    public partial class total : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dept",
                table: "Customers",
                newName: "TotalShopping");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPayment",
                table: "Customers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPayment",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "TotalShopping",
                table: "Customers",
                newName: "Dept");
        }
    }
}
