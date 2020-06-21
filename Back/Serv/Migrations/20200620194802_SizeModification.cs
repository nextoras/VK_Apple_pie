using Microsoft.EntityFrameworkCore.Migrations;

namespace Vk_server.Migrations
{
    public partial class SizeModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropColumn(
                name: "Foots",
                table: "UserParameters");

            migrationBuilder.DropColumn(
                name: "Pelvic",
                table: "UserParameters");

            migrationBuilder.DropColumn(
                name: "SizeN",
                table: "Sizes");

            migrationBuilder.RenameColumn(
                name: "Shulders",
                table: "UserParameters",
                newName: "Length");

            migrationBuilder.AlterColumn<long>(
                name: "Legs",
                table: "UserParameters",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<long>(
                name: "SizeDownId",
                table: "UserParameters",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SizeMiddleId",
                table: "UserParameters",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SizeUpId",
                table: "UserParameters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SizesType",
                table: "Sizes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserParameters_SizeDownId",
                table: "UserParameters",
                column: "SizeDownId");

            migrationBuilder.CreateIndex(
                name: "IX_UserParameters_SizeMiddleId",
                table: "UserParameters",
                column: "SizeMiddleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserParameters_SizeUpId",
                table: "UserParameters",
                column: "SizeUpId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserParameters_Sizes_SizeDownId",
                table: "UserParameters",
                column: "SizeDownId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserParameters_Sizes_SizeMiddleId",
                table: "UserParameters",
                column: "SizeMiddleId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserParameters_Sizes_SizeUpId",
                table: "UserParameters",
                column: "SizeUpId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserParameters_Sizes_SizeDownId",
                table: "UserParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_UserParameters_Sizes_SizeMiddleId",
                table: "UserParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_UserParameters_Sizes_SizeUpId",
                table: "UserParameters");

            migrationBuilder.DropIndex(
                name: "IX_UserParameters_SizeDownId",
                table: "UserParameters");

            migrationBuilder.DropIndex(
                name: "IX_UserParameters_SizeMiddleId",
                table: "UserParameters");

            migrationBuilder.DropIndex(
                name: "IX_UserParameters_SizeUpId",
                table: "UserParameters");

            migrationBuilder.DropColumn(
                name: "SizeDownId",
                table: "UserParameters");

            migrationBuilder.DropColumn(
                name: "SizeMiddleId",
                table: "UserParameters");

            migrationBuilder.DropColumn(
                name: "SizeUpId",
                table: "UserParameters");

            migrationBuilder.DropColumn(
                name: "SizesType",
                table: "Sizes");

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "UserParameters",
                newName: "Shulders");

            migrationBuilder.AlterColumn<double>(
                name: "Legs",
                table: "UserParameters",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Foots",
                table: "UserParameters",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Pelvic",
                table: "UserParameters",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SizeN",
                table: "Sizes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    ClothingId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => new { x.UserId, x.ClothingId });
                    table.ForeignKey(
                        name: "FK_Baskets_Clothings_ClothingId",
                        column: x => x.ClothingId,
                        principalTable: "Clothings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Baskets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_ClothingId",
                table: "Baskets",
                column: "ClothingId");
        }
    }
}
