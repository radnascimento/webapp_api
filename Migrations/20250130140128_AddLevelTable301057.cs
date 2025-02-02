using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelTable301057 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudyPCIdStudyPC",
                table: "Study",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Study_StudyPCIdStudyPC",
                table: "Study",
                column: "StudyPCIdStudyPC");

            migrationBuilder.AddForeignKey(
                name: "FK_Study_StudyPC_StudyPCIdStudyPC",
                table: "Study",
                column: "StudyPCIdStudyPC",
                principalTable: "StudyPC",
                principalColumn: "IdStudyPC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Study_StudyPC_StudyPCIdStudyPC",
                table: "Study");

            migrationBuilder.DropIndex(
                name: "IX_Study_StudyPCIdStudyPC",
                table: "Study");

            migrationBuilder.DropColumn(
                name: "StudyPCIdStudyPC",
                table: "Study");
        }
    }
}
