using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IApplicationDbContext
    {
        bool HasChanges { get; }


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
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<LandOffice> LandOffices { get; set; }
        public DbSet<LawCase> LawCases { get; set; }
        public DbSet<LoanType> LoanTypes { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<Referencer> Referencers { get; set; }
        public DbSet<LoanSubTaskStatus> LoanSubTaskStatuses { get; set; }
        public DbSet<LoanTaskStatus> LoanTaskStatuses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<LoaneeRemark> LoaneeRemarks { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<JusticeCase> JusticeCases { get; set; }
        public DbSet<JusticeAppointment> JusticeAppointments { get; set; }
        public DbSet<JusticeCaseDocument> JusticeCaseDocuments { get; set; }
        public DbSet<JusticeCaseLawyer> JusticeCaseLawyers { get; set; }
        public DbSet<BankActionCode> BankActionCodes { get; set; }
        public DbSet<BankResultCode> BankResultCodes { get; set; }
        public DbSet<CompanyActionCode> CompanyActionCodes { get; set; }
        public DbSet<CompanyResultCode> CompanyResultCodes { get; set; }
        public DbSet<RemainTask> RemainTasks { get; set; }
        public DbSet<UserEmployerMapping> UserEmployerMappings { get; set; }

        public DbSet<MonthModel> MonthModels { get; set; }
        public DbSet<ReceiptSummary> ReceiptSummaries { get; set; }



        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
