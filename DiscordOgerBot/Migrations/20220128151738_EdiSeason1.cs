using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscordOgerBot.Migrations
{
    public partial class EdiSeason1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "EdiTimeOutTotalSeason0",
                table: "DiscordUsers",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EdiTimeOutTotalSeason1",
                table: "DiscordUsers",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<long>(
                name: "EdiTimeTillUnlock",
                table: "DiscordUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EdiTimeOutTotalSeason0",
                table: "DiscordUsers");

            migrationBuilder.DropColumn(
                name: "EdiTimeOutTotalSeason1",
                table: "DiscordUsers");

            migrationBuilder.DropColumn(
                name: "EdiTimeTillUnlock",
                table: "DiscordUsers");
        }
    }
}
