using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AS.Data.Migrations
{
    /// <inheritdoc />
    public partial class pagesEntitiySlugDeleteSeoTitleAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Yeni sütun ekle
            migrationBuilder.AddColumn<string>(
                name: "SeoTitle",
                schema: "AS",
                table: "Pages",
                maxLength: 255,
                nullable: true);

            // Veriyi taşı
            migrationBuilder.Sql("UPDATE `AS_Pages` SET `SeoTitle` = `Slug`");

            // Eski sütunu kaldır
            migrationBuilder.DropColumn(
                name: "Slug",
                schema: "AS",
                table: "Pages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eski sütunu ekle
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                schema: "AS",
                table: "Pages",
                maxLength: 255,
                nullable: true);

            // Veriyi geri taşı
            migrationBuilder.Sql("UPDATE `AS_Pages` SET `Slug` = `SeoTitle`");

            // Yeni sütunu kaldır
            migrationBuilder.DropColumn(
                name: "SeoTitle",
                schema: "AS",
                table: "Pages");
        }

    }
}

//Eski Hali bizim maridb sürümü desteklemiyor. Ondan migration düzenlemek lazımdı. 
/// <inheritdoc />
//protected override void Up(MigrationBuilder migrationBuilder)
//{
//    migrationBuilder.RenameColumn(
//        name: "Slug",
//        schema: "AS",
//        table: "Pages",
//        newName: "SeoTitle");
//}

///// <inheritdoc />
//protected override void Down(MigrationBuilder migrationBuilder)
//{
//    migrationBuilder.RenameColumn(
//        name: "SeoTitle",
//        schema: "AS",
//        table: "Pages",
//        newName: "Slug");

