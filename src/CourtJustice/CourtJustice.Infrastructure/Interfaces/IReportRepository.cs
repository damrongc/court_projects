using CourtJustice.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IReportRepository
    {
        Task<LoaneeNoticeViewModel> GetNotice(string cusId);
    }
}
