using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DwarfCodeData.Migrations
{
    /// <inheritdoc />
    public partial class NombreDeLaNuevaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeUser");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "AnimeUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "AnimeUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnimeUser",
                columns: table => new
                {
                    AnimesAnimeId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeUser", x => new { x.AnimesAnimeId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_AnimeUser_Animes_AnimesAnimeId",
                        column: x => x.AnimesAnimeId,
                        principalTable: "Animes",
                        principalColumn: "AnimeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeUser_UsersUserId",
                table: "AnimeUser",
                column: "UsersUserId");
        }
    }
}
