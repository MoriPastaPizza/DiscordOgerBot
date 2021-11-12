using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscordOgerBot.Migrations
{
    public partial class VlogCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VlogCount",
                table: "PersistentData",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VlogCount",
                table: "PersistentData");
        }
    }
}
