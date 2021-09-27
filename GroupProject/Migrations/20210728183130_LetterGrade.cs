using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class LetterGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LetterGrade",
                table: "StudentRegistration",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LetterGrade",
                table: "StudentRegistration");
        }
    }
}
