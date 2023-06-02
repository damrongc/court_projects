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
    }
}
