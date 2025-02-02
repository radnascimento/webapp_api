using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelTable301051 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyReview_StudyPC_IdStudyPC",
                table: "StudyReview");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyReview_Study_IdStudy",
                table: "StudyReview");

            migrationBuilder.DropIndex(
                name: "IX_StudyReview_IdStudy",
                table: "StudyReview");

            migrationBuilder.DropIndex(
                name: "IX_StudyReview_IdStudyPC",
                table: "StudyReview");

            migrationBuilder.AddColumn<int>(
                name: "StudyIdStudy",
                table: "StudyReview",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudyPCIdStudyPC",
                table: "StudyReview",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudyReview_StudyIdStudy",
                table: "StudyReview",
                column: "StudyIdStudy");

            migrationBuilder.CreateIndex(
                name: "IX_StudyReview_StudyPCIdStudyPC",
                table: "StudyReview",
                column: "StudyPCIdStudyPC");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyReview_StudyPC_StudyPCIdStudyPC",
                table: "StudyReview",
                column: "StudyPCIdStudyPC",
                principalTable: "StudyPC",
                principalColumn: "IdStudyPC",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyReview_Study_StudyIdStudy",
                table: "StudyReview",
                column: "StudyIdStudy",
                principalTable: "Study",
                principalColumn: "IdStudy",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyReview_StudyPC_StudyPCIdStudyPC",
                table: "StudyReview");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyReview_Study_StudyIdStudy",
                table: "StudyReview");

            migrationBuilder.DropIndex(
                name: "IX_StudyReview_StudyIdStudy",
                table: "StudyReview");

            migrationBuilder.DropIndex(
                name: "IX_StudyReview_StudyPCIdStudyPC",
                table: "StudyReview");

            migrationBuilder.DropColumn(
                name: "StudyIdStudy",
                table: "StudyReview");

            migrationBuilder.DropColumn(
                name: "StudyPCIdStudyPC",
                table: "StudyReview");

            migrationBuilder.CreateIndex(
                name: "IX_StudyReview_IdStudy",
                table: "StudyReview",
                column: "IdStudy");

            migrationBuilder.CreateIndex(
                name: "IX_StudyReview_IdStudyPC",
                table: "StudyReview",
                column: "IdStudyPC");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyReview_StudyPC_IdStudyPC",
                table: "StudyReview",
                column: "IdStudyPC",
                principalTable: "StudyPC",
                principalColumn: "IdStudyPC",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyReview_Study_IdStudy",
                table: "StudyReview",
                column: "IdStudy",
                principalTable: "Study",
                principalColumn: "IdStudy",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
