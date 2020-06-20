using Microsoft.EntityFrameworkCore.Migrations;

namespace Vk_server.Migrations
{
    public partial class fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_OAuthClientDetails_OAuthClientDetailId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Sizes_SizeId",
                table: "Users");

            migrationBuilder.AlterColumn<long>(
                name: "SizeId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "OAuthClientDetailId",
                table: "Users",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "VkId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Sizes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Coordinates",
                table: "PhotoHumans",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_UserId",
                table: "Sizes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sizes_Users_UserId",
                table: "Sizes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_OAuthClientDetails_OAuthClientDetailId",
                table: "Users",
                column: "OAuthClientDetailId",
                principalTable: "OAuthClientDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Sizes_SizeId",
                table: "Users",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sizes_Users_UserId",
                table: "Sizes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_OAuthClientDetails_OAuthClientDetailId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Sizes_SizeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Sizes_UserId",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "VkId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "PhotoHumans");

            migrationBuilder.AlterColumn<long>(
                name: "SizeId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "OAuthClientDetailId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_OAuthClientDetails_OAuthClientDetailId",
                table: "Users",
                column: "OAuthClientDetailId",
                principalTable: "OAuthClientDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Sizes_SizeId",
                table: "Users",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
