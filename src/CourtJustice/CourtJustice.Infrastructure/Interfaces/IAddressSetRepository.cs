using CourtJustice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IAddressSetRepository
    {
        Task<AddressSet> GetById(int id);
        Task<IEnumerable<AddressSet>> GetPaging(int skip, int take, string filter);
        Task<int> GetRecordCount(string filter);
    }
}
