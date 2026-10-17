using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewPrep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillsAndQuestionSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionSkills",
                columns: table => new
                {
                    QuestionId = table.Column<long>(type: "bigint", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionSkills", x => new { x.QuestionId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_QuestionSkills_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateIndex(
              name: "IX_QuestionSkills_SkillId",
              table: "QuestionSkills",
              column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "UX_Skills_Name",
                table: "Skills",
                column: "Name",
                unique: true);
            // Seed the original categories with their exact historical IDs
            // (1002/1003 exist because of manual duplicate cleanup performed
            // during early development, before migration-based seeding existed).
            migrationBuilder.Sql(
                """
IF NOT EXISTS (SELECT 1 FROM dbo.Categories WHERE Id = 1)
BEGIN
    SET IDENTITY_INSERT dbo.Categories ON;

    INSERT INTO dbo.Categories (Id, Name)
    VALUES
        (1, 'Technical'),
        (2, 'HR'),
        (3, 'Coding'),
        (4, 'System Design'),
        (1002, 'SQL'),
        (1003, 'Azure');

    SET IDENTITY_INSERT dbo.Categories OFF;
END;
"""
            );
            // Seed the 5 baseline roles if they don't already exist.
            // These predate our migration-based seeding discipline and were
            // originally created manually during early development.
            migrationBuilder.Sql(
                """
    IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Id = 1)
    BEGIN
        SET IDENTITY_INSERT dbo.Roles ON;

        INSERT INTO dbo.Roles (Id, Name)
        VALUES
            (1, '.NET Developer'),
            (2, 'Angular Developer'),
            (3, 'Azure Developer'),
            (4, 'AWS Developer'),
            (5, 'Full Stack Developer');

        SET IDENTITY_INSERT dbo.Roles OFF;
    END;
    """
            );
            // Seed the 5 baseline interview questions if they don't already exist.
            // Ensures a fresh database has the reference questions that
            // Skill/Company mappings below depend on.
            migrationBuilder.Sql(
                """
    IF NOT EXISTS (SELECT 1 FROM dbo.Questions WHERE Id = 1)
    BEGIN
        SET IDENTITY_INSERT dbo.Questions ON;

        INSERT INTO dbo.Questions
            (Id, Title, Answer, Category, ExperienceLevel, RoleId, CategoryId, ExperienceLevelId)
        VALUES
            (1, 'What is Dependency Injection?',
                'Dependency Injection is a design pattern used to achieve loose coupling.',
                'Technical', '3-5 Years', 1, 1, 3),

            (2, 'Explain Middleware in ASP.NET Core',
                'Middleware handles requests and responses in the HTTP pipeline.',
                'Technical', '3-5 Years', 1, 1, 3),

            (3, 'What is a SQL Join?',
                'A SQL Join combines rows from two or more tables.',
                'SQL', '3-5 Years', 1, 1002, 3),

            (4, 'What is Azure Blob Storage?',
                'Azure Blob Storage is Microsoft cloud object storage.',
                'Azure', '3-5 Years', 1, 1003, 3),

            (5, 'Tell me about yourself',
                'A concise professional introduction.',
                'HR', 'Any Experience', 1, 2, 1);

        SET IDENTITY_INSERT dbo.Questions OFF;
    END;
    """
            );
            migrationBuilder.InsertData(
    table: "Skills",
    columns: new[]
    {
        "Id",
        "Name"
    },
    values: new object[,]
    {
        { 1, "C#" },
        { 2, "ASP.NET Core" },
        { 3, "Dependency Injection" },
        { 4, "Middleware" },
        { 5, "SQL Server" },
        { 6, "SQL Joins" },
        { 7, "Azure" },
        { 8, "Azure Blob Storage" },
        { 9, "Communication" },
        { 10, "Behavioral Interview" }
    });

            // Verify that the expected existing questions are available
            // before creating QuestionSkill relationships.
            migrationBuilder.Sql(
                """
    IF
    (
        SELECT COUNT(*)
        FROM dbo.Questions
        WHERE Id IN (1, 2, 3, 4, 5)
    ) <> 5
    BEGIN
        THROW 51003,
        'Skill migration failed: expected existing questions 1 through 5 were not found.',
        1;
    END;
    """
            );

            migrationBuilder.InsertData(
    table: "QuestionSkills",
    columns: new[]
    {
        "QuestionId",
        "SkillId"
    },
    values: new object[,]
    {
        // Question 1 - Dependency Injection
        { 1L, 1 },
        { 1L, 2 },
        { 1L, 3 },

        // Question 2 - ASP.NET Core Middleware
        { 2L, 1 },
        { 2L, 2 },
        { 2L, 4 },

        // Question 3 - SQL Join
        { 3L, 5 },
        { 3L, 6 },

        // Question 4 - Azure Blob Storage
        { 4L, 7 },
        { 4L, 8 },

        // Question 5 - Tell me about yourself
        { 5L, 9 },
        { 5L, 10 }
    });

          
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionSkills");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
