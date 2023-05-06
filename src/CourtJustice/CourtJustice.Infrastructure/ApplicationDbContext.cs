using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace CourtJustice.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext(DbContextOptions opt, IHttpContextAccessor httpContextAccessor) : base(opt)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public bool HasChanges => ChangeTracker.HasChanges();
        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<AppProgram> AppPrograms { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<LawOffice> LawOffices { get; set; }
        public DbSet<Loanee> Loanees { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<SubDistrict> SubDistricts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<AddressSet> AddressSets { get; set; }
        public DbSet<AssetLand> AssetLands { get; set; }
        public DbSet<AssetCar> AssetCars { get; set; }
        public DbSet<AssetSalary> AssetSalaries { get; set; }
        public DbSet<AssetImage> AssetImages { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<CaseType> CaseTypes { get; set; }
        public DbSet<CaseResult> CaseResults { get; set; }
        public DbSet<Bucket> Buckets { get; set; }
        public DbSet<CreditType> CreditTypes { get; set; }
        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<LandOffice> LandOffices { get; set; }
        public DbSet<LawCase> LawCases { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //var auditEntries = HandleAuditingBeforeSaveChanges("System");
            //int result = await base.SaveChangesAsync(cancellationToken);
            //await HandleAuditingAfterSaveChangesAsync(auditEntries, cancellationToken);

            //var xx = _httpContextAccessor.HttpContext.Session.GetStringValue("UserId");


            var userId = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            OnBeforeSaveChanges(userId);
            var result = await base.SaveChangesAsync();
            return result;
        }

        private void OnBeforeSaveChanges(string userId)
        {
            try
            {
                ChangeTracker.DetectChanges();

                foreach (var entry in ChangeTracker.Entries())
                {
                    if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                        continue;

                    if (entry.Entity is BaseEntity)
                    {
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                ((BaseEntity)entry.Entity).CreatedDateTime = DateTime.Now;
                                ((BaseEntity)entry.Entity).UserCreated = userId;
                                break;
                            case EntityState.Modified:
                                ((BaseEntity)entry.Entity).UpdatedDateTime = DateTime.Now;
                                ((BaseEntity)entry.Entity).UserUpdated = userId;
                                break;
                        }
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(builder);
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<decimal>()
                .HavePrecision(18, 2);
            configurationBuilder
                .Properties<string>()
                .HaveMaxLength(400);

        }
    }
}