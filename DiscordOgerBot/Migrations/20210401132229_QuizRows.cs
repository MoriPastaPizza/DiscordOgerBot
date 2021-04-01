using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscordOgerBot.Migrations
{
    public partial class QuizRows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizPointsTotal",
                table: "DiscordUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuizWonTotal",
                table: "DiscordUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuizPointsTotal",
                table: "DiscordUsers");

            migrationBuilder.DropColumn(
                name: "QuizWonTotal",
                table: "DiscordUsers");
        }
    }
}
