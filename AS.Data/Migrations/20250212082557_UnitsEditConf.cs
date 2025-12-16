using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AS.Data.Migrations
{
    /// <inheritdoc />
    public partial class UnitsEditConf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Units_LogoDocumentId",
                schema: "AS",
                table: "Units",
                column: "LogoDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_LogoTwoDocumentId",
                schema: "AS",
                table: "Units",
                column: "LogoTwoDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Document_LogoDocumentId",
                schema: "AS",
                table: "Units",
                column: "LogoDocumentId",
                principalSchema: "AS",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Document_LogoTwoDocumentId",
                schema: "AS",
                table: "Units",
                column: "LogoTwoDocumentId",
                principalSchema: "AS",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Document_LogoDocumentId",
                schema: "AS",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Document_LogoTwoDocumentId",
                schema: "AS",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_LogoDocumentId",
                schema: "AS",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_LogoTwoDocumentId",
                schema: "AS",
                table: "Units");
        }
    }
}
