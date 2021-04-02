using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StrikeNet.EntityFramework.Migrations
{
    public partial class FixingUserIdDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Predictions",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PointsReceived",
                table: "Predictions",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointsReceived",
                table: "Predictions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Predictions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
