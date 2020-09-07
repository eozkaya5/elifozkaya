using Microsoft.EntityFrameworkCore.Migrations;

namespace CreditBook.Migrations
{
    public partial class totall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fee",
                table: "Shoppings");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Customers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Customers");

            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                table: "Shoppings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
