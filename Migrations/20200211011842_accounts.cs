using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDomainProject.Migrations
{
    public partial class accounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountData",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(nullable: false),
                    AccountName = table.Column<string>(nullable: true),
                    AccountDescription = table.Column<string>(nullable: true),
                    NormalSide = table.Column<string>(nullable: true),
                    AccountCategory = table.Column<string>(nullable: true),
                    AccountSubCategory = table.Column<string>(nullable: true),
                    InitialBalance = table.Column<double>(nullable: false),
                    Debit = table.Column<string>(nullable: true),
                    Credit = table.Column<string>(nullable: true),
                    Balance = table.Column<double>(nullable: false),
                    TimeAccountAdded = table.Column<DateTime>(nullable: false),
                    ID = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Statement = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountData", x => x.AccountNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountData");
        }
    }
}
