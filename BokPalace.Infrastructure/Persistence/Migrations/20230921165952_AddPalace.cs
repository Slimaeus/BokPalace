using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BokPalace.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPalace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PalaceId",
                table: "Rooms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Palaces",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palaces", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PalaceId",
                table: "Rooms",
                column: "PalaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Palaces_PalaceId",
                table: "Rooms",
                column: "PalaceId",
                principalTable: "Palaces",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Palaces_PalaceId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "Palaces");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_PalaceId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "PalaceId",
                table: "Rooms");
        }
    }
}
