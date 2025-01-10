using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DwarfCodeData.Migrations
{
    /// <inheritdoc />
    public partial class EliminatAnimeUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AnimeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeUsers", x => new { x.UserId, x.AnimeId });
                    table.ForeignKey(
                        name: "FK_AnimeUsers_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "AnimeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeUsers_AnimeId",
                table: "AnimeUsers",
                column: "AnimeId");
        }
    }
}
