using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class fixNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeetinEndTime",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "MeetinStartTime",
                table: "Course");

            migrationBuilder.AddColumn<DateTime>(
                name: "MeetingEndTime",
                table: "Course",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MeetingStartTime",
                table: "Course",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeetingEndTime",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "MeetingStartTime",
                table: "Course");

            migrationBuilder.AddColumn<DateTime>(
                name: "MeetinEndTime",
                table: "Course",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MeetinStartTime",
                table: "Course",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
