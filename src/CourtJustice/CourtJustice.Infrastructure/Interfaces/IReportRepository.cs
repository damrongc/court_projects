using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IReportRepository
    {
        Task<LoaneeNoticeViewModel> GetNotice(string cusId);
        Task<List<LoaneeRemarkExcelViewModel>> GetLoaneeRemark(string employerCode,string startDate , string endDate);

        Task<List<PaymentExcelViewModel>> GetLoaneeReceipt(string employerCode, string startDate, string endDate);

    }
}
