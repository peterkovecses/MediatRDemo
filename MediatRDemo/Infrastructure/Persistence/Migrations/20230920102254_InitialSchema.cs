using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MediatRDemo.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "ReleaseYear", "Title" },
                values: new object[,]
                {
                    { 1, 1994, "Forrest Gump" },
                    { 2, 1994, "Pulp Fiction" },
                    { 3, 1994, "The Shawshank Redemption" },
                    { 4, 2008, "The Dark Knight" },
                    { 5, 1999, "Fight Club" },
                    { 6, 1972, "The Godfather" },
                    { 7, 1997, "Titanic" },
                    { 8, 2010, "Inception" },
                    { 9, 1999, "The Matrix" },
                    { 10, 1993, "Schindler's List" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
