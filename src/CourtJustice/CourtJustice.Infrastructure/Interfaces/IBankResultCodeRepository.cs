using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface IBankResultCodeRepository
	{
        Task<List<BankResultCode>> GetAll();
        Task Create(BankResultCode model);
        Task Update(string id, BankResultCode model);
        Task Delete(string id);
        Task<BankResultCode> GetByKey(string id);
    }
}

