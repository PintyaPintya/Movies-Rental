using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MoviesRental.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieDto_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "DateAdded", "Genre", "Name", "ReleaseDate", "Stock" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drama", "The Shawshank Redemption", new DateOnly(1994, 9, 22), 3 },
                    { 2, new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Crime", "The Godfather", new DateOnly(1972, 3, 24), 5 },
                    { 3, new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action", "The Dark Knight", new DateOnly(2008, 7, 18), 5 },
                    { 4, new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drama", "Forrest Gump", new DateOnly(1994, 7, 6), 5 },
                    { 5, new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sci-Fi", "Inception", new DateOnly(2010, 7, 16), 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieDto_UserId",
                table: "MovieDto",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieDto");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
