using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AS.Data.Migrations
{
    /// <inheritdoc />
    public partial class seo_guncellendi : Migration
    {



        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Yeni sütun ekle
            migrationBuilder.AddColumn<string>(
                name: "SeoCode",
                schema: "AS",
                table: "Language",
                maxLength: 100,
                nullable: true);

            // Veriyi taşı
            migrationBuilder.Sql("UPDATE `AS_Language` SET `SeoCode` = `Seo_Code`");

            // Eski sütunu kaldır
            migrationBuilder.DropColumn(
                name: "Seo_Code",
                schema: "AS",
                table: "Language");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eski sütunu ekle
            migrationBuilder.AddColumn<string>(
                name: "Seo_Code",
                schema: "AS",
                table: "Language",
                maxLength: 100,
                nullable: true);

            // Veriyi geri taşı
            migrationBuilder.Sql("UPDATE `AS_Language` SET `Seo_Code` = `SeoCode`");

            // Yeni sütunu kaldır
            migrationBuilder.DropColumn(
                name: "SeoCode",
                schema: "AS",
                table: "Language");
        }



        /// <inheritdoc />
        //protected override void Up(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.RenameColumn(
        //        name: "Seo_Code",
        //        schema: "AS",
        //        table: "Language",
        //        newName: "SeoCode");
        //}

        ///// <inheritdoc />
        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.RenameColumn(
        //        name: "SeoCode",
        //        schema: "AS",
        //        table: "Language",
        //        newName: "Seo_Code");
        //}
    }
}
