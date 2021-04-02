using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StrikeNet.EntityFramework.Migrations
{
    public partial class FirstGameData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "_typeResult",
                table: "Games",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Date", "Result", "Team1", "Team2", "_typeResult", "typeResult" },
                values: new object[] { 1, new DateTime(2020, 3, 10, 21, 0, 0, 0, DateTimeKind.Unspecified), "3-0", "RB Leipzig", "Tottenham Hotspur", 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "_typeResult",
                table: "Games");
        }
    }
}
