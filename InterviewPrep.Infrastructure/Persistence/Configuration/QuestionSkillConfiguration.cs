using InterviewPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewPrep.Infrastructure.Persistence.Configurations;

public sealed class QuestionSkillConfiguration
    : IEntityTypeConfiguration<QuestionSkill>
{
    public void Configure(
        EntityTypeBuilder<QuestionSkill> builder)
    {
        builder.ToTable("QuestionSkills");


        // Composite primary key
        builder.HasKey(questionSkill => new
        {
            questionSkill.QuestionId,
            questionSkill.SkillId
        });


        // Question relationship
        builder.HasOne(questionSkill => questionSkill.Question)
            .WithMany(question => question.QuestionSkills)
            .HasForeignKey(questionSkill => questionSkill.QuestionId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName(
                "FK_QuestionSkills_Questions_QuestionId"
            );


        // Skill relationship
        builder.HasOne(questionSkill => questionSkill.Skill)
            .WithMany(skill => skill.QuestionSkills)
            .HasForeignKey(questionSkill => questionSkill.SkillId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName(
                "FK_QuestionSkills_Skills_SkillId"
            );


        // Useful for searches by skill
        builder.HasIndex(questionSkill => questionSkill.SkillId)
            .HasDatabaseName(
                "IX_QuestionSkills_SkillId"
            );
    }
}