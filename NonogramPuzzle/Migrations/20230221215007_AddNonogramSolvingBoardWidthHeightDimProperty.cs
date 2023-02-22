using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NonogramPuzzle.Migrations
{
    public partial class AddNonogramSolvingBoardWidthHeightDimProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "solvingBoardDim",
                table: "Nonograms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "solvingBoardWidth",
                table: "Nonograms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "solvingBoardHeight",
                table: "Nonograms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "solvingBoardDim",
                table: "Nonograms");

            migrationBuilder.DropColumn(
                name: "solvingBoardWidth",
                table: "Nonograms");

            migrationBuilder.DropColumn(
                name: "solvingBoardHeight",
                table: "Nonograms");
        }
    }
}
