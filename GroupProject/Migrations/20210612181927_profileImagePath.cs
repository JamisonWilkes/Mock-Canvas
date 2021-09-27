using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class profileImagePath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImageFilePath",
                table: "UserInfo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_UserInfoID",
                table: "Course",
                column: "UserInfoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_UserInfo_UserInfoID",
                table: "Course",
                column: "UserInfoID",
                principalTable: "UserInfo",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_UserInfo_UserInfoID",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_UserInfoID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "ProfileImageFilePath",
                table: "UserInfo");
        }
    }
}
