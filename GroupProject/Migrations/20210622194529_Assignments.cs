using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebApplicationHW1.Migrations
{
    public partial class Assignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    AssignmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentTitle = table.Column<string>(nullable: false),
                    AssignmentDescription = table.Column<string>(nullable: false),
                    AssignmentDueDate = table.Column<DateTime>(nullable: false),
                    AssignmentMaxPoints = table.Column<int>(nullable: false),
                    CourseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.AssignmentID);
                    table.ForeignKey(
                        name: "FK_Assignments_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.NoAction);
                });

                migrationBuilder.CreateIndex(
                name: "IX_Assignments_CourseID",
                table: "Assignments",
                column: "CourseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
