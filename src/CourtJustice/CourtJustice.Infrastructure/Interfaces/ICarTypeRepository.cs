using System;
using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface ICarTypeRepository
	{
        Task<List<CarType>> GetAll();
        Task Create(CarType model);
        Task Update(string id, CarType model);
        Task Delete(string id);
        Task<CarType> GetByKey(string id);
    }
}


