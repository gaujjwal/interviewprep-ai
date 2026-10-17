using InterviewPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewPrep.Infrastructure.Persistence.Configurations;

public sealed class ExperienceLevelConfiguration
    : IEntityTypeConfiguration<ExperienceLevel>
{
    public void Configure(
        EntityTypeBuilder<ExperienceLevel> builder)
    {
        builder.ToTable("ExperienceLevels");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.SortOrder)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique()
            .HasDatabaseName(
                "UX_ExperienceLevels_Name"
            );
    }
}