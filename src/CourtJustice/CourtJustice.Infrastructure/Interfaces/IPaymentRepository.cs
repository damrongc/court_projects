using System;
using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface IPaymentRepository
	{
        Task<List<Payment>> GetAll();
        Task Create(Payment model);
        Task Update(int id, Payment model);
        Task Delete(int id);
        Task<Payment> GetByKey(int id);

        Task<List<PaymentViewModel>> GetByCusId(string id);
        //Task<IEnumerable<AssetLandViewModel>> GetPaging(int skip, int take, string filter);
        //Task<int> GetRecordCount(string filter);
        bool IsExisting(int id);

        Task<int> GetPaymentSeq(string cusId);
    }
}

