using CourtJustice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ILawyerRepository
    {
        Task<List<Lawyer>> GetAll();
        Task Create(Lawyer model);
        Task Update(int id, Lawyer model);
        Task Delete(int id);
        Task<Lawyer> GetByKey(int id);

        Task<int> CheckExistingAtUser(int id);
    }
}
