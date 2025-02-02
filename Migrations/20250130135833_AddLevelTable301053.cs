using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelTable301053 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Study_Study_StudyIdStudy",
                table: "Study");

            migrationBuilder.DropIndex(
                name: "IX_Study_StudyIdStudy",
                table: "Study");

            migrationBuilder.DropColumn(
                name: "StudyIdStudy",
                table: "Study");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudyIdStudy",
                table: "Study",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Study_StudyIdStudy",
                table: "Study",
                column: "StudyIdStudy");

            migrationBuilder.AddForeignKey(
                name: "FK_Study_Study_StudyIdStudy",
                table: "Study",
                column: "StudyIdStudy",
                principalTable: "Study",
                principalColumn: "IdStudy");
        }
    }
}
