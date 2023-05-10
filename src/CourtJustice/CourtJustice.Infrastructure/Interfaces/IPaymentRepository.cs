using System;
using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface IPaymentRepository
	{
        Task<List<Payment>> GetAll();
        Task Create(Payment model);
        Task Update(int id, Payment model);
        Task Delete(int id);
        Task<Payment> GetByKey(int id);
    }
}

