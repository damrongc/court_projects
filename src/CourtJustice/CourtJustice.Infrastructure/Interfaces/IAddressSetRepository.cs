using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IAddressSetRepository
    {
        Task<AddressSet> GetById(int id);
        Task<IEnumerable<AddressSet>> GetPaging(int skip, int take, string filter);
        Task<int> GetRecordCount(string filter);
    }
}
