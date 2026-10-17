using InterviewPrep.Domain.Entities;
using InterviewPrep.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InterviewPrep.Infrastructure.Persistence;

public class InterviewPrepDbContext : DbContext
{
    public InterviewPrepDbContext(
        DbContextOptions<InterviewPrepDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<Question> Questions => Set<Question>();

    public DbSet<UserProgress> UserProgresses =>
        Set<UserProgress>();

    public DbSet<Category> Categories =>
        Set<Category>();
    public DbSet<ExperienceLevel> ExperienceLevels =>
    Set<ExperienceLevel>();

    public DbSet<Skill> Skills => Set<Skill>();

    public DbSet<QuestionSkill> QuestionSkills =>
        Set<QuestionSkill>();
    public DbSet<Company> Companies =>
    Set<Company>();

    public DbSet<QuestionCompany> QuestionCompanies =>
        Set<QuestionCompany>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(InterviewPrepDbContext).Assembly
        );
    }
}