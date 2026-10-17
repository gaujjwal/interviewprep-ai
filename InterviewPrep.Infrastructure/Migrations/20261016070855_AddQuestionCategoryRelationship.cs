using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewPrep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionCategoryRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(
     MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.Sql(
                """
        UPDATE q
        SET q.CategoryId = c.Id
        FROM dbo.Questions AS q
        INNER JOIN dbo.Categories AS c
            ON LOWER(LTRIM(RTRIM(q.Category))) =
               LOWER(LTRIM(RTRIM(c.Name)));
        """
            );

            migrationBuilder.Sql(
                """
        IF EXISTS
        (
            SELECT 1
            FROM dbo.Questions
            WHERE CategoryId IS NULL
        )
        BEGIN
            THROW 51000,
            'Category migration failed: one or more questions could not be mapped to a category.',
            1;
        END;
        """
            );

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CategoryId",
                table: "Questions",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Categories_CategoryId",
                table: "Questions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
        protected override void Down(
            MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Categories_CategoryId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CategoryId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Questions");
        }
    }
}
