using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace transport.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DestinationAdresses_Countries_CountryId",
                table: "DestinationAdresses");

            migrationBuilder.DropForeignKey(
                name: "FK_InitialAdresses_Countries_CountryId",
                table: "InitialAdresses");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_InitialAdresses_CountryId",
                table: "InitialAdresses");

            migrationBuilder.DropIndex(
                name: "IX_DestinationAdresses_CountryId",
                table: "DestinationAdresses");

            migrationBuilder.DropColumn(
                name: "InitialAdressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "InitialAdresses");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "DestinationAdresses");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "InitialAdresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "DestinationAdresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "InitialAdresses");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "DestinationAdresses");

            migrationBuilder.AddColumn<int>(
                name: "InitialAdressId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "InitialAdresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "DestinationAdresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CountryName" },
                values: new object[,]
                {
                    { 1, "Austria" },
                    { 2, "Belgia" },
                    { 3, "Bułgaria" },
                    { 4, "Chorwacja" },
                    { 5, "Cypr" },
                    { 6, "Czechy" },
                    { 7, "Dania" },
                    { 8, "Estonia" },
                    { 9, "Finlandia" },
                    { 10, "Francja" },
                    { 11, "Grecja" },
                    { 12, "Hiszpania" },
                    { 13, "Irlandia" },
                    { 14, "Litwa" },
                    { 15, "Luksemburg" },
                    { 16, "Łotwa" },
                    { 17, "Malta" },
                    { 18, "Holandia" },
                    { 19, "Niemcy" },
                    { 20, "Polska" },
                    { 21, "Portugalia" },
                    { 22, "Rumunia" },
                    { 23, "Słowacja" },
                    { 24, "Słowenia" },
                    { 25, "Szwecja" },
                    { 26, "Węgry" },
                    { 27, "Włochy" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InitialAdresses_CountryId",
                table: "InitialAdresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DestinationAdresses_CountryId",
                table: "DestinationAdresses",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DestinationAdresses_Countries_CountryId",
                table: "DestinationAdresses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InitialAdresses_Countries_CountryId",
                table: "InitialAdresses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
