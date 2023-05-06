using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface IGuarantorRepository
	{
        Task<List<Guarantor>> GetAll();
        Task Create(Guarantor model);
        Task Update(int id, Guarantor model);
        Task Delete(string id);
        Task<Guarantor> GetByKey(string id);
    }
}

