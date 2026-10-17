using InterviewPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewPrep.Infrastructure.Persistence.Configurations;

public sealed class QuestionConfiguration
    : IEntityTypeConfiguration<Question>
{
    public void Configure(
        EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.HasKey(question => question.Id);

        builder.Property(question => question.Title)
            .IsRequired();

        builder.Property(question => question.Answer)
            .IsRequired();

        builder.Property(question => question.Category)
            .IsRequired();

        builder.Property(question => question.ExperienceLevel)
            .IsRequired();

        builder.HasOne(question => question.CategoryDetails)
            .WithMany(category => category.Questions)
            .HasForeignKey(question => question.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName(
                "FK_Questions_Categories_CategoryId"
            );

        builder.HasIndex(question => question.CategoryId)
            .HasDatabaseName(
                "IX_Questions_CategoryId"
            );
        builder.HasOne(
        question => question.ExperienceLevelDetails
    )
    .WithMany(
        experience => experience.Questions
    )
    .HasForeignKey(
        question => question.ExperienceLevelId
    )
    .OnDelete(DeleteBehavior.Restrict)
    .HasConstraintName(
        "FK_Questions_ExperienceLevels_ExperienceLevelId"
    );

        builder.HasIndex(
                question => question.ExperienceLevelId
            )
            .HasDatabaseName(
                "IX_Questions_ExperienceLevelId"
            );
    }
}
