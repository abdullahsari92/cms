using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AS.Data.Migrations
{
    /// <inheritdoc />
    public partial class unitcolor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                schema: "AS",
                table: "Units",
                type: "varchar(40)",
                nullable: false,
                defaultValue: "",
                comment: "Birim Tema Rengi")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "SeoTitle",
                schema: "AS",
                table: "Announcement",
                type: "varchar(500)",
                nullable: false,
                comment: "Duyuru Seo Title",
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldComment: "Duyuru Seo Title")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ContentDetail",
                schema: "AS",
                table: "Announcement",
                type: "varchar(4000)",
                nullable: false,
                comment: "Duyuru İçeriği ",
                oldClrType: typeof(string),
                oldType: "varchar(850)",
                oldComment: "Duyuru İçeriği ")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                schema: "AS",
                table: "Units");

            migrationBuilder.AlterColumn<string>(
                name: "SeoTitle",
                schema: "AS",
                table: "Announcement",
                type: "varchar(250)",
                nullable: false,
                comment: "Duyuru Seo Title",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldComment: "Duyuru Seo Title")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ContentDetail",
                schema: "AS",
                table: "Announcement",
                type: "varchar(850)",
                nullable: false,
                comment: "Duyuru İçeriği ",
                oldClrType: typeof(string),
                oldType: "varchar(4000)",
                oldComment: "Duyuru İçeriği ")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
