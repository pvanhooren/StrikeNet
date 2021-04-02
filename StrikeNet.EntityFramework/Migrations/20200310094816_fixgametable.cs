using System;
using Microsoft.EntityFrameworkCore.Migrations;
using StrikeNet.EntityFramework.Enums;

namespace StrikeNet.EntityFramework.Migrations
{
    public partial class fixgametable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Teams",
                table: "Games");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Games",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Team1",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Team2",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<TypeResult>(
                name: "typeResult",
                table: "Games",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Team1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Team2",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "typeResult",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "Teams",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
