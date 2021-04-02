using Microsoft.EntityFrameworkCore.Migrations;

namespace StrikeNet.EntityFramework.Migrations
{
    public partial class AddPredictionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_typeResult",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "Predictions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    PredictedResult = table.Column<string>(nullable: true),
                    PredictedTypeResult = table.Column<int>(nullable: false),
                    EarnedPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predictions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Predictions");

            migrationBuilder.AddColumn<int>(
                name: "_typeResult",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "_typeResult",
                value: 1);
        }
    }
}
