using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MjaARES.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemovesommefromtableTrancheAgeandaddittoSexeTrancheAgeEvenement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Somme",
                table: "TrancheAges");

            migrationBuilder.AddColumn<int>(
                name: "Somme",
                table: "SexeTrancheAgeEvenements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Somme",
                table: "SexeTrancheAgeEvenements");

            migrationBuilder.AddColumn<int>(
                name: "Somme",
                table: "TrancheAges",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
