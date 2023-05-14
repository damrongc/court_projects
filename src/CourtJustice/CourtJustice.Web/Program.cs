using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using CourtJustice.Infrastructure;
using CourtJustice.Infrastructure.Interfaces;
using CourtJustice.Infrastructure.Repositories;
using CourtJustice.Infrastructure.Seeds;
using CourtJustice.Web.ActionFilters;
using FastReport.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CourtJustice.Web
{
    public class Program
    {
        public static int Progress { get; set; }
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


            FastReport.Utils.RegisteredObjects.AddConnection(typeof(MySqlDataConnection));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseNpgsql(connectionString);
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                options.UseSnakeCaseNamingConvention();
            });

            // Add services to the container.
            builder.Services.AddTransient<IAppUserRepository, AppUserRepository>();
            builder.Services.AddTransient<IAddressSetRepository, AddressSetRepository>();
            builder.Services.AddTransient<IMenuRepository, MenuRepository>();
            builder.Services.AddTransient<IGroupUserRepository, GroupUserRepository>();
            builder.Services.AddTransient<ILawyerRepository, LawyerRepository>();
            builder.Services.AddTransient<ITitleRepository, TitleRepository>();
            builder.Services.AddTransient<IAssetCarRepository, AssetCarRepository>();
            builder.Services.AddTransient<IAssetImageRepository, AssetImageRepository>();
            builder.Services.AddTransient<IAssetLandRepository, AssetLandRepository>();
            builder.Services.AddTransient<IAssetSalaryRepository, AssetSalaryRepository>();
            builder.Services.AddTransient<IAssetTypeRepository, AssetTypeRepository>();
            builder.Services.AddTransient<ILoaneeRepository, LoaneeRepository>();
            builder.Services.AddTransient<IBucketRepository, BucketRepository>();
            builder.Services.AddTransient<ICardTypeRepository, CardTypeRepository>();
            builder.Services.AddTransient<ICaseResultRepository, CaseResultRepository>();
            builder.Services.AddTransient<ICreditTypeRepository, CreditTypeRepository>();
            builder.Services.AddTransient<ILandOfficeRepository, LandOfficeRepository>();
            builder.Services.AddTransient<ILawCaseRepository, LawCaseRepository>();
            builder.Services.AddTransient<ILawOfficeRepository, LawOfficeRepository>();
            builder.Services.AddTransient<ILawyerRepository, LawyerRepository>();
            builder.Services.AddTransient<INationalityRepository, NationalityRepository>();

            builder.Services.AddTransient<IOccupationRepository, OccupationRepository>();
            builder.Services.AddTransient<IGuarantorRepository, GuarantorRepository>();
            builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddTransient<IEmployerRepository, EmployerRepository>();
            builder.Services.AddTransient<ICourtRepository, CourtRepository>();
            builder.Services.AddTransient<ILoanTypeRepository, LoanTypeRepository>();
            builder.Services.AddTransient<ICarTypeRepository, CarTypeRepository >();
            builder.Services.AddTransient<ILoanTaskStatusRepository, LoanTaskStatusRepository>();
            builder.Services.AddTransient<ILoanSubTaskStatusRepository, LoanSubTaskStatusRepository>();
            builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();


            builder.Services.AddScoped<RequestAuthenticationFilter>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddNotyf(o =>
            {
                o.DurationInSeconds = 5;
                o.IsDismissable = true;
                o.HasRippleEffect = true;

            });
            builder.Services.AddSession();
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = ".Court.Session";
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.IsEssential = true;

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseNotyf();
            app.UseHttpsRedirection();
           // app.UseFastReport();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("app");
                try
                {
                    //var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    DefaultProgram.Seed(context);
                    DefaultGroupUser.Seed(context);
                    DefaultUser.Seed(context);
                    DefaultTaskStatus.Seed(context);
                    DefaultOccupation.Seed(context);
                    DefaultBucket.Seed(context);
                    DefaultEmployer.Seed(context);
                    DefaultEmployee.Seed(context);
                    DefaultLandOffice.Seed(context);
                    DefaultLoanee.Seed(context);
                    logger.LogInformation("Application Starting");
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "An error occurred seeding the DB");
                }
            }
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            app.Run();
        }
    }
}