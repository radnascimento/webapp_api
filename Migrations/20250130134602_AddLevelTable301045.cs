using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelTable301045 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studies_AspNetUsers_IdUser",
                table: "Studies");

            migrationBuilder.DropForeignKey(
                name: "FK_Studies_Studies_StudyIdStudy",
                table: "Studies");

            migrationBuilder.DropForeignKey(
                name: "FK_Studies_StudyPC_IdStudyPC",
                table: "Studies");

            migrationBuilder.DropForeignKey(
                name: "FK_Studies_Topics_IdTopic",
                table: "Studies");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyReview_Studies_IdStudy",
                table: "StudyReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Studies",
                table: "Studies");

            migrationBuilder.RenameTable(
                name: "Studies",
                newName: "Study");

            migrationBuilder.RenameIndex(
                name: "IX_Studies_StudyIdStudy",
                table: "Study",
                newName: "IX_Study_StudyIdStudy");

            migrationBuilder.RenameIndex(
                name: "IX_Studies_IdUser",
                table: "Study",
                newName: "IX_Study_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_Studies_IdTopic",
                table: "Study",
                newName: "IX_Study_IdTopic");

            migrationBuilder.RenameIndex(
                name: "IX_Studies_IdStudyPC",
                table: "Study",
                newName: "IX_Study_IdStudyPC");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Study",
                table: "Study",
                column: "IdStudy");

            migrationBuilder.AddForeignKey(
                name: "FK_Study_AspNetUsers_IdUser",
                table: "Study",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Study_StudyPC_IdStudyPC",
                table: "Study",
                column: "IdStudyPC",
                principalTable: "StudyPC",
                principalColumn: "IdStudyPC",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Study_Study_StudyIdStudy",
                table: "Study",
                column: "StudyIdStudy",
                principalTable: "Study",
                principalColumn: "IdStudy");

            migrationBuilder.AddForeignKey(
                name: "FK_Study_Topics_IdTopic",
                table: "Study",
                column: "IdTopic",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Study_AspNetUsers_IdUser",
                table: "Study");

            migrationBuilder.DropForeignKey(
                name: "FK_Study_StudyPC_IdStudyPC",
                table: "Study");

            migrationBuilder.DropForeignKey(
                name: "FK_Study_Study_StudyIdStudy",
                table: "Study");

            migrationBuilder.DropForeignKey(
                name: "FK_Study_Topics_IdTopic",
                table: "Study");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyReview_Study_IdStudy",
                table: "StudyReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Study",
                table: "Study");

            migrationBuilder.RenameTable(
                name: "Study",
                newName: "Studies");

            migrationBuilder.RenameIndex(
                name: "IX_Study_StudyIdStudy",
                table: "Studies",
                newName: "IX_Studies_StudyIdStudy");

            migrationBuilder.RenameIndex(
                name: "IX_Study_IdUser",
                table: "Studies",
                newName: "IX_Studies_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_Study_IdTopic",
                table: "Studies",
                newName: "IX_Studies_IdTopic");

            migrationBuilder.RenameIndex(
                name: "IX_Study_IdStudyPC",
                table: "Studies",
                newName: "IX_Studies_IdStudyPC");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Studies",
                table: "Studies",
                column: "IdStudy");

            migrationBuilder.AddForeignKey(
                name: "FK_Studies_AspNetUsers_IdUser",
                table: "Studies",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Studies_Topics_IdTopic",
                table: "Studies",
                column: "IdTopic",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyReview_Studies_IdStudy",
                table: "StudyReview",
                column: "IdStudy",
                principalTable: "Studies",
                principalColumn: "IdStudy",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
