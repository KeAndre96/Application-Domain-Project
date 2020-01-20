using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDomainProject.Migrations
{
    public partial class user_info : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Class",
                table: "LoginData",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LoginData",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserInfoData",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfoData", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfoData");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "LoginData");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LoginData");
        }
    }
}
