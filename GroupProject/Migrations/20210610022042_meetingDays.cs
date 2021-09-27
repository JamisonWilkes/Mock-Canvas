using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class meetingDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department = table.Column<string>(maxLength: 30, nullable: false),
                    CourseTitle = table.Column<string>(maxLength: 30, nullable: false),
                    CourseNumber = table.Column<string>(maxLength: 6, nullable: false),
                    CourseDescription = table.Column<string>(maxLength: 500, nullable: false),
                    Credits = table.Column<int>(nullable: false),
                    MettingDaysTime = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Course");
        }
    }
}
