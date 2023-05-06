using System;
using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface ICreditTypeRepository
	{
        Task<List<CreditType>> GetAll();
        Task Create(CreditType model);
        Task Update(string id, CreditType model);
        Task Delete(string id);
        Task<CreditType> GetByKey(string id);
    }
}

