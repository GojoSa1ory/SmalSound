using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPCourseWork.Migrations
{
    /// <inheritdoc />
    public partial class TimeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "createdAt",
                table: "Tracks",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "updatedAt",
                table: "Tracks",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "createdAt",
                table: "Playlists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "updatedAt",
                table: "Playlists",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "Playlists");
        }
    }
}
