using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IJusticeCaseLawyerRepository
    {
        Task Create(JusticeCaseLawyer model);

        Task<List<JusticeCaseLawyerViewModel>> GetByCaseNo(string id);
    }
}
