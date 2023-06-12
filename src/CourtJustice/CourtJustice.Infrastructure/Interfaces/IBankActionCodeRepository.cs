using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface IBankActionCodeRepository
	{
        Task<List<BankActionCode>> GetAll();
        Task Create(BankActionCode model);
        Task Update(string id, BankActionCode model);
        Task Delete(string id);
        Task<BankActionCode> GetByKey(string id);
    }
}

