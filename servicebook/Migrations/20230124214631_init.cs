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
                    CountryName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
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
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
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
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
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
                name: "PrincipalsAdresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrincipalsAdresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrincipalsAdresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Principals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrincipalAdressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Principals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Principals_PrincipalsAdresses_PrincipalAdressId",
                        column: x => x.PrincipalAdressId,
                        principalTable: "PrincipalsAdresses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<float>(type: "real", maxLength: 10, nullable: false),
                    PalletPlace = table.Column<float>(type: "real", maxLength: 2, nullable: false),
                    Directly = table.Column<bool>(type: "bit", nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    InitialAdressId = table.Column<int>(type: "int", nullable: false),
                    DestinationAdressId = table.Column<int>(type: "int", nullable: false),
                    PrincipalId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_Orders_InitialAdresses_InitialAdressId",
                        column: x => x.InitialAdressId,
                        principalTable: "InitialAdresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Principals_PrincipalId",
                        column: x => x.PrincipalId,
                        principalTable: "Principals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Orders_InitialAdressId",
                table: "Orders",
                column: "InitialAdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PrincipalId",
                table: "Orders",
                column: "PrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_Principals_PrincipalAdressId",
                table: "Principals",
                column: "PrincipalAdressId");

            migrationBuilder.CreateIndex(
                name: "IX_PrincipalsAdresses_CountryId",
                table: "PrincipalsAdresses",
                column: "CountryId");
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
                name: "Principals");

            migrationBuilder.DropTable(
                name: "PrincipalsAdresses");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
