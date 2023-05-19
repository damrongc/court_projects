using System;
using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IReferencerRepository
    {
        Task<List<Referencer>> GetAll();
        Task Create(Referencer model);
        Task Update(string id, Referencer model);
        Task Delete(string id);
        Task<Referencer> GetByKey(string id);

        Task<List<ReferencerViewModel>> GetByCusId(string id);      
        bool IsExisting(string id);

    }
}

