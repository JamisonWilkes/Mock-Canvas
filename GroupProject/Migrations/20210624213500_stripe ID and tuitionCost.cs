using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationHW1.Migrations
{
    public partial class stripeIDandtuitionCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripeID",
                table: "UserInfo",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Tuition",
                table: "UserInfo",
                nullable: false,
                defaultValue: 0.0);

 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "StripeID",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "Tuition",
                table: "UserInfo");
        }
    }
}
