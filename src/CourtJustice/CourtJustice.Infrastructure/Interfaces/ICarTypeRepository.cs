using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ICarTypeRepository
    {
        Task<List<CarType>> GetAll();
        Task Create(CarType model);
        Task Update(int id, CarType model);
        Task Delete(int id);
        Task<CarType> GetByKey(int id);

        bool IsExisting(int id);
    }
}


