using System;
using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface INationalityRepository
	{
        Task<List<Nationality>> GetAll();
        Task Create(Nationality model);
        Task Update(string id, Nationality model);
        Task Delete(string id);
        Task<Nationality> GetByKey(string id);
    }
}

