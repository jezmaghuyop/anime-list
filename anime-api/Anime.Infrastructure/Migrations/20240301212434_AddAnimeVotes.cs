using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anime.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimeVotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Votes",
                table: "Animes");

            migrationBuilder.CreateTable(
                name: "AnimeVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimeVotes_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeVotes_AnimeId",
                table: "AnimeVotes",
                column: "AnimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeVotes");

            migrationBuilder.AddColumn<long>(
                name: "Votes",
                table: "Animes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
