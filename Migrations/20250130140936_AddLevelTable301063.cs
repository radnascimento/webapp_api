using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelTable301063 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StudyReview_IdStudy",
                table: "StudyReview",
                column: "IdStudy");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyReview_Study_IdStudy",
                table: "StudyReview",
                column: "IdStudy",
                principalTable: "Study",
                principalColumn: "IdStudy",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyReview_Study_IdStudy",
                table: "StudyReview");

            migrationBuilder.DropIndex(
                name: "IX_StudyReview_IdStudy",
                table: "StudyReview");
        }
    }
}
