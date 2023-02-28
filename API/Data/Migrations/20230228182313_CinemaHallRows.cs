using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmFlow.API.Migrations
{
    /// <inheritdoc />
    public partial class CinemaHallRows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemaHallRows_CinemaHalls_HallId",
                table: "CinemaHallRows");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_CinemaHallRows_RowId",
                table: "Reservations");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CinemaHallRows_HallId_RowId",
                table: "CinemaHallRows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CinemaHallRows",
                table: "CinemaHallRows");

            migrationBuilder.RenameTable(
                name: "CinemaHallRows",
                newName: "CinemaHallsRows");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CinemaHallsRows_HallId_RowId",
                table: "CinemaHallsRows",
                columns: new[] { "HallId", "RowId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CinemaHallsRows",
                table: "CinemaHallsRows",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemaHallsRows_CinemaHalls_HallId",
                table: "CinemaHallsRows",
                column: "HallId",
                principalTable: "CinemaHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_CinemaHallsRows_RowId",
                table: "Reservations",
                column: "RowId",
                principalTable: "CinemaHallsRows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CinemaHallsRows_CinemaHalls_HallId",
                table: "CinemaHallsRows");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_CinemaHallsRows_RowId",
                table: "Reservations");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CinemaHallsRows_HallId_RowId",
                table: "CinemaHallsRows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CinemaHallsRows",
                table: "CinemaHallsRows");

            migrationBuilder.RenameTable(
                name: "CinemaHallsRows",
                newName: "CinemaHallRows");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CinemaHallRows_HallId_RowId",
                table: "CinemaHallRows",
                columns: new[] { "HallId", "RowId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CinemaHallRows",
                table: "CinemaHallRows",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CinemaHallRows_CinemaHalls_HallId",
                table: "CinemaHallRows",
                column: "HallId",
                principalTable: "CinemaHalls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_CinemaHallRows_RowId",
                table: "Reservations",
                column: "RowId",
                principalTable: "CinemaHallRows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
