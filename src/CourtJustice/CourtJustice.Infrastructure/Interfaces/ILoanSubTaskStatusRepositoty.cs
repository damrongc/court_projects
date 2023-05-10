using System;
using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface ILoanSubTaskStatusRepository
	{
        Task<List<LoanSubTaskStatus>> GetAll();
        Task Create(LoanSubTaskStatus model);
        Task Update(int id, LoanSubTaskStatus model);
        Task Delete(int id);
        Task<LoanSubTaskStatus> GetByKey(int id);


        
    }
}

