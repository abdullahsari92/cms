using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AS.Data.Migrations
{
    /// <inheritdoc />
    public partial class ContentConfiguration_UpdateDocumentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ContentCategoryId",
                schema: "AS",
                table: "Content",
                type: "int",
                nullable: false,
                comment: "İçerik Kategorisi : News=0,       Activity=1,      Announcement=2, Slider=3",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "İçerik Kategorisi : News=0,       Activity=1,      Announcement=2,");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ContentCategoryId",
                schema: "AS",
                table: "Content",
                type: "int",
                nullable: false,
                comment: "İçerik Kategorisi : News=0,       Activity=1,      Announcement=2,",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "İçerik Kategorisi : News=0,       Activity=1,      Announcement=2, Slider=3");
        }
    }
}
