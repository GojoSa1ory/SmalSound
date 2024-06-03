using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPCourseWork.Migrations
{
    /// <inheritdoc />
    public partial class createTrackListenerCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListenCount",
                table: "Tracks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListenCount",
                table: "Tracks");
        }
    }
}
