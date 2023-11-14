using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestApiProject.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "game");

            migrationBuilder.CreateTable(
                name: "Characters",
                schema: "game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CharacterHealth = table.Column<int>(type: "int", nullable: false),
                    CharacterType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperPowers",
                schema: "game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PowerName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HealthGain = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperPowers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuperPowers_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalSchema: "game",
                        principalTable: "Characters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalSchema: "game",
                        principalTable: "Characters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CharactersSuperpowersJoin",
                schema: "game",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    SuperPowerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharactersSuperpowersJoin", x => new { x.SuperPowerId, x.CharacterId });
                    table.ForeignKey(
                        name: "FK_CharactersSuperpowersJoin_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalSchema: "game",
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharactersSuperpowersJoin_SuperPowers_SuperPowerId",
                        column: x => x.SuperPowerId,
                        principalSchema: "game",
                        principalTable: "SuperPowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharactersSuperpowersJoin_CharacterId",
                schema: "game",
                table: "CharactersSuperpowersJoin",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_SuperPowers_CharacterId",
                schema: "game",
                table: "SuperPowers",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CharacterId",
                schema: "game",
                table: "Users",
                column: "CharacterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharactersSuperpowersJoin",
                schema: "game");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "game");

            migrationBuilder.DropTable(
                name: "SuperPowers",
                schema: "game");

            migrationBuilder.DropTable(
                name: "Characters",
                schema: "game");
        }
    }
}
