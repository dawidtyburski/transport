using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace transport.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "DestinationAdresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationAdresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DestinationAdresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InitialAdresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitialAdresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InitialAdresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    PalletPlace = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    InitialAdressId = table.Column<int>(type: "int", nullable: false),
                    PickupAdressId = table.Column<int>(type: "int", nullable: false),
                    DestinationAdressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_DestinationAdresses_DestinationAdressId",
                        column: x => x.DestinationAdressId,
                        principalTable: "DestinationAdresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_InitialAdresses_PickupAdressId",
                        column: x => x.PickupAdressId,
                        principalTable: "InitialAdresses",
                        principalColumn: "Id");
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
                name: "IX_DestinationAdresses_CountryId",
                table: "DestinationAdresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_InitialAdresses_CountryId",
                table: "InitialAdresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DestinationAdressId",
                table: "Orders",
                column: "DestinationAdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PickupAdressId",
                table: "Orders",
                column: "PickupAdressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "DestinationAdresses");

            migrationBuilder.DropTable(
                name: "InitialAdresses");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
