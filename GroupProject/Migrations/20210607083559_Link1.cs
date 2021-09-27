using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class Link1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link1",
                table: "UserInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link2",
                table: "UserInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link3",
                table: "UserInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link1",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "Link2",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "Link3",
                table: "UserInfo");
        }
    }
}
