using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IDashboardRepository
    {
        Task<IEnumerable<RemainTaskViewModel>> GetRemainTaskPaging(string employeeCode, int skip, int take, string filter);
        Task<int> GetRemainTaskRecordCount(string employeeCode, string filter);
        Task<IEnumerable<LoaneeRemarkViewModel>> GetEmployeeTodoPaging(string employeeCode, int skip, int take, string filter);
        Task<int> GetEmployeeTodoRecordCount(string employeeCode, string filter);

        Task<List<LoaneeSummaryViewModel>> GetLoaneeSummary(int groupId,string employeeCode);

        Task<List<LoaneeSummaryViewModel>> GetPaymentSummary(string employeeCode,string employerCode, string startDate,string endDate);
    }
}
