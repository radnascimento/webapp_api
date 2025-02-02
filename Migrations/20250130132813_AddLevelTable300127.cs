using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelTable300127 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdStudyPC",
                table: "Studies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudyIdStudy",
                table: "Studies",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudyPC",
                columns: table => new
                {
                    IdStudyPC = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyPC", x => x.IdStudyPC);
                });

            migrationBuilder.CreateTable(
                name: "StudyReview",
                columns: table => new
                {
                    IdStudyReview = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdStudy = table.Column<int>(type: "INTEGER", nullable: false),
                    IdStudyPC = table.Column<int>(type: "INTEGER", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyReview", x => x.IdStudyReview);
                    table.ForeignKey(
                        name: "FK_StudyReview_Studies_IdStudy",
                        column: x => x.IdStudy,
                        principalTable: "Studies",
                        principalColumn: "IdStudy",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyReview_StudyPC_IdStudyPC",
                        column: x => x.IdStudyPC,
                        principalTable: "StudyPC",
                        principalColumn: "IdStudyPC",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Studies_IdStudyPC",
                table: "Studies",
                column: "IdStudyPC");

            migrationBuilder.CreateIndex(
                name: "IX_Studies_StudyIdStudy",
                table: "Studies",
                column: "StudyIdStudy");

            migrationBuilder.CreateIndex(
                name: "IX_StudyReview_IdStudy",
                table: "StudyReview",
                column: "IdStudy");

            migrationBuilder.CreateIndex(
                name: "IX_StudyReview_IdStudyPC",
                table: "StudyReview",
                column: "IdStudyPC");

            migrationBuilder.AddForeignKey(
                name: "FK_Studies_Studies_StudyIdStudy",
                table: "Studies",
                column: "StudyIdStudy",
                principalTable: "Studies",
                principalColumn: "IdStudy");

            migrationBuilder.AddForeignKey(
                name: "FK_Studies_StudyPC_IdStudyPC",
                table: "Studies",
                column: "IdStudyPC",
                principalTable: "StudyPC",
                principalColumn: "IdStudyPC",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studies_Studies_StudyIdStudy",
                table: "Studies");

            migrationBuilder.DropForeignKey(
                name: "FK_Studies_StudyPC_IdStudyPC",
                table: "Studies");

            migrationBuilder.DropTable(
                name: "StudyReview");

            migrationBuilder.DropTable(
                name: "StudyPC");

            migrationBuilder.DropIndex(
                name: "IX_Studies_IdStudyPC",
                table: "Studies");

            migrationBuilder.DropIndex(
                name: "IX_Studies_StudyIdStudy",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "IdStudyPC",
                table: "Studies");

            migrationBuilder.DropColumn(
                name: "StudyIdStudy",
                table: "Studies");
        }
    }
}
