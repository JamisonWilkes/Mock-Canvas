using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class meetingDaysTimes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MettingDaysTime",
                table: "Course");

            migrationBuilder.AddColumn<string>(
                name: "MeetingDayOne",
                table: "Course",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MeetingDayThree",
                table: "Course",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MeetingDayTwo",
                table: "Course",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "MeetingTime",
                table: "Course",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeetingDayOne",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "MeetingDayThree",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "MeetingDayTwo",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "MeetingTime",
                table: "Course");

            migrationBuilder.AddColumn<string>(
                name: "MettingDaysTime",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
