using Microsoft.EntityFrameworkCore.Migrations;

namespace AppDomainProject.Migrations
{
    public partial class journal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "TransactionData");

            migrationBuilder.AlterColumn<int>(
                name: "Journal",
                table: "TransactionData",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "JournalData",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usetID = table.Column<string>(nullable: true),
                    JournalStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalData", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalData");

            migrationBuilder.AlterColumn<string>(
                name: "Journal",
                table: "TransactionData",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "TransactionData",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
