using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPCourseWork.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFavoritesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Favorite_FavoriteId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FavoriteId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Favorite",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_UserId",
                table: "Favorite",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_Users_UserId",
                table: "Favorite",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_Users_UserId",
                table: "Favorite");

            migrationBuilder.DropIndex(
                name: "IX_Favorite_UserId",
                table: "Favorite");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Favorite");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FavoriteId",
                table: "Users",
                column: "FavoriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Favorite_FavoriteId",
                table: "Users",
                column: "FavoriteId",
                principalTable: "Favorite",
                principalColumn: "Id");
        }
    }
}
