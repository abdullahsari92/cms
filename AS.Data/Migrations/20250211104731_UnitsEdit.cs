using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AS.Data.Migrations
{
    /// <inheritdoc />
    public partial class UnitsEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                schema: "AS",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "Logo2",
                schema: "AS",
                table: "Units");

            migrationBuilder.AddColumn<Guid>(
                name: "LogoDocumentId",
                schema: "AS",
                table: "Units",
                type: "char(36)",
                nullable: true,
                comment: "Birim Logo 1",
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "LogoTwoDocumentId",
                schema: "AS",
                table: "Units",
                type: "char(36)",
                nullable: true,
                comment: "Birim Logo 2",
                collation: "ascii_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoDocumentId",
                schema: "AS",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "LogoTwoDocumentId",
                schema: "AS",
                table: "Units");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                schema: "AS",
                table: "Units",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "",
                comment: "Birim Logo 1")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Logo2",
                schema: "AS",
                table: "Units",
                type: "varchar(150)",
                nullable: true,
                comment: "Birim Logo 2")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
