using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class changetutionname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalTuition",
                table: "Payments");

            migrationBuilder.AddColumn<double>(
                name: "TuitionBalance",
                table: "Payments",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TuitionBalance",
                table: "Payments");

            migrationBuilder.AddColumn<double>(
                name: "totalTuition",
                table: "Payments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
