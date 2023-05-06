using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface ILawOfficeRepository
	{
        Task<List<LawOffice>> GetAll();
        Task Create(LawOffice model);
        Task Update(int id, LawOffice model);
        Task Delete(string id);
        Task<LawOffice> GetByKey(string id);
    }
}

