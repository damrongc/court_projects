using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IReferencerRepository
    {
        Task<List<Referencer>> GetAll();
        Task Create(Referencer model);
        Task Update(int id, Referencer model);
        Task Delete(int id);
        Task<Referencer> GetByKey(int id);

        Task<List<ReferencerViewModel>> GetByCusId(string id);
        bool IsExisting(int id);

    }
}

