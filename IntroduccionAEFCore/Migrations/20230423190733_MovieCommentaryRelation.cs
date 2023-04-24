using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntroduccionAEFCore.Migrations
{
    /// <inheritdoc />
    public partial class MovieCommentaryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Commentaries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Commentaries_MovieId",
                table: "Commentaries",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaries_Movies_MovieId",
                table: "Commentaries",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commentaries_Movies_MovieId",
                table: "Commentaries");

            migrationBuilder.DropIndex(
                name: "IX_Commentaries_MovieId",
                table: "Commentaries");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Commentaries");
        }
    }
}
