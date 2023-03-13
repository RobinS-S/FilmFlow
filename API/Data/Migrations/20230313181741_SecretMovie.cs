using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmFlow.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class SecretMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSecret",
                table: "CinemaShows",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSecret",
                table: "CinemaShows");
        }
    }
}
