using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class newCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImageFilePath",
                table: "UserInfo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuildingName",
                table: "Course",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageFilePath",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "BuildingName",
                table: "Course");
        }
    }
}
