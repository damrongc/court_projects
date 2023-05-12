using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class ReportRepository : BaseRepository, IReportRepository
    {
        public ReportRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public Task<LoaneeNoticeViewModel> GetNotice(string cusId)
        {
            throw new NotImplementedException();
        }
    }
}
