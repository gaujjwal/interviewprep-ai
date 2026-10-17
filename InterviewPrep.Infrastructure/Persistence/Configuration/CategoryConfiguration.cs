using InterviewPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewPrep.Infrastructure.Persistence.Configurations;

public sealed class CategoryConfiguration
    : IEntityTypeConfiguration<Category>
{
    public void Configure(
        EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(category => category.Id);

        builder.Property(category => category.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(category => category.Name)
            .IsUnique()
            .HasDatabaseName("UX_Categories_Name");
    }
}