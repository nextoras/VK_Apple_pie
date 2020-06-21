using Microsoft.EntityFrameworkCore.Migrations;

namespace Vk_server.Migrations
{
    public partial class HeightAndCoordinatesRelocate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "PhotoHumans");

            migrationBuilder.AddColumn<long>(
                name: "Height",
                table: "Users",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "PartsCoordinates",
                table: "UserParameters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PartsCoordinates",
                table: "UserParameters");

            migrationBuilder.AddColumn<string>(
                name: "Coordinates",
                table: "PhotoHumans",
                nullable: true);
        }
    }
}
