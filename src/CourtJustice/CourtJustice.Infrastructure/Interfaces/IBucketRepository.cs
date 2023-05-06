using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface IBucketRepository
	{

        Task<List<Bucket>> GetAll();
        Task Create(Bucket model);
        Task Update(int id, Bucket model);
        Task Delete(int id);
        Task<Bucket> GetByKey(int id);
    }
}

