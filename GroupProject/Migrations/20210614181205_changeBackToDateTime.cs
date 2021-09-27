using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class changeBackToDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "MeetingStartTime",
                table: "Course",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MeetingEndTime",
                table: "Course",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "MeetingStartTime",
                table: "Course",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "MeetingEndTime",
                table: "Course",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
