using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface IGuarantorRepository
	{
        Task<List<Guarantor>> GetAll();
        Task Create(Guarantor model);
        Task Update(string id, Guarantor model);
        Task Delete(string id);
        Task<Guarantor> GetByKey(string id);

      
        Task<List<GuarantorViewModel>> GetByCusId(string id);
        bool IsExisting(string id);

    }
}


