using Microsoft.EntityFrameworkCore.Migrations;

namespace Vk_server.Migrations
{
    public partial class SizeModification2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "UserParameters");

            migrationBuilder.AlterColumn<double>(
                name: "Legs",
                table: "UserParameters",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Legs",
                table: "UserParameters",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<double>(
                name: "Length",
                table: "UserParameters",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
