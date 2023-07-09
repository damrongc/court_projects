using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IRemainTaskRepository
    {
        //Task<IEnumerable<RemainTaskViewModel>> GetPaging(string employeeCode, int skip, int take, string filter);
        //Task<int> GetRecordCount(string employeeCode, string filter);

        Task<List<RemainTask>> GetAll();
        Task Create(RemainTask model);
        Task Update(int id, RemainTask model);
        Task Delete(int id);
    }
}
