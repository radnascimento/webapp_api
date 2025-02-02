using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelTable301055 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudyPCIdStudyPC",
                table: "Study",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Study_StudyPCIdStudyPC",
                table: "Study",
                column: "StudyPCIdStudyPC");

            migrationBuilder.AddForeignKey(
                name: "FK_Study_StudyPC_StudyPCIdStudyPC",
                table: "Study",
                column: "StudyPCIdStudyPC",
                principalTable: "StudyPC",
                principalColumn: "IdStudyPC",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
