using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDomainProject.Migrations
{
    public partial class added_to_eventlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "after_image",
                table: "EventLogData",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "before_image",
                table: "EventLogData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "after_image",
                table: "EventLogData");

            migrationBuilder.DropColumn(
                name: "before_image",
                table: "EventLogData");
        }
    }
}
