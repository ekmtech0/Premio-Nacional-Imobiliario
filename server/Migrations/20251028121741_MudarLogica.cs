using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class MudarLogica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votos_Eleitores_EleitorId",
                table: "Votos");

            migrationBuilder.DropTable(
                name: "Eleitores");

            migrationBuilder.DropIndex(
                name: "IX_Votos_EleitorId_CategoriaId",
                table: "Votos");

            migrationBuilder.DropColumn(
                name: "EleitorId",
                table: "Votos");

            migrationBuilder.AddColumn<string>(
                name: "BrowserId",
                table: "Votos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataVoto",
                table: "Votos",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Votos_BrowserId_CategoriaId",
                table: "Votos",
                columns: new[] { "BrowserId", "CategoriaId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Votos_BrowserId_CategoriaId",
                table: "Votos");

            migrationBuilder.DropColumn(
                name: "BrowserId",
                table: "Votos");

            migrationBuilder.DropColumn(
                name: "DataVoto",
                table: "Votos");

            migrationBuilder.AddColumn<Guid>(
                name: "EleitorId",
                table: "Votos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Eleitores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eleitores", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votos_EleitorId_CategoriaId",
                table: "Votos",
                columns: new[] { "EleitorId", "CategoriaId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Votos_Eleitores_EleitorId",
                table: "Votos",
                column: "EleitorId",
                principalTable: "Eleitores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
