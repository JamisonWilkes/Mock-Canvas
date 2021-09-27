using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class StreetAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "UserInfo",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "UserInfo",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "UserInfo",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "UserInfo",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "UserInfo",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "State",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "UserInfo");
        }
    }
}
