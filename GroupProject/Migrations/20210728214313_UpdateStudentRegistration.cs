using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class UpdateStudentRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "StudentRegistration",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "StudentRegistration",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "StudentRegistration");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "StudentRegistration");
        }
    }
}
