using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AS.Data.Migrations
{
    /// <inheritdoc />
    public partial class contentCategoryIdRenameContentCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Yeni sütun ekleyin
            migrationBuilder.Sql("ALTER TABLE `AS_Content` ADD `ContentCategory` INT(11) NOT NULL;");

            // Eski sütundaki verileri yeni sütuna taşıyın
            migrationBuilder.Sql("UPDATE `AS_Content` SET `ContentCategory` = `ContentCategoryId`;");

            // Eski sütunu kaldırın
            migrationBuilder.Sql("ALTER TABLE `AS_Content` DROP COLUMN `ContentCategoryId`;");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Yeni sütun oluşturun
            migrationBuilder.Sql("ALTER TABLE `AS_Content` ADD `ContentCategoryId` INT(11) NOT NULL;");

            // Verileri eski sütuna geri taşıyın
            migrationBuilder.Sql("UPDATE `AS_Content` SET `ContentCategoryId` = `ContentCategory`;");

            // Yeni sütunu kaldırın
            migrationBuilder.Sql("ALTER TABLE `AS_Content` DROP COLUMN `ContentCategory`;");
        }


    }
}
