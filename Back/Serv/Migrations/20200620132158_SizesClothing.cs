using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Vk_server.Migrations
{
    public partial class SizesClothing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothings_ClothingSizes_ClothingSizeId",
                table: "Clothings");

            migrationBuilder.DropForeignKey(
                name: "FK_Sizes_Users_UserId",
                table: "Sizes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Sizes_SizeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_SizeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Sizes_UserId",
                table: "Sizes");

            migrationBuilder.DropIndex(
                name: "IX_Clothings_ClothingSizeId",
                table: "Clothings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClothingSizes",
                table: "ClothingSizes");

            migrationBuilder.DropColumn(
                name: "Chest",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "Foots",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "Hips",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "Legs",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "Pelvic",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "Shulders",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "Waist",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "ClothingSizeId",
                table: "Clothings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ClothingSizes");

            migrationBuilder.DropColumn(
                name: "SizeN",
                table: "ClothingSizes");

            migrationBuilder.DropColumn(
                name: "SizeName",
                table: "ClothingSizes");

            migrationBuilder.RenameTable(
                name: "ClothingSizes",
                newName: "ClothingSize");

            migrationBuilder.AddColumn<long>(
                name: "UserParameterId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SizeN",
                table: "Sizes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SizeName",
                table: "Sizes",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ClothingId",
                table: "ClothingSize",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SizeId",
                table: "ClothingSize",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClothingSize",
                table: "ClothingSize",
                columns: new[] { "ClothingId", "SizeId" });

            migrationBuilder.CreateTable(
                name: "UserParameters",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Chest = table.Column<double>(nullable: false),
                    Waist = table.Column<double>(nullable: false),
                    Hips = table.Column<double>(nullable: false),
                    Shulders = table.Column<double>(nullable: false),
                    Pelvic = table.Column<double>(nullable: false),
                    Legs = table.Column<double>(nullable: false),
                    Foots = table.Column<double>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserParameters_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserParameterId",
                table: "Users",
                column: "UserParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothingSize_SizeId",
                table: "ClothingSize",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserParameters_UserId",
                table: "UserParameters",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothingSize_Clothings_ClothingId",
                table: "ClothingSize",
                column: "ClothingId",
                principalTable: "Clothings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClothingSize_Sizes_SizeId",
                table: "ClothingSize",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserParameters_UserParameterId",
                table: "Users",
                column: "UserParameterId",
                principalTable: "UserParameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClothingSize_Clothings_ClothingId",
                table: "ClothingSize");

            migrationBuilder.DropForeignKey(
                name: "FK_ClothingSize_Sizes_SizeId",
                table: "ClothingSize");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserParameters_UserParameterId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserParameters");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserParameterId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClothingSize",
                table: "ClothingSize");

            migrationBuilder.DropIndex(
                name: "IX_ClothingSize_SizeId",
                table: "ClothingSize");

            migrationBuilder.DropColumn(
                name: "UserParameterId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SizeN",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "SizeName",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "ClothingId",
                table: "ClothingSize");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "ClothingSize");

            migrationBuilder.RenameTable(
                name: "ClothingSize",
                newName: "ClothingSizes");

            migrationBuilder.AddColumn<double>(
                name: "Chest",
                table: "Sizes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Foots",
                table: "Sizes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Hips",
                table: "Sizes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Legs",
                table: "Sizes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Pelvic",
                table: "Sizes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Shulders",
                table: "Sizes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Sizes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "Waist",
                table: "Sizes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "ClothingSizeId",
                table: "Clothings",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "ClothingSizes",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddColumn<double>(
                name: "SizeN",
                table: "ClothingSizes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SizeName",
                table: "ClothingSizes",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClothingSizes",
                table: "ClothingSizes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SizeId",
                table: "Users",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_UserId",
                table: "Sizes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Clothings_ClothingSizeId",
                table: "Clothings",
                column: "ClothingSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothings_ClothingSizes_ClothingSizeId",
                table: "Clothings",
                column: "ClothingSizeId",
                principalTable: "ClothingSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sizes_Users_UserId",
                table: "Sizes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Sizes_SizeId",
                table: "Users",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
