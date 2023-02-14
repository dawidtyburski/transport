﻿
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace transport.Migrations
{
    /// <inheritdoc />
    public partial class blockuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isBlocked",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBlocked",
                table: "AspNetUsers");
        }
    }
}
