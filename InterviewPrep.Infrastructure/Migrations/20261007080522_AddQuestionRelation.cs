using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewPrep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Questions_RoleId",
                table: "Questions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Roles_RoleId",
                table: "Questions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Roles_RoleId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_RoleId",
                table: "Questions");
        }
    }
}
