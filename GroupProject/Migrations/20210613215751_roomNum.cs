using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class roomNum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageFilePath",
                table: "UserInfo");

            migrationBuilder.AddColumn<string>(
                name: "RoomNumber",
                table: "Course",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomNumber",
                table: "Course");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageFilePath",
                table: "UserInfo",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
