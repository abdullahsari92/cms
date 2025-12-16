using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AS.Data.Migrations
{
    /// <inheritdoc />
    public partial class announcementEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SeoTitle",
                schema: "AS",
                table: "News",
                type: "varchar(250)",
                nullable: false,
                comment: "Haber Seo Title",
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldComment: "Seo Title")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                schema: "AS",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DisplayOrder",
                schema: "AS",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keyword",
                schema: "AS",
                table: "Document",
                type: "varchar(150)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Announcement",
                schema: "AS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(400)", nullable: false, comment: "Duyuru Başlık")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SeoTitle = table.Column<string>(type: "varchar(250)", nullable: false, comment: "Duyuru Seo Title")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContentDetail = table.Column<string>(type: "varchar(850)", nullable: false, comment: "Duyuru İçeriği ")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    ContentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LanguageId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PublishBeginDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PublishEndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AnnouncementType = table.Column<int>(type: "int", nullable: false, comment: "Duyuru Tipleri"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsApproved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Announcement_Content_ContentId",
                        column: x => x.ContentId,
                        principalSchema: "AS",
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Announcement_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "AS",
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Announcement_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "AS",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Announcement_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalSchema: "AS",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Duyurular Tablosu")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_ContentId",
                schema: "AS",
                table: "Announcement",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_CreatedById",
                schema: "AS",
                table: "Announcement",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_LanguageId",
                schema: "AS",
                table: "Announcement",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcement_UpdatedById",
                schema: "AS",
                table: "Announcement",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcement",
                schema: "AS");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                schema: "AS",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Keyword",
                schema: "AS",
                table: "Document");

            migrationBuilder.AlterColumn<string>(
                name: "SeoTitle",
                schema: "AS",
                table: "News",
                type: "varchar(150)",
                nullable: false,
                comment: "Seo Title",
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldComment: "Haber Seo Title")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "DisplayOrder",
                schema: "AS",
                table: "Menus",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
