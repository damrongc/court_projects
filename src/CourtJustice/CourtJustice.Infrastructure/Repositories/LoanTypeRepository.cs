using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtJustice.Infrastructure.Repositories
{
    public class LoanTypeRepository : BaseRepository, ILoanTypeRepository
    {
        public LoanTypeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public Task Create(LoanType model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LoanType>> GetAll()
        {
            var loadTypes = new List<LoanType>();
            loadTypes.Add(new LoanType { LoanTypeCode = "01",LoanTypeName="สินเชื่อส่วนบุคคล" });
            loadTypes.Add(new LoanType { LoanTypeCode = "02",LoanTypeName= "สินเชื่อรถ" });
            return loadTypes;
        }

        public Task<LoanType> GetByKey(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(string id, LoanType model)
        {
            throw new NotImplementedException();
        }
    }
}
