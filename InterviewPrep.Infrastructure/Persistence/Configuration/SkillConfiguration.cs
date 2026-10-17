using InterviewPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewPrep.Infrastructure.Persistence.Configurations;

public sealed class SkillConfiguration
    : IEntityTypeConfiguration<Skill>
{
    public void Configure(
        EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skills");

        builder.HasKey(skill => skill.Id);

        builder.Property(skill => skill.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(skill => skill.Name)
            .IsUnique()
            .HasDatabaseName("UX_Skills_Name");
    }
}