using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class fixForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Course");

            migrationBuilder.AddColumn<int>(
                name: "UserInfoID",
                table: "Course",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserInfoID",
                table: "Course");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
