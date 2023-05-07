using CourtJustice.Domain.Models;
namespace CourtJustice.Infrastructure.Interfaces
{
	public interface ICardTypeRepository
	{
        Task<List<CardType>> GetAll();
        Task Create(CardType model);
        Task Update(string id, CardType model);
        Task Delete(string id);
        Task<CardType> GetByKey(int id);
    }
}

