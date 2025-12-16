using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AS.Data.Migrations
{
    /// <inheritdoc />
    public partial class SliderEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Öncelikle mevcut ContentId kolonunun Foreign Key ilişkisini kaldırıyoruz
            migrationBuilder.DropForeignKey(
                name: "FK_Slider_Content_ContentId",
                schema: "AS",
                table: "Slider");

            // Yeni UnitId sütunu ekliyoruz
            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                schema: "AS",
                table: "Slider",
                type: "char(36)",
                nullable: false,
                defaultValue: Guid.Empty,
                collation: "ascii_general_ci");

            // ContentId kolonundaki veriyi UnitId'ye taşıyoruz
            migrationBuilder.Sql("UPDATE AS_Slider SET UnitId = ContentId;");

            // Eski ContentId kolonunu siliyoruz
            migrationBuilder.DropColumn(
                name: "ContentId",
                schema: "AS",
                table: "Slider");

            // UnitId için indeks oluşturuyoruz
            migrationBuilder.CreateIndex(
                name: "IX_Slider_UnitId",
                schema: "AS",
                table: "Slider",
                column: "UnitId");

            // Yeni Foreign Key ekliyoruz (UnitId ile Units tablosu arasında)
            migrationBuilder.AddForeignKey(
                name: "FK_Slider_Units_UnitId",
                schema: "AS",
                table: "Slider",
                column: "UnitId",
                principalSchema: "AS",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            // Yeni DocumentId sütunu ekliyoruz
            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                schema: "AS",
                table: "Slider",
                type: "char(36)",
                nullable: false,
                defaultValue: Guid.Empty,
                collation: "ascii_general_ci");

            // DocumentId için indeks oluşturuyoruz
            migrationBuilder.CreateIndex(
                name: "IX_Slider_DocumentId",
                schema: "AS",
                table: "Slider",
                column: "DocumentId");

            // Yeni Foreign Key ekliyoruz (DocumentId ile Document tablosu arasında)
            migrationBuilder.AddForeignKey(
                name: "FK_Slider_Document_DocumentId",
                schema: "AS",
                table: "Slider",
                column: "DocumentId",
                principalSchema: "AS",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Foreign Key ve Indexleri kaldırıyoruz
            migrationBuilder.DropForeignKey(
                name: "FK_Slider_Document_DocumentId",
                schema: "AS",
                table: "Slider");

            migrationBuilder.DropForeignKey(
                name: "FK_Slider_Units_UnitId",
                schema: "AS",
                table: "Slider");

            migrationBuilder.DropIndex(
                name: "IX_Slider_DocumentId",
                schema: "AS",
                table: "Slider");

            // DocumentId kolonunu siliyoruz
            migrationBuilder.DropColumn(
                name: "DocumentId",
                schema: "AS",
                table: "Slider");

            // UnitId kolonunu siliyoruz
            migrationBuilder.DropColumn(
                name: "UnitId",
                schema: "AS",
                table: "Slider");

            // Eski ContentId kolonunu tekrar ekliyoruz
            migrationBuilder.AddColumn<Guid>(
                name: "ContentId",
                schema: "AS",
                table: "Slider",
                type: "char(36)",
                nullable: false,
                defaultValue: Guid.Empty,
                collation: "ascii_general_ci");

            // ContentId için indeks oluşturuyoruz
            migrationBuilder.CreateIndex(
                name: "IX_Slider_ContentId",
                schema: "AS",
                table: "Slider",
                column: "ContentId");

            // Eski Foreign Key ekliyoruz (ContentId ile Content tablosu arasında)
            migrationBuilder.AddForeignKey(
                name: "FK_Slider_Content_ContentId",
                schema: "AS",
                table: "Slider",
                column: "ContentId",
                principalSchema: "AS",
                principalTable: "Content",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

    }
}
