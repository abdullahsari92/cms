using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AS.Data.Migrations
{
    /// <inheritdoc />
    public partial class _sliderEntityAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishEndDate",
                schema: "AS",
                table: "Activity",
                type: "datetime(6)",
                nullable: false,
                comment: "Etkinliğin sitede yayından kaldırılma tarihi",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishBeginDate",
                schema: "AS",
                table: "Activity",
                type: "datetime(6)",
                nullable: false,
                comment: "Etkinliğin sitede yayına alınma başlangıç tarihi",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActivityStartDate",
                schema: "AS",
                table: "Activity",
                type: "datetime(6)",
                nullable: false,
                comment: "Etkinliğin gerçekleşme başlangıç tarihi",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActivityEndDate",
                schema: "AS",
                table: "Activity",
                type: "datetime(6)",
                nullable: false,
                comment: "Etkinliğin gerçekleşme bitiş tarihi",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.CreateTable(
                name: "Slider",
                schema: "AS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(250)", nullable: false, comment: "Slider Başlık")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(700)", nullable: false, comment: "Slider Açıklaması")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, comment: "Slider Sıralaması"),
                    LanguageId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ContentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsApproved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slider_Content_ContentId",
                        column: x => x.ContentId,
                        principalSchema: "AS",
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Slider_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "AS",
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Slider_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "AS",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Slider_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalSchema: "AS",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Slider Tablosu")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Slider_ContentId",
                schema: "AS",
                table: "Slider",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Slider_CreatedById",
                schema: "AS",
                table: "Slider",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Slider_LanguageId",
                schema: "AS",
                table: "Slider",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Slider_UpdatedById",
                schema: "AS",
                table: "Slider",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slider",
                schema: "AS");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishEndDate",
                schema: "AS",
                table: "Activity",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "Etkinliğin sitede yayından kaldırılma tarihi");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishBeginDate",
                schema: "AS",
                table: "Activity",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "Etkinliğin sitede yayına alınma başlangıç tarihi");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActivityStartDate",
                schema: "AS",
                table: "Activity",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "Etkinliğin gerçekleşme başlangıç tarihi");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActivityEndDate",
                schema: "AS",
                table: "Activity",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldComment: "Etkinliğin gerçekleşme bitiş tarihi");
        }
    }
}
