﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NonogramPuzzle.Migrations
{
    public partial class AddNonogramDimProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NonogramDim",
                table: "Nonograms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NonogramDim",
                table: "Nonograms");
        }
    }
}
