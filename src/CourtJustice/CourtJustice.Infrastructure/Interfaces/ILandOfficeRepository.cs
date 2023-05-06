using System;
using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface ILandOfficeRepository
	{
        Task<List<LandOffice>> GetAll();
        Task Create(LandOffice model);
        Task Update(string id, LandOffice model);
        Task Delete(string id);
        Task<LandOffice> GetByKey(string id);
    }
}

