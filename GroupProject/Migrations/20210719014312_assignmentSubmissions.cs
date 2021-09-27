using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class assignmentSubmissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignmentSubmissions",
                columns: table => new
                {
                    SubmissionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmissionExists = table.Column<bool>(nullable: false),
                    SubmissionFilePath = table.Column<string>(nullable: true),
                    SubmissionName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    TimeOfSubmission = table.Column<DateTime>(nullable: false),
                    UserInfoID = table.Column<int>(nullable: false),
                    CourseID = table.Column<int>(nullable: false),
                    AssignmentID = table.Column<int>(nullable: false),
                    Grade = table.Column<int>(nullable: false),
                    AssignmentsAssignmentID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentSubmissions", x => x.SubmissionID);
                    table.ForeignKey(
                        name: "FK_AssignmentSubmissions_Assignments_AssignmentsAssignmentID",
                        column: x => x.AssignmentsAssignmentID,
                        principalTable: "Assignments",
                        principalColumn: "AssignmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignmentSubmissions_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AssignmentSubmissions_UserInfo_UserInfoID",
                        column: x => x.UserInfoID,
                        principalTable: "UserInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmissions_AssignmentsAssignmentID",
                table: "AssignmentSubmissions",
                column: "AssignmentsAssignmentID");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmissions_CourseID",
                table: "AssignmentSubmissions",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSubmissions_UserInfoID",
                table: "AssignmentSubmissions",
                column: "UserInfoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentSubmissions");
        }
    }
}
