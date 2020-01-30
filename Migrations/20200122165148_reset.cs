using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDomainProject.Migrations
{
    public partial class reset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoginData",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    SecurityAnswer1 = table.Column<string>(nullable: true),
                    SecurityAnswer2 = table.Column<string>(nullable: true),
                    SecurityAnswer3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PersonalInfoData",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DOB = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInfoData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserInfoData",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Class = table.Column<int>(nullable: false),
                    PasswordSetDate = table.Column<DateTime>(nullable: false),
                    PasswordExpirationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfoData", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginData");

            migrationBuilder.DropTable(
                name: "PersonalInfoData");

            migrationBuilder.DropTable(
                name: "UserInfoData");
        }
    }
}
