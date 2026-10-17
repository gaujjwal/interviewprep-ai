using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewPrep.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCompaniesAndQuestionCompanies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ========================================
            // 1. Create Companies table
            // ========================================

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(
                        type: "int",
                        nullable: false)
                        .Annotation(
                            "SqlServer:Identity",
                            "1, 1"),

                    Name = table.Column<string>(
                        type: "nvarchar(150)",
                        maxLength: 150,
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_Companies",
                        x => x.Id);
                });


            // ========================================
            // 2. Create QuestionCompanies table
            // ========================================

            migrationBuilder.CreateTable(
                name: "QuestionCompanies",
                columns: table => new
                {
                    QuestionId = table.Column<long>(
                        type: "bigint",
                        nullable: false),

                    CompanyId = table.Column<int>(
                        type: "int",
                        nullable: false)
                },
                constraints: table =>
                {
                    // Composite Primary Key
                    table.PrimaryKey(
                        "PK_QuestionCompanies",
                        x => new
                        {
                            x.QuestionId,
                            x.CompanyId
                        });


                    // Company FK
                    table.ForeignKey(
                        name: "FK_QuestionCompanies_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);


                    // Question FK
                    table.ForeignKey(
                        name: "FK_QuestionCompanies_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            // ========================================
            // 3. Unique Company Name
            // ========================================

            migrationBuilder.CreateIndex(
                name: "UX_Companies_Name",
                table: "Companies",
                column: "Name",
                unique: true);


            // ========================================
            // 4. Company Search Index
            // ========================================

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCompanies_CompanyId",
                table: "QuestionCompanies",
                column: "CompanyId");


            // ========================================
            // 5. Seed Initial Companies
            // ========================================

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[]
                {
            "Id",
            "Name"
                },
                values: new object[,]
                {
            { 1, "Microsoft" },
            { 2, "Amazon" },
            { 3, "Google" },
            { 4, "Accenture" },
            { 5, "TCS" },
            { 6, "Adobe" }
                });


            // ========================================
            // 6. Validate Existing Questions
            // ========================================

            migrationBuilder.Sql(
                """
        IF
        (
            SELECT COUNT(*)
            FROM dbo.Questions
            WHERE Id IN (1, 2, 3, 4, 5)
        ) <> 5
        BEGIN
            THROW 51004,
            'Company migration failed: expected existing questions 1 through 5 were not found.',
            1;
        END;
        """
            );


            // ========================================
            // 7. Map Companies To Questions
            // ========================================

            migrationBuilder.InsertData(
                table: "QuestionCompanies",
                columns: new[]
                {
            "QuestionId",
            "CompanyId"
                },
                values: new object[,]
                {
            // Question 1 - Dependency Injection
            { 1L, 1 }, // Microsoft
            { 1L, 4 }, // Accenture


            // Question 2 - ASP.NET Core Middleware
            { 2L, 1 }, // Microsoft
            { 2L, 4 }, // Accenture


            // Question 3 - SQL Join
            { 3L, 2 }, // Amazon
            { 3L, 5 }, // TCS


            // Question 4 - Azure Blob Storage
            { 4L, 1 }, // Microsoft
            { 4L, 6 }, // Adobe


            // Question 5 - Behavioral Question
            { 5L, 1 }, // Microsoft
            { 5L, 2 }, // Amazon
            { 5L, 3 }, // Google
            { 5L, 4 }, // Accenture
            { 5L, 5 }, // TCS
            { 5L, 6 }  // Adobe
                });
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionCompanies");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
