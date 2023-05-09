using System;
using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IReferencerRepository
    {
        Task<List<Referencer>> GetAll();
        Task Create(Referencer model);
        Task Update(string id, Referencer model);
        Task Delete(string id);
        Task<Referencer> GetByKey(string id);
    }
}

