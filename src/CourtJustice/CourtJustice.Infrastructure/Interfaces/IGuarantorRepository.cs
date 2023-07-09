using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IGuarantorRepository
    {
        Task<List<Guarantor>> GetAll();
        Task Create(Guarantor model);
        Task Update(int id, Guarantor model);
        Task Delete(int id);
        Task<Guarantor> GetByKey(int id);


        Task<List<GuarantorViewModel>> GetByCusId(string id);
        bool IsExisting(int id);

    }
}


