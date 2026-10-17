using InterviewPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewPrep.Infrastructure.Persistence.Configurations;

public sealed class QuestionCompanyConfiguration
    : IEntityTypeConfiguration<QuestionCompany>
{
    public void Configure(
        EntityTypeBuilder<QuestionCompany> builder)
    {
        builder.ToTable("QuestionCompanies");

        // Composite Primary Key
        builder.HasKey(questionCompany => new
        {
            questionCompany.QuestionId,
            questionCompany.CompanyId
        });


        // Question relationship
        builder.HasOne(
                questionCompany => questionCompany.Question
            )
            .WithMany(
                question => question.QuestionCompanies
            )
            .HasForeignKey(
                questionCompany => questionCompany.QuestionId
            )
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName(
                "FK_QuestionCompanies_Questions_QuestionId"
            );


        // Company relationship
        builder.HasOne(
                questionCompany => questionCompany.Company
            )
            .WithMany(
                company => company.QuestionCompanies
            )
            .HasForeignKey(
                questionCompany => questionCompany.CompanyId
            )
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName(
                "FK_QuestionCompanies_Companies_CompanyId"
            );


        // Important for company-based search
        builder.HasIndex(
                questionCompany => questionCompany.CompanyId
            )
            .HasDatabaseName(
                "IX_QuestionCompanies_CompanyId"
            );
    }
}