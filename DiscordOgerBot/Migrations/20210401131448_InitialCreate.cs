using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscordOgerBot.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscordUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ActiveGuildsId = table.Column<List<string>>(type: "text[]", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TimesBotUsed = table.Column<long>(type: "bigint", nullable: false),
                    TimeSpendWorking = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscordUsers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscordUsers");
        }
    }
}
