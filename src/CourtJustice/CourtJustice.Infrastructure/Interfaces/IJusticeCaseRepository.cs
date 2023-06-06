using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IJusticeCaseRepository
    {
        Task Create(JusticeCase model);
        Task<JusticeCaseViewModel> GetByKey(string id);
        Task<JusticeCaseViewModel> GetByCusId(string cusId);
        Task Update(string id, JusticeCase model);

        Task<List<JusticeCaseViewModel>> GetAll();


        Task<IEnumerable<JusticeCaseViewModel>> GetPaging(string courtId, int caseResultId, int skip, int take, string filter);
        Task<int> GetRecordCount(string courtId, int caseResultId, string filter);
    }
}
