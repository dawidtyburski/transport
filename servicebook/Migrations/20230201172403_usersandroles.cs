using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace transport.Migrations
{
    /// <inheritdoc />
    public partial class usersandroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_InitialAdresses_PickupAdressId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InitialAdresses",
                table: "InitialAdresses");

            migrationBuilder.RenameTable(
                name: "InitialAdresses",
                newName: "PickupAdresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PickupAdresses",
                table: "PickupAdresses",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PickupAdresses_PickupAdressId",
                table: "Orders",
                column: "PickupAdressId",
                principalTable: "PickupAdresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PickupAdresses_PickupAdressId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PickupAdresses",
                table: "PickupAdresses");

            migrationBuilder.RenameTable(
                name: "PickupAdresses",
                newName: "InitialAdresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InitialAdresses",
                table: "InitialAdresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_InitialAdresses_PickupAdressId",
                table: "Orders",
                column: "PickupAdressId",
                principalTable: "InitialAdresses",
                principalColumn: "Id");
        }
    }
}
