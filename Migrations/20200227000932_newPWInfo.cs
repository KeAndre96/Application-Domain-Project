using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDomainProject.Migrations
{
    public partial class newPWInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Attempts",
                table: "LoginData",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SecurityQuestion1",
                table: "LoginData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityQuestion2",
                table: "LoginData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityQuestion3",
                table: "LoginData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attempts",
                table: "LoginData");

            migrationBuilder.DropColumn(
                name: "SecurityQuestion1",
                table: "LoginData");

            migrationBuilder.DropColumn(
                name: "SecurityQuestion2",
                table: "LoginData");

            migrationBuilder.DropColumn(
                name: "SecurityQuestion3",
                table: "LoginData");
        }
    }
}
