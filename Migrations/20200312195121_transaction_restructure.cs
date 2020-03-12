using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDomainProject.Migrations
{
    public partial class transaction_restructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "TransactionData");

            migrationBuilder.AddColumn<double>(
                name: "Credits",
                table: "TransactionData",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Debits",
                table: "TransactionData",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credits",
                table: "TransactionData");

            migrationBuilder.DropColumn(
                name: "Debits",
                table: "TransactionData");

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "TransactionData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
