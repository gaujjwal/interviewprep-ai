using InterviewPrep.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewPrep.Infrastructure.Persistence.Configurations;

public sealed class CompanyConfiguration
    : IEntityTypeConfiguration<Company>
{
    public void Configure(
        EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(company => company.Id);

        builder.Property(company => company.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(company => company.Name)
            .IsUnique()
            .HasDatabaseName("UX_Companies_Name");
    }
}