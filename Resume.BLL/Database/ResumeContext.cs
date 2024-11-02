using BeaniesUtilities.Models.Resume;
using BeaniesUtilities.SQLDataOperations.Resume.SQL.EntityMapping;
using Microsoft.EntityFrameworkCore;

namespace Gay.TCazier.Resume.BLL.Contexts;

public class ResumeContext : DbContext
{
    public DbSet<TechTagModel> TechTags => Set<TechTagModel>();
    public DbSet<AddressModel> Addresses => Set<AddressModel>();
    public DbSet<PhoneNumberModel> PhoneNumbers => Set<PhoneNumberModel>();
    public DbSet<EducationInstitutionModel> EducationInstitutions => Set<EducationInstitutionModel>();
    public DbSet<EducationDegreeModel> EducationDegrees => Set<EducationDegreeModel>();
    public DbSet<CertificateModel> Certificates => Set<CertificateModel>();
    public DbSet<ProjectModel> Projects => Set<ProjectModel>();
    public DbSet<WorkExperienceModel> WorkExperiences => Set<WorkExperienceModel>();
    public DbSet<PersonModel> People => Set<PersonModel>();

    public ResumeContext(DbContextOptions<ResumeContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Not proper logging
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TechTagMapping());
        modelBuilder.ApplyConfiguration(new AddressMapping());
        modelBuilder.ApplyConfiguration(new PhoneNumberMapping());
        modelBuilder.ApplyConfiguration(new EducationalInstitutionMapping());
        modelBuilder.ApplyConfiguration(new EducationDegreeMapping());
        modelBuilder.ApplyConfiguration(new CertificateMapping());
        modelBuilder.ApplyConfiguration(new ProjectModelMapping());
        modelBuilder.ApplyConfiguration(new WorkExperienceMapping());
        modelBuilder.ApplyConfiguration(new PersonModelMapping());
        modelBuilder.ApplyConfiguration(new ResumeModelMapping());
    }
}