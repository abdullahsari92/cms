using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AS.Data.Migrations
{
    /// <inheritdoc />
    public partial class activityEntityOk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activity",
                schema: "AS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ContentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    LanguageId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "varchar(400)", nullable: false, comment: "Etkinlik Başlık")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SeoTitle = table.Column<string>(type: "varchar(250)", nullable: false, comment: "Etkinlik Seo Title")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Speakers = table.Column<string>(type: "varchar(250)", nullable: false, comment: "Etkinlik Konuşmacıları")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Moderator = table.Column<string>(type: "varchar(250)", nullable: false, comment: "Etkinlik Moderatörleri")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Responsible = table.Column<string>(type: "varchar(250)", nullable: false, comment: "Etkinlik Sorumluları")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContentDetail = table.Column<string>(type: "varchar(850)", nullable: false, comment: "Etkinlik İçeriği ")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PosterUrl = table.Column<string>(type: "varchar(250)", nullable: false, comment: "Etkinlik Poster Url")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ActivityCategory = table.Column<string>(type: "varchar(250)", nullable: false, comment: "Etkinlik Kategorisi(   Seminar = 0,  Webinar = 1,  Meeting = 2,  Workshop = 3,  Training = 4,)")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ActivityType = table.Column<string>(type: "varchar(250)", nullable: false, comment: "Etkinlik Tipi (Physical = 1, Virtual = 2, Hybrid = 3,)")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PublishBeginDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PublishEndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ActivityStartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ActivityEndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    IsApproved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activity_Content_ContentId",
                        column: x => x.ContentId,
                        principalSchema: "AS",
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activity_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "AS",
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activity_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "AS",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activity_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalSchema: "AS",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Etkinlikler Tablosu")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_ContentId",
                schema: "AS",
                table: "Activity",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_CreatedById",
                schema: "AS",
                table: "Activity",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_LanguageId",
                schema: "AS",
                table: "Activity",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_UpdatedById",
                schema: "AS",
                table: "Activity",
                column: "UpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activity",
                schema: "AS");
        }
    }
}
