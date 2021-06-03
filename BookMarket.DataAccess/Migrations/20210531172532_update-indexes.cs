using Microsoft.EntityFrameworkCore.Migrations;

namespace BookMarket.DataAccess.Migrations
{
    public partial class updateindexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Writers_Name",
                table: "Writers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Publishers_Name",
                table: "Publishers");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Genres_Name",
                table: "Genres");

            migrationBuilder.CreateIndex(
                name: "IX_Writers_Name",
                table: "Writers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_Name",
                table: "Publishers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Writers_Name",
                table: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Publishers_Name",
                table: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_Genres_Name",
                table: "Genres");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Writers_Name",
                table: "Writers",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Publishers_Name",
                table: "Publishers",
                column: "Name");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Genres_Name",
                table: "Genres",
                column: "Name");
        }
    }
}
