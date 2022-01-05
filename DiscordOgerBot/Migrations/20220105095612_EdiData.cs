using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscordOgerBot.Migrations
{
    public partial class EdiData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EdiSuccessfull",
                table: "DiscordUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EdiTimeOutTotal",
                table: "DiscordUsers",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "EdiUsed",
                table: "DiscordUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EdiSuccessfull",
                table: "DiscordUsers");

            migrationBuilder.DropColumn(
                name: "EdiTimeOutTotal",
                table: "DiscordUsers");

            migrationBuilder.DropColumn(
                name: "EdiUsed",
                table: "DiscordUsers");
        }
    }
}
