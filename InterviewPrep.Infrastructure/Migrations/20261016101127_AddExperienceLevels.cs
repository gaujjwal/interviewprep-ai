using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewPrep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExperienceLevels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Add nullable ExperienceLevelId to Questions
            migrationBuilder.AddColumn<int>(
                name: "ExperienceLevelId",
                table: "Questions",
                type: "int",
                nullable: true);


            // 2. Create ExperienceLevels table
            migrationBuilder.CreateTable(
                name: "ExperienceLevels",
                columns: table => new
                {
                    Id = table.Column<int>(
                        type: "int",
                        nullable: false)
                        .Annotation(
                            "SqlServer:Identity",
                            "1, 1"),

                    Name = table.Column<string>(
                        type: "nvarchar(50)",
                        maxLength: 50,
                        nullable: false),

                    MinYears = table.Column<int>(
                        type: "int",
                        nullable: true),

                    MaxYears = table.Column<int>(
                        type: "int",
                        nullable: true),

                    SortOrder = table.Column<int>(
                        type: "int",
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_ExperienceLevels",
                        x => x.Id);
                });


            // 3. Add standard experience ranges
            migrationBuilder.InsertData(
                table: "ExperienceLevels",
                columns: new[]
                {
            "Id",
            "Name",
            "MinYears",
            "MaxYears",
            "SortOrder"
                },
                values: new object[,]
                {
            {
                1,
                "Any Experience",
                null,
                null,
                1
            },
            {
                2,
                "0-2 Years",
                0,
                2,
                2
            },
            {
                3,
                "3-5 Years",
                3,
                5,
                3
            },
            {
                4,
                "6-8 Years",
                6,
                8,
                4
            },
            {
                5,
                "9-12 Years",
                9,
                12,
                5
            },
            {
                6,
                "12+ Years",
                12,
                null,
                6
            }
                });


            // 4. Map existing question data
            migrationBuilder.Sql(
                """
        UPDATE dbo.Questions
        SET ExperienceLevelId = 1
        WHERE LOWER(LTRIM(RTRIM(ExperienceLevel)))
              IN ('any', 'any experience');


        UPDATE dbo.Questions
        SET ExperienceLevelId = 3
        WHERE LOWER(LTRIM(RTRIM(ExperienceLevel)))
              IN ('3 years', '5 years');
        """
            );


            // 5. Safety check
            // Migration must fail if an existing question
            // could not be mapped.
            migrationBuilder.Sql(
                """
        IF EXISTS
        (
            SELECT 1
            FROM dbo.Questions
            WHERE ExperienceLevelId IS NULL
        )
        BEGIN
            THROW 51002,
            'Experience-level migration failed: unmapped questions exist.',
            1;
        END;
        """
            );


            // 6. Add indexes
            migrationBuilder.CreateIndex(
                name: "IX_Questions_ExperienceLevelId",
                table: "Questions",
                column: "ExperienceLevelId");


            migrationBuilder.CreateIndex(
                name: "UX_ExperienceLevels_Name",
                table: "ExperienceLevels",
                column: "Name",
                unique: true);


            // 7. Add FK
            migrationBuilder.AddForeignKey(
                name: "FK_Questions_ExperienceLevels_ExperienceLevelId",
                table: "Questions",
                column: "ExperienceLevelId",
                principalTable: "ExperienceLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_ExperienceLevels_ExperienceLevelId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "ExperienceLevels");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ExperienceLevelId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ExperienceLevelId",
                table: "Questions");
        }
    }
}
