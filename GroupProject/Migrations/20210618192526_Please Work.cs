using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class PleaseWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileTable");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    paymentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cardNumber = table.Column<string>(nullable: false),
                    nameOnCard = table.Column<string>(nullable: false),
                    cardExpiration = table.Column<DateTime>(nullable: false),
                    cvcNumber = table.Column<string>(nullable: false),
                    totalPayment = table.Column<string>(nullable: false),
                    totalTuition = table.Column<double>(nullable: false),
                    UserInfoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.paymentID);
                    table.ForeignKey(
                        name: "FK_Payments_UserInfo_UserInfoID",
                        column: x => x.UserInfoID,
                        principalTable: "UserInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "StudentRegistration",
                columns: table => new
                {
                    RegistrationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserInfoID = table.Column<int>(nullable: false),
                    CourseID = table.Column<int>(nullable: false),
                    TuitionCost = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRegistration", x => x.RegistrationID);
                    table.ForeignKey(
                        name: "FK_StudentRegistration_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentRegistration_UserInfo_UserInfoID",
                        column: x => x.UserInfoID,
                        principalTable: "UserInfo",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserInfoID",
                table: "Payments",
                column: "UserInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRegistration_CourseID",
                table: "StudentRegistration",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRegistration_UserInfoID",
                table: "StudentRegistration",
                column: "UserInfoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "StudentRegistration");

            migrationBuilder.CreateTable(
                name: "FileTable",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UploadedFile = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileTable", x => x.ID);
                });
        }
    }
}
